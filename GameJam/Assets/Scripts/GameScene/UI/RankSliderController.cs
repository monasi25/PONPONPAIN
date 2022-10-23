using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSliderController : ReferenceComboController
{
    private enum E_RANK_VALUE
    {
        A = 15,
        B = 10,
        C = 5,
        D = 0,
    }

    private const float m_fMAX_VALUE = 20;
    private Slider m_slider;

    private void Start()
    {
        AddList();
        m_slider = GetComponent<Slider>();
        m_slider.maxValue = m_fMAX_VALUE;
    }

    /// <summary>
    /// スライダー増加
    /// </summary>
    public override void DoAddProcess()
    {
        m_slider.value++;
    }

    /// <summary>
    /// スライダー減少
    /// </summary>
    public override void DoResetProcess()
    {
        switch(GameDirector.GetInstance.GetCurrentRank)
        {
            case E_RANK.A:
                m_slider.value = (int)E_RANK_VALUE.B;
                break;
            case E_RANK.B:
                m_slider.value = (int)E_RANK_VALUE.C;
                break;
            case E_RANK.C:
                m_slider.value = (int)E_RANK_VALUE.D;
                break;
            case E_RANK.D:
                m_slider.value = (int)E_RANK_VALUE.D;
                break;
        }
    }
}
