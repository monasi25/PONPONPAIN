using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_RANK
{
    D,
    C,
    B,
    A,
}

public class RankController : ReferenceComboController
{
    [SerializeField] private bool m_onDebugRank = false; // デバッグ用、現在のランクを表示

    private int     m_iCurrentScore = 0; // コンボ数をもとにランクの増減度（スコア）を計算する
    private E_RANK  m_eCurrentRank  = E_RANK.D;
    public E_RANK GetCurrentRank => m_eCurrentRank; // 現在のランクを返す

    /// <summary>
    /// ランク増加
    /// </summary>
    public override void DoAddProcess()
    {
        m_iCurrentScore++;
        if (m_eCurrentRank == E_RANK.A) return;
     
        if (m_iCurrentScore == 5 || m_iCurrentScore == 10 || m_iCurrentScore == 15)
        {
            m_eCurrentRank++;
            if (m_onDebugRank) print("ランクアップ : " + m_eCurrentRank);
        }
    }

    /// <summary>
    /// ランク減少
    /// </summary>
    public override void DoResetProcess()
    {
        m_iCurrentScore = 0;
        if (m_eCurrentRank == E_RANK.D) return;
        
        m_eCurrentRank--;
        if (m_onDebugRank) print("ランクダウン : " + m_eCurrentRank);
    }
}
