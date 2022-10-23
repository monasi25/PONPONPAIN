using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceComboController : MonoBehaviour
{
    private void Start()
    {
        AddList();
    }

    /// <summary>
    /// コンボ数に影響を受けるクラスとしてリストに登録する
    /// </summary>
    protected void AddList()
    {
        GameDirector.GetInstance.AddReferenceComboList(this);
    }

    /// <summary>
    /// コンボ加算時の処理
    /// </summary>
    public virtual void DoAddProcess()
    {

    }

    /// <summary>
    /// コンボリセット時の処理
    /// </summary>
    public virtual void DoResetProcess()
    {

    }
}
