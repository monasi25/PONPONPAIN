using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementUIController : ReferenceJudgement
{
    [SerializeField] private Animator UIanim;

    [SerializeField] private Flamecall Flamecall;
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
                Flamecall.BadCall();
                break;

            case E_JUDGE.Good:
                Flamecall.GoodCall();
                break;

            case E_JUDGE.Great:
                Flamecall.GreatCall();
                break;

            case E_JUDGE.Perfect:
                Flamecall.PerfectCall();
                break;               
        }
       
        //print(this + "の" + _eJudge + "処理が実行されました。\n");
    }

}
