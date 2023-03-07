using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    private Sequence _seq;

    private bool isHost;
    #region GameObjectOrValues
    #region Title
    [SerializeField]
    private TMP_Text _hostName;
    #endregion

    #region Ready/Start
    private bool isReady = false;
    [SerializeField]
    private Button ReadyOrStartBtn;
    #endregion
    #region Users
    [SerializeField]
    private List<GameObject> _playerList;
    [SerializeField] 
    private List<Image> _imageList;

    #endregion
    #endregion


    void Start()
    {
        _hostName.text = GetHostName();
        isHost = CheckIsHost("플레이어 1");
        SetBtnColor();
        ReadyOrStartBtn.onClick.AddListener(ClickReadyOrStartBtn);
        if(isHost)
            ButtonSetting();
    }

    
    public void ButtonSetting()
    {
        foreach(Image image in _imageList)
        {
            image.GetComponent<Button>().onClick.AddListener(HostLock);
        }
    }


    /// <summary>
    /// 방 호스트가 자리를 잠군다
    /// </summary>
    public void HostLock()
    {
        Debug.Log("Lock");
    }


    /// <summary>
    /// 처음 방 들어갈 때 해당 방의 호스트 이름을 가져옴. 바뀔 때도 호출해야 함.
    /// </summary>
    /// <returns>string</returns>
    public string GetHostName()
    {
        string hostName = "플레이어 12";
        // 어쩌고 저쩌고 호스트 이름 가져옴
        return hostName;
    }


    #region ButtonClickFunc

    /// <summary>
    /// 처음 방 들어갈 때 자신이 해당 방의 호스트인지 확인. 바뀔 때도 호출해야 함.
    /// </summary>
    /// <returns>bool</returns>
    public bool CheckIsHost(string playerName)
    {
        return _hostName.text == playerName?true:false;
    }

    public void ClickReadyOrStartBtn()
    {
        if (isHost)
            StartButton();
        else
            ReadyButton();
    }
    /// <summary>
    /// 시작 버튼
    /// </summary>
    /// <returns>void</returns>
    public void StartButton()
    {
        if(/*만약 모든 플레이어가 레디상태라면*/true)
        {
            //게임 시작
        }
        else
        {
            Debug.Log("모든 플레이어가 준비가 되어있지 않습니다.");
        }
    }
    /// <summary>
    /// 준비 버튼
    /// </summary>
    /// <returns>void</returns>
    public void ReadyButton()
    {
        isReady = !isReady;
        // 서버에서 현재 상태 입력
        SetBtnColor();
    }
    #endregion


    /// <summary>
    /// 준비 혹은 시작 버튼의 색을 지정한다.
    /// </summary>
    /// <returns>void</returns>
    public void SetBtnColor()
    {
        if(isHost)
        {
            ReadyOrStartBtn.image.color = Color.gray;
        }
        else if (isReady)
        {
            ReadyOrStartBtn.image.color = Color.green;
        }
        else
        {
            ReadyOrStartBtn.image.color = Color.red;
        }
    }




    public void CurPlayer()
    {
        foreach (GameObject player in _playerList)
        {
            //만약 준비중이라면
        }
    }

}
