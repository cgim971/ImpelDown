using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapItem : MonoBehaviour
{
    bool isFirst = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("Player") == collision.gameObject.layer)
        {
            if (isFirst)
            {
                isFirst = false;
                return;
            }
            StartCoroutine(TrapTrigger(collision.gameObject));
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator TrapTrigger(GameObject collision)
    {
        collision.GetComponent<BasePlayer>().InputModule.ReverseAble();
        yield return new WaitForSeconds(3f);
        collision.GetComponent<BasePlayer>().InputModule.ReverseAble();
        Destroy(this.gameObject);
    }
}
