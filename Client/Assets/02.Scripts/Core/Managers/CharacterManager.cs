using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    #region Property
    public static CharacterManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<CharacterManager>();

            return _instance;
        }
    }
    #endregion
    private static CharacterManager _instance = null;

    [SerializeField] private BasePlayer _piratePrefab;

    public BasePlayer PlayerCharacterPrefab(int characterIndex) {
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
                return _piratePrefab;
            case 6:
                break;
        }
        return null;
    }

}
