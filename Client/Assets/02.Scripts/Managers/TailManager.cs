using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour {

    #region Property
    public static TailManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<TailManager>();
            }

            return _instance;
        }
    }
    #endregion
    private static TailManager _instance;

    [SerializeField] private GameObject _redTail;
    [SerializeField] private GameObject _orangeTail;
    [SerializeField] private GameObject _yellowTail;
    [SerializeField] private GameObject _greenTail;
    [SerializeField] private GameObject _blueTail;
    [SerializeField] private GameObject _navyTail;
    [SerializeField] private GameObject _purpleTail;
    [SerializeField] private GameObject _pinkTail;


    public GameObject GetTail(int tailIndex) {
        switch (tailIndex) {
            case 0:
                return _redTail;
            case 1:
                return _orangeTail;
            case 2:
                return _yellowTail;
            case 3:
                return _greenTail;
            case 4:
                return _blueTail;
            case 5:
                return _navyTail;
            case 6:
                return _purpleTail;
            case 7:
                return _pinkTail;
        }
        return null;
    }


}
