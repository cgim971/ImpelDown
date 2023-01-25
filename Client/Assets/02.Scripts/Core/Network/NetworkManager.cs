using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    #region Property
    public static NetworkManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<NetworkManager>();

            return _instance;
        }
        set { _instance = value; }
    }
    #endregion

    private static NetworkManager _instance = null;


    private string _url = string.Empty;

    private ClientWebSocket _socket = null;
    private RecvBuffer _recvBuffer = null;


    private CancellationTokenSource _quitSource = null;
    private CancellationToken _quitToken = CancellationToken.None;


    private PacketManager _packetManager = null;
    private Queue<PacketMessage> _sendQueue = null;
    private bool _isReadyToSend = true;


    public void Init(string url) {
        _url = url;
        _recvBuffer = new RecvBuffer(1024 * 10);
        _packetManager = new PacketManager();
        _sendQueue = new Queue<PacketMessage>();
    }

    private void Update() {

        if (PacketQueue.Instance.Count > 0) {
            List<PacketMessage> list = PacketQueue.Instance.PopAll();

            foreach (PacketMessage pMsg in list) {
                IPacketHandler handler = _packetManager.GetPacketHandler(pMsg.Id);
                if (handler != null) {
                    handler.Process(pMsg.Message);
                }
                else {
                    Debug.LogError($"There is no handler for this packet : {pMsg.Id}");
                }
            }
        }

        if (_isReadyToSend && _sendQueue.Count > 0) {
            SendMessage();
        }
    }

    public void RegisterSend(ushort code, IMessage msg) => _sendQueue.Enqueue(new PacketMessage(code, msg));

    private async void SendMessage() {
        if (_socket != null && _socket.State == WebSocketState.Open) {
            _isReadyToSend = false;

            List<PacketMessage> sendList = new List<PacketMessage>();
            while (_sendQueue.Count > 0) {
                sendList.Add(_sendQueue.Dequeue());
            }

            byte[] sendBuffer = new byte[1024 * 1024];
            foreach (PacketMessage pMsg in sendList) {
                int len = pMsg.Message.CalculateSize();
                Array.Copy(BitConverter.GetBytes((ushort)(len + 4)), 0, sendBuffer, 0, sizeof(ushort));
                Array.Copy(BitConverter.GetBytes(pMsg.Id), 0, sendBuffer, 2, sizeof(ushort));
                Array.Copy(pMsg.Message.ToByteArray(), 0, sendBuffer, 4, len);

                ArraySegment<byte> segment = new ArraySegment<byte>(sendBuffer, 0, len + 4);
                await _socket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);
            }

            _isReadyToSend = true;
        }
    }

    public async void Connection() {
        if (_socket != null && _socket.State == WebSocketState.Open) {
            Debug.LogError("Alreay Connected!!");
            return;
        }

        _socket = new ClientWebSocket();
        Uri serverUri = new Uri(_url);

        try {
            await _socket.ConnectAsync(serverUri, CancellationToken.None);
            ReceiveLoop();
        }
        catch (Exception e) {
            Debug.LogError($"Connection Error : check server status... {e.Message}");
        }
    }

    private async void ReceiveLoop() {
        while (_socket != null && _socket.State == WebSocketState.Open) {
            try {
                WebSocketReceiveResult result = await _socket.ReceiveAsync(_recvBuffer.WriteSegment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Binary) {
                    if (result.EndOfMessage == true) {
                        _recvBuffer.OnWrite(result.Count);
                        int readByte = ProcessPacket(_recvBuffer.ReaddSegment);

                        if (readByte == 0) {
                            Disconnect();
                            break;
                        }

                        _recvBuffer.OnRead(readByte);
                        _recvBuffer.Clean();
                    }
                    else {
                        _recvBuffer.OnWrite(result.Count);
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Close) {
                    Debug.LogError("Socket closed by server in normal status");
                    break;
                }
            }
            catch (WebSocketException we) {
                Debug.LogError(we.Message);
                break;
            }
            catch (Exception e) {
                Debug.LogError($"{e.GetType()} : {e.Message}");
                break;
            }
        }
    }

    private int ProcessPacket(ArraySegment<byte> buffer) => _packetManager.OnRecvPacket(buffer);

    public void Disconnect() {
        if (_socket != null && _socket.State == WebSocketState.Open) {
            _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Quite Client", CancellationToken.None);
        }
    }

}
