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
        CreateTail(_tailName, Player.TailIndex);
    }

    public void CreateTail(ETailName tailName,int tailIndex)
    {
        GameObject tail = Instantiate(Player.TailSO.allTails[(int)tailName].colorTail[tailIndex]);
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
