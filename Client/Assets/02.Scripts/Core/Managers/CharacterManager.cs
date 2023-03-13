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
    [SerializeField] private BasePlayer _ninjaPrefab;
    [SerializeField] private BasePlayer _raccoonPrefab;
    [SerializeField] private BasePlayer _robotPrefab;

    public BasePlayer PlayerCharacterPrefab(int characterIndex) {
        switch (characterIndex) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                return _raccoonPrefab;
            case 3:
                return _piratePrefab;
            case 4:
                return _robotPrefab;
            case 5:
                return _ninjaPrefab;
            case 6:
                break;
        }
        return null;
    }


}
