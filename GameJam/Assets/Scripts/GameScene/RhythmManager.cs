using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 判定と判定幅
/// </summary>
public enum E_JUDGE
{
    None    = 0,    // 無効
    Perfect = 100,
    Great   = 200,
    Good    = 300,
    Bad     = 500,
}

/// <summary>
/// 小節と拍数
/// </summary>
public struct S_BAR
{
    public int iBar;   // 小節
    public int iBeat;  // 拍

    public S_BAR(int iBar, int iBeat)
    {
        this.iBar = iBar;
        this.iBeat = iBeat;
    }
}

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private bool m_onDebugRhythmCount = false; // デバッグ用、現在の小節と拍数を出力

    private const float m_fBPM  = 160;              // Beat Per Minute
    private const float m_fBPS  = m_fBPM / 60.0f;   // Beat Per Second
    private const float m_fSPB  = 60.0f / m_fBPM;   // Second Per Beat
    private const int   m_iBEAT = 4;                // 拍子

    private float   m_fTimeCount    = 0;                // 音楽スタートからの秒数
    private S_BAR   m_sCurrentBar   = new S_BAR(1, 1);  // 1小節1拍目

    private void Start()
    {
        if (m_onDebugRhythmCount) Debug.Log(m_sCurrentBar.iBar + "小節 " + m_sCurrentBar.iBeat + "拍目 : " + m_fTimeCount + "秒");
    }

    private void Update()
    {
        if (GameDirector.GetInstance.GetSetIsPlayingMusic)
        {
            CountRhythm();
        }
    }

    /// <summary>
    /// 現在の小節と拍数をカウントする
    /// </summary>
    private void CountRhythm()
    {
        m_fTimeCount += Time.deltaTime;
        int iLastBeat = m_sCurrentBar.iBeat;
        int iCurrentBeat = (int)(m_fTimeCount * m_fBPS) % m_iBEAT + 1;
        if (iLastBeat != iCurrentBeat)
        {
            m_sCurrentBar.iBeat = iCurrentBeat;
            if (iCurrentBeat < iLastBeat) m_sCurrentBar.iBar++;
            if (m_onDebugRhythmCount) print(m_sCurrentBar.iBar + "小節 " + m_sCurrentBar.iBeat + "拍目 : " + m_fTimeCount + "秒\n");
        }
        Debug.Assert(1 <= m_sCurrentBar.iBeat && m_sCurrentBar.iBeat <= m_iBEAT);
    }

    /// <summary>
    /// 現在の小節と拍数を返す
    /// </summary>
    /// <returns></returns>
    public S_BAR GetCurrentBar()
    {
        return m_sCurrentBar;
    }

    /// <summary>
    /// 引数との誤差を返す
    /// </summary>
    /// <param name="_sHitBar">ターゲットの小節と拍数</param>
    /// <returns>判定結果</returns>
    public E_JUDGE GetJudgement(S_BAR _sHitBar)
    {
        float fJustHitTime = (m_fSPB * m_iBEAT) * (_sHitBar.iBar - 1) + (m_fSPB * (_sHitBar.iBeat - 1));
        float fErrorMargin = Mathf.Abs((fJustHitTime - m_fTimeCount)) * 1000; //秒からミリ秒へ変換
        Debug.Assert(0 <= fErrorMargin);
        // 誤差によって判定結果を返す
        if (fErrorMargin <= (int)E_JUDGE.Perfect)
        {
            return E_JUDGE.Perfect;
        }
        else if (fErrorMargin <= (int)E_JUDGE.Great)
        {
            return E_JUDGE.Great;
        }
        else if (fErrorMargin <= (int)E_JUDGE.Good)
        {
            return E_JUDGE.Good;
        }
        else if (fErrorMargin <= (int)E_JUDGE.Bad)
        {
            return E_JUDGE.Bad;
        }
        else
        {
            return E_JUDGE.None;
        }
    }
}