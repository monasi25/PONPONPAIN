using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_gTargetPrefab;

    private const int m_iHitTiming = 1; // 生成後、1小節先でヒットタイミングとなる

    private int m_iCurrentIndex = 0;

    [SerializeField] private Flamecall Flamecall;
    // Targetを生成するタイミングを格納した配列
    private S_BAR[] m_iRhythms =
    {
        new S_BAR(3, 1),
        new S_BAR(3, 2),
        new S_BAR(3, 3),
        new S_BAR(5, 1),
        new S_BAR(5, 2),
        new S_BAR(5, 3),
        new S_BAR(7, 1),
        new S_BAR(7, 3),
        new S_BAR(7, 4),
        new S_BAR(9, 1),
        new S_BAR(9, 2),
        new S_BAR(9, 4),
        new S_BAR(11, 1),
        new S_BAR(11, 2),
        new S_BAR(11, 3),
        new S_BAR(11, 4),
        new S_BAR(13, 2),
        new S_BAR(13, 4),
        new S_BAR(15, 1),
        new S_BAR(15, 4),
        new S_BAR(17, 1),
        new S_BAR(17, 2),
        new S_BAR(17, 3),
        new S_BAR(19, 1),
        new S_BAR(19, 2),
        new S_BAR(19, 3),
        new S_BAR(19, 4),
        new S_BAR(21, 1),
        new S_BAR(21, 2),
        new S_BAR(21, 3),
        new S_BAR(21, 4),
        new S_BAR(23, 1),
        new S_BAR(23, 4),
        new S_BAR(25, 1),
        new S_BAR(25, 2),
        new S_BAR(25, 4),
        new S_BAR(27, 1),
        new S_BAR(27, 3),
        new S_BAR(27, 4),
        new S_BAR(29, 1),
        new S_BAR(29, 2),
        new S_BAR(29, 3),
        new S_BAR(31, 1),
        new S_BAR(31, 2),
        new S_BAR(31, 3),
        new S_BAR(31, 4),
        new S_BAR(33, 1),
        new S_BAR(33, 2),
        new S_BAR(33, 3),
        new S_BAR(33, 4),
    };

    private void Update()
    {
        if (m_iCurrentIndex < m_iRhythms.Length)
        {
            GenerateTargetPrefab();
        }
    }

    /// <summary>
    /// TargetのPrefabを生成する
    /// </summary>
    private void GenerateTargetPrefab()
    {
        // 現在の小節と拍数がTargetの生成タイミングになったらTargetを生成
        S_BAR sCurrentBar = GameDirector.GetInstance.GetCurrentBar;
        if (sCurrentBar.iBar == m_iRhythms[m_iCurrentIndex].iBar && sCurrentBar.iBeat == m_iRhythms[m_iCurrentIndex].iBeat)
        {
            Flamecall.BoxFlame();
            // TargetPrefabを生成してリストの末尾に追加
            GameObject gTarget = Instantiate(m_gTargetPrefab);
            TargetController targetController = gTarget.GetComponent<TargetController>();
            GameDirector.GetInstance.AddTargetList(targetController);

            // 生成したTargetPrefabのヒットのタイミングと生成インデックスを設定
            S_BAR sHitTiming = new S_BAR(m_iRhythms[m_iCurrentIndex].iBar + m_iHitTiming, m_iRhythms[m_iCurrentIndex].iBeat);
            targetController.GetSetHitTiming = sHitTiming;
            targetController.GetSetIndex = m_iCurrentIndex;

            //print(m_iRhythms[m_iCurrentIndex].iBar + "小節 " + m_iRhythms[m_iCurrentIndex].iBeat + "拍目にTargetを生成しました。\n");
            m_iCurrentIndex++;
        }
    }
}
