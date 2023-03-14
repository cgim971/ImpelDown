using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    #region Property
    public static CharacterManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<CharacterManager>();
            }

            return _instance;
        }
    }
    #endregion
    private static CharacterManager _instance;


    [SerializeField] private BasePlayer _hunterPlayer;


    public BasePlayer GetPlayer(int characterIndex) {
        //switch (characterIndex) {

        //}

        return _hunterPlayer;
    }

}
