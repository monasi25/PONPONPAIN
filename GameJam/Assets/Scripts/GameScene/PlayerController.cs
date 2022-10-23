using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ReferenceJudgement
{
    [SerializeField] private ParticleSystem flash;

    private Animator Planim;

    private void Start()
    {
        AddList();
        SetOnJudge(true);

        Planim = this.GetComponent<Animator>();
    }

    public override void SetEvent(E_JUDGE _eJudge)
    {
        flash.Play();
        //Planim.SetTrigger("syagekiTrigger");

        // 判定結果に合わせた処理を実行
        switch (_eJudge)
        {
            case E_JUDGE.None:
                break;

            case E_JUDGE.Bad:
                Planim.SetTrigger("missTriger");
                break;

            case E_JUDGE.Good:
                break;

            case E_JUDGE.Great:
                break;

            case E_JUDGE.Perfect:
                break;
        }
        //print(this + "の" + _eJudge + "処理が実行されました。\n");
    }
}
