using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : ReferenceJudgement
{
    [SerializeField] private bool m_onDebugComboCount = false; // デバッグ用、現在のコンボ数を表示
    // コンボ数に影響を受けるクラスのリスト
    private List<ReferenceComboController> m_referenceComboList = new List<ReferenceComboController>();
    private int m_iCurrentCombo = 0;
    public int GetCurrentCombo => m_iCurrentCombo;

    private void Start()
    {
        AddList();
        SetOnJudge(true);
    }

    public override void SetEvent(E_JUDGE _eJudge)
    {
        // 判定結果に合わせた処理を実行
        switch (_eJudge)
        {
            case E_JUDGE.None:
                break;

            case E_JUDGE.Bad:
                ResetCombo();
                break;

            case E_JUDGE.Good:
            case E_JUDGE.Great:
            case E_JUDGE.Perfect:
                AddCombo();
                break;
        }
    }

    /// <summary>
    /// コンボを加算する
    /// </summary>
    private void AddCombo()
    {
        m_iCurrentCombo++;
        for (int i = m_referenceComboList.Count - 1; 0 <= i; i--)
        {
            m_referenceComboList[i].DoAddProcess();
        }
        if (m_onDebugComboCount) print(m_iCurrentCombo + "コンボ");
    }

    /// <summary>
    /// コンボをリセットする
    /// </summary>
    public void ResetCombo()
    {
        m_iCurrentCombo = 0;
        for (int i = m_referenceComboList.Count - 1; 0 <= i; i--)
        {
            m_referenceComboList[i].DoResetProcess();
        }
        if (m_onDebugComboCount) print(m_iCurrentCombo + "コンボ");
    }

    /// <summary>
    /// コンボ数に影響を受けるクラスをリストに追加
    /// </summary>
    /// <param name="_a"></param>
    public void AddReferenceComboList(ReferenceComboController _a)
    {
        m_referenceComboList.Add(_a);
    }
}
