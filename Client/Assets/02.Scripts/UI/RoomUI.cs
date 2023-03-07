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
        isHost = CheckIsHost("�÷��̾� 1");
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
    /// �� ȣ��Ʈ�� �ڸ��� �ᱺ��
    /// </summary>
    public void HostLock()
    {
        Debug.Log("Lock");
    }


    /// <summary>
    /// ó�� �� �� �� �ش� ���� ȣ��Ʈ �̸��� ������. �ٲ� ���� ȣ���ؾ� ��.
    /// </summary>
    /// <returns>string</returns>
    public string GetHostName()
    {
        string hostName = "�÷��̾� 12";
        // ��¼�� ��¼�� ȣ��Ʈ �̸� ������
        return hostName;
    }


    #region ButtonClickFunc

    /// <summary>
    /// ó�� �� �� �� �ڽ��� �ش� ���� ȣ��Ʈ���� Ȯ��. �ٲ� ���� ȣ���ؾ� ��.
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
    /// ���� ��ư
    /// </summary>
    /// <returns>void</returns>
    public void StartButton()
    {
        if(/*���� ��� �÷��̾ ������¶��*/true)
        {
            //���� ����
        }
        else
        {
            Debug.Log("��� �÷��̾ �غ� �Ǿ����� �ʽ��ϴ�.");
        }
    }
    /// <summary>
    /// �غ� ��ư
    /// </summary>
    /// <returns>void</returns>
    public void ReadyButton()
    {
        isReady = !isReady;
        // �������� ���� ���� �Է�
        SetBtnColor();
    }
    #endregion


    /// <summary>
    /// �غ� Ȥ�� ���� ��ư�� ���� �����Ѵ�.
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
            //���� �غ����̶��
        }
    }

}
