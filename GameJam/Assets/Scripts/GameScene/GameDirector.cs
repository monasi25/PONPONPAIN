using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private enum E_GAME_STATE
    {
        BeforePlaying,
        Playing,
        AfterPlaying,
    }

    private const float m_fBPM = 160; // Beat Per Minute
    private const float m_fMUSIC_START_TIME = 2.0f;
    
    private static GameDirector ms_instance; // シングルトン
    private static E_RANK       m_eFinalRank = E_RANK.D; // 最終ランク（リザルトから取得）

    private RhythmManager       m_rhythmManager;
    private Judgement           m_judgement;
    private ComboController     m_comboController;
    private RankController      m_rankController;
    private List<TargetController> m_targetList = new List<TargetController>(); // Targetのリスト
    
    private bool            m_isPlayingMusic    = false;
    private E_GAME_STATE    m_eCurrentGameState = E_GAME_STATE.BeforePlaying;

    public float GetBPM => m_fBPM;
    public E_RANK GetCurrentRank => m_rankController.GetCurrentRank;
    public int GetCurrentCombo => m_comboController.GetCurrentCombo;
    public S_BAR GetCurrentBar => m_rhythmManager.GetCurrentBar();
    public E_RANK GetFinalRank => m_eFinalRank;

    private void Awake()
    {
        if (ms_instance == null) ms_instance = this; // インスタンス生成
        
        m_rhythmManager     = GetComponent<RhythmManager>();
        m_judgement         = GetComponent<Judgement>();
        m_comboController   = GetComponent<ComboController>();
        m_rankController    = GetComponent<RankController>();

        StartCoroutine("PlayMusic");
    }

    private void Update()
    {
        switch(m_eCurrentGameState)
        {
            case E_GAME_STATE.BeforePlaying:
                break;

            case E_GAME_STATE.Playing:
                // プレイヤーのキー入力に合わせてタイミングを判定する
                if (Input.GetKeyDown(KeyCode.S))
                {
                    E_JUDGE eJudge = E_JUDGE.None; // Targetがいないときは空振りするように
                    if (0 < m_targetList.Count)
                    {
                        eJudge = JudgeTiming(m_targetList[0].GetSetHitTiming);
                    }
                    m_judgement.JudgeRhythm(eJudge);
                }
                break;

            case E_GAME_STATE.AfterPlaying:
                // ランクをセットしてリザルトシーンへ遷移
                m_eFinalRank = GetCurrentRank;
                SceneManager.LoadScene("Result");
                break;
        }
    }

    /// <summary>
    /// インスタンスを返す
    /// </summary>
    public static GameDirector GetInstance
    {
        get { return ms_instance; }
    }

    /// <summary>
    /// 現在音楽が再生されているか否か
    /// </summary>
    public bool GetSetIsPlayingMusic
    {
        get { return m_isPlayingMusic; }
        set
        { 
            m_isPlayingMusic = value;
            m_eCurrentGameState = E_GAME_STATE.AfterPlaying;
        }
    }

    /// <summary>
    /// 判定に影響を受けるクラスをリストに追加する
    /// </summary>
    public void AddReferenceJudgementList(ReferenceJudgement _a)
    {
        m_judgement.AddRefferenceJudgementList(_a);
    }

    /// <summary>
    /// 判定に影響を受けるクラスをリストから削除する
    /// </summary>
    public void RemoveReferenceJudgementList(ReferenceJudgement _a)
    {
        m_judgement.RemoveRefferenceJudgementList(_a);
    }

    /// <summary>
    /// コンボ数に影響を受けるクラスをリストに追加する
    /// </summary>
    /// <param name="_a"></param>
    public void AddReferenceComboList(ReferenceComboController _a)
    {
        m_comboController.AddReferenceComboList(_a);
    }

    /// <summary>
    /// Tergetリストの末尾に追加する
    /// </summary>
    public void AddTargetList(TargetController _a)
    {
        m_targetList.Add(_a);
        if (m_targetList.Count == 1)
        {
            _a.SetOnJudge(); // 要素がこれ1つなら判定をONにする
        }
    }

    /// <summary>
    /// Tergetリストの先頭を削除する
    /// </summary>
    public void RemoveTargetList()
    {
        m_targetList.RemoveAt(0);
        if (0 < m_targetList.Count)
        {
            m_targetList[0].SetOnJudge(); // 次の要素があれば判定をONにする
        }
    }

    /// <summary>
    /// リズム判定
    /// </summary>
    /// <param name="_a"></param>
    /// <returns></returns>
    public E_JUDGE JudgeTiming(S_BAR _a)
    {
        return m_rhythmManager.GetJudgement(_a);
    }

    /// <summary>
    /// コンボ数リセット
    /// </summary>
    public void ResetCombo()
    {
        m_comboController.ResetCombo();
    }

    /// <summary>
    /// 指定秒数後に音楽をスタートさせるコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(m_fMUSIC_START_TIME);
        m_isPlayingMusic = true;
        m_eCurrentGameState = E_GAME_STATE.Playing;
    }
}
