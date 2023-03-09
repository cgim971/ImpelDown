using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{

    private bool _isUserIn = false;

    private CurState _currentState;


    private bool _isLock = false;
    public bool _IsLock { get { return _isLock; } set { } }

    public List<Sprite> _backImg;
    public Image _personImg;
    
    void Start()
    {
        _currentState = CurState.NONE;
    }

    void Update()
    {
        if(_currentState == CurState.LOCK)
        {
            GetComponent<Image>().sprite = _backImg[0];
            _personImg.GetComponent<Image>().color = new Color(_personImg.color.r, _personImg.color.g, _personImg.color.b, 0f);
        }
        else if(_currentState == CurState.NONE)
        {
            GetComponent<Image>().sprite = _backImg[1]; 
            _personImg.GetComponent<Image>().color = new Color(_personImg.color.r, _personImg.color.g, _personImg.color.b, 0f);
        }
        else if (_currentState == CurState.WAIT)
        {
            GetComponent<Image>().sprite = _backImg[2]; 
            _personImg.GetComponent<Image>().color = new Color(_personImg.color.r, _personImg.color.g, _personImg.color.b, 1f);
        }
        else
        {
            GetComponent<Image>().sprite = _backImg[3];
            _personImg.GetComponent<Image>().color = new Color(_personImg.color.r, _personImg.color.g, _personImg.color.b, 1f);
        }

    }


    #region SetState
    public void SetLock()
    {
        _isLock = !_isLock;
        Debug.Log(gameObject.name);
        _currentState = CurState.LOCK;
    }

    public void SetNONE()
    {
        _isLock = !_isLock;
        _currentState = CurState.NONE;
    }
    public void SetWait()
    {
        _currentState = CurState.WAIT;
    }

    public void SetReady()
    {
        _currentState = CurState.READY;
    }
    #endregion

    public CurState GetCurState() { return _currentState; }

}

public enum CurState
{
    LOCK,
    NONE,
    WAIT,
    READY
}
