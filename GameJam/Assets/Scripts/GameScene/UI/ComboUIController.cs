using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUIController : ReferenceComboController
{
    [SerializeField] private Flamecall Flamecall;
    /// <summary>
    /// コンボ増加時の処理
    /// </summary>
    public override void DoAddProcess()
    {
        // 現在のコンボ数の取得
        int iCurrentCombo = GameDirector.GetInstance.GetCurrentCombo;
        if (iCurrentCombo > 1)
        {
            Flamecall.Combo(iCurrentCombo);
        }
    }

    /// <summary>
    /// コンボリセット時の処理
    /// </summary>
    public override void DoResetProcess()
    {

    }
}
