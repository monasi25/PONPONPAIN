using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceJudgement : MonoBehaviour
{
    private bool m_onJudge = false; // 判定が有効かどうか

    public bool OnJudge => m_onJudge;

    /// <summary>
    /// 判定を有効/無効にする
    /// </summary>
    protected void SetOnJudge(bool _a)
    {
        m_onJudge = _a;
    }

    /// <summary>
    /// 判定に影響を受けるクラスとしてリストに登録する
    /// </summary>
    protected void AddList()
    {
        GameDirector.GetInstance.AddReferenceJudgementList(this);
    }

    /// <summary>
    /// 判定に影響を受けるクラスのリストから削除する
    /// </summary>
    protected void RemoveList()
    {
        GameDirector.GetInstance.RemoveReferenceJudgementList(this);
    }

    /// <summary>
    /// 判定時の処理
    /// </summary>
    public virtual void SetEvent(E_JUDGE _eJudge)
    {

    }
}
