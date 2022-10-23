using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    [SerializeField] private bool m_onDebugJudgement = false; // デバッグ用、判定結果を表示
    // 判定に影響を受ける（イベントが発生する）クラスのリスト
    private List<ReferenceJudgement> m_referenceJudgementList = new List<ReferenceJudgement>();

    /// <summary>
    /// 判定に影響を受けるクラスをリストに追加
    /// </summary>
    public void AddRefferenceJudgementList(ReferenceJudgement _a)
    {
        m_referenceJudgementList.Add(_a);
    }

    /// <summary>
    /// 判定に影響を受けるクラスをリストから削除
    /// </summary>
    public void RemoveRefferenceJudgementList(ReferenceJudgement _a)
    {
        m_referenceJudgementList.Remove(_a);
    }

    /// <summary>
    /// 入力のタイミングを判定する
    /// </summary>
    public void JudgeRhythm(E_JUDGE _eJudge)
    {
        if (m_onDebugJudgement) print("判定 : " + _eJudge + "\n");

        for (int i = m_referenceJudgementList.Count - 1; 0 <= i; i--)
        {
            if (m_referenceJudgementList[i].OnJudge)
            {
                // 判定が有効ならばリスト内の各クラスのイベントを発生させる
                m_referenceJudgementList[i].SetEvent(_eJudge);
            }
        }
    }
}
