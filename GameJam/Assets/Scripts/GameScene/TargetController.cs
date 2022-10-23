using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : ReferenceJudgement
{
    private S_BAR   m_sHitTiming;   // ヒットのタイミング
    private int     m_iIndex;       // Targetの生成インデックス
    private bool    m_isOnce;

    private Animator Enanim;
    [SerializeField]
    private TargetMove TargetMove;
    [SerializeField]
    private ParticleSystem blood;
    private void Start()
    {
        AddList();
        Enanim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 判定タイミングを過ぎたら無効にする
        if (PassHitTiming())
        {
            GameDirector.GetInstance.ResetCombo();
            DoDisableJudgement();
        }
        // 画面外に出たらオブジェクト削除
        if (TargetMove.IsOffScreen())
        {
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// ヒットのタイミングのゲッターセッター
    /// </summary>
    public S_BAR GetSetHitTiming
    {
        get { return m_sHitTiming; }
        set { m_sHitTiming = value; }
    }

    /// <summary>
    /// Targetの生成インデックスのゲッターセッター
    /// </summary>
    public int GetSetIndex
    {
        get { return m_iIndex; }
        set { m_iIndex = value; }
    }

    /// <summary>
    /// 判定を有効にする
    /// </summary>
    public void SetOnJudge()
    {
        base.SetOnJudge(true);
    }


    public override void SetEvent(E_JUDGE _eJudge)
    {
        // 判定結果に合わせた処理を実行
        switch (_eJudge)
        {
            case E_JUDGE.None:
                break;

            case E_JUDGE.Bad:
                break;

            case E_JUDGE.Good:
                //break;

            case E_JUDGE.Great:
                //break;

            case E_JUDGE.Perfect:
                Enanim.SetTrigger("DeadTrigger");
                blood.Play();
                TargetMove.enabled = false;
                StartCoroutine("waitdeath");
                break;
        }

        if (_eJudge != E_JUDGE.None)
        {
            DoDisableJudgement();
        }
        //print(this + "の" + _eJudge + "処理が実行されました。\n");
    }

    private IEnumerator waitdeath()                                  
    {       
            yield return new WaitForSeconds(0.35f);
            Destroy(this.gameObject);
    }

    /// <summary>
    /// ヒットのタイミングを通り過ぎたら真を返す
    /// </summary>
    private bool PassHitTiming()
    {
        // 判定が無効ならばスルー判定を行わない
        if (!OnJudge) return false;
        
        // 自身のヒットの拍になったらスルー判定を始める
        if (!m_isOnce)
        {
            S_BAR sCurrentBar = GameDirector.GetInstance.GetCurrentBar;
            if (sCurrentBar.iBar == m_sHitTiming.iBar && sCurrentBar.iBeat == m_sHitTiming.iBeat)
            {
                m_isOnce = true;
            }
            else
            {
                return false;
            }
        }
        // スルー判定
        if (GameDirector.GetInstance.JudgeTiming(m_sHitTiming) == E_JUDGE.None)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ヒット判定を無効にする
    /// </summary>
    private void DoDisableJudgement()
    {
        SetOnJudge(false);
        RemoveList();
        GameDirector.GetInstance.RemoveTargetList();
    }
}
