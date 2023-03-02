using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTailModule : MonoBehaviour
{

    #region Property
    public BasePlayer Player => _player;
    public ETailName TailName => _tailName;
    #endregion

    protected BasePlayer _player;
    protected ETailName _tailName = ETailName.Snake;

    public virtual void Init()
    {
        CreateTail(Player.TailIndex);
    }

    public void CreateTail(int tailIndex)
    {
        //GameObject Tails = transform.Find("Tails").gameObject;

        //if (Tails == null)
        //{
        //    Debug.Log("A");
        //    Tails = new GameObject
        //    {
        //        name = "Tails",
        //    };
        //    Tails.transform.SetParent(transform);
        //}

        //int tailIndex = 0;
        //tailIndex = Player.TargetTailIndex - 1 >= 0 ? Player.TargetTailIndex - 1 : PlayerManager.Instance.GetPlayerCount();

        GameObject tail = Instantiate(Player.TailSO.allTails[(int)_tailName].colorTail[tailIndex]);
        tail.transform.parent = transform;
        tail.transform.localPosition = new Vector3(-0.5f, -0.5f, 0f);
    }

    public void RemoveTail()
    {
        //GameObject Tails = transform.Find("Tails").gameObject;

        //if (Tails != null)
        //{
        //    Destroy(Tails);
        //}
    }
}

[System.Serializable]
public enum ETailName
{
    Snake,
}
