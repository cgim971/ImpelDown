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

    private Dictionary<Image, UserUI> _imageToUserUIDic = new Dictionary<Image, UserUI>();

    [SerializeField]
    private int userIdx = 1;

    #endregion
    #endregion

    private void Awake()
    {
        _hostName.text = GetHostName();
        isHost = CheckIsHost("�÷��̾� 11");
        SetBtnColor();
        if (isHost)
        {
            ButtonSetting();
        }
        ButtionDictionary();
        ReadyOrStartBtn.onClick.AddListener(ClickReadyOrStartBtn);
    }

    #region FirstSetting
    /// <summary>
    /// ��ư �̺�Ʈ ����
    /// </summary>
    public void ButtonSetting()
    {
        foreach(Image image in _imageList)
        {
            image.GetComponent<Button>().onClick.AddListener(delegate { HostLock(image.GetComponent<UserUI>()); });
        }
    }

    public void ButtionDictionary()
    {
        foreach (Image image in _imageList)
        {
            _imageToUserUIDic[image] = image.gameObject.GetComponent<UserUI>();
            Debug.Log(_imageToUserUIDic[image]);
        }
    }
    #endregion

    /// <summary>
    /// ���� ���� ui�� ����ִ��� 
    /// </summary>
    /// <param name="userUI"></param>
    /// <returns>bool</returns>
    bool CheckIsLocked(UserUI userUI)
    {
        return userUI.GetComponent<UserUI>()._IsLock;
    }


    void UserReady(int idx)
    {
        if (!CheckIsLocked(_imageToUserUIDic[_imageList[idx]]))
        {
            _imageToUserUIDic[_imageList[idx]].SetReady();
        }
    }

    void UserWait(int idx)
    {
        if (!CheckIsLocked(_imageToUserUIDic[_imageList[idx]]))
        {
            _imageToUserUIDic[_imageList[idx]].SetWait();
        }
    }

    
    


    /// <summary>
    /// �� ȣ��Ʈ�� �ڸ��� �ᱺ��
    /// </summary>
    public void HostLock(UserUI userUI)
    {
        Debug.Log("asdfdsf");
        if(CheckIsLocked(userUI))
        {
            userUI.GetComponent<UserUI>().SetNONE();
            userUI.GetComponent<UserUI>()._IsLock = false;
        }
        else
        {
            userUI.GetComponent<UserUI>().SetLock();
            userUI.GetComponent<UserUI>()._IsLock = true;
        }
    }


    /// <summary>
    /// ó�� �� �� �� �ش� ���� ȣ��Ʈ �̸��� ������. �ٲ� ���� ȣ���ؾ� ��.
    /// </summary>
    /// <returns>string</returns>
    public string GetHostName()
    {
        string hostName = "�÷��̾� 11";
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
        {
            ReadyButton();
            Debug.Log("asdf");
            if (_imageToUserUIDic[_imageList[userIdx]].GetCurState() == CurState.WAIT)
                UserReady(userIdx);
            else
                UserWait(userIdx);
        }
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

    public void CurUsers()
    {
        foreach (GameObject player in _playerList)
        {
            //���� �غ����̶��
        }
    }




}
