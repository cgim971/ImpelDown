using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("PlayerTs");
            Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("GhostTs"));
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("PlayerTs"));
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("GhostTs");
        }
    }
}
