using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour
{
    #region Property
    public static TailManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<TailManager>();

            return _instance;
        }
    }
    #endregion
    private static TailManager _instance = null;

    [SerializeField] private List<Sprite> _snakeTailList = new List<Sprite>();

    public Sprite GetSnakeTail(int characterIndex) {
        switch (characterIndex) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
            case 6:
                break;
        }
        return null;
    }

}
