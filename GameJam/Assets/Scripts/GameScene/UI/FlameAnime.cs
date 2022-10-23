using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameAnime : MonoBehaviour
{
    private RectTransform rect;
    private Image Image;
    private float i = 0f;
    private float alpha = 0f;

    private const float m_fFINAL_SCALE = 0.8f;
    private const float m_fDEFAULT_BPM = 60;            // BPM60をデフォルトとする
    private const float m_fSCALING_SPEED = 0.25f;       // デフォルトで1秒間のスケーリング量
    private const float m_fOPACITY_SPEED = 0.3f;        // デフォルトで1秒間の不透明量
    private float       m_fBPM;                         // 実際のBPM
    private Vector3     m_vScalingSpeed = Vector3.one;  // スケーリング速度
    private float       m_fOpacitySpeed = 1;            // 不透明速度

    void Start()
    {
        Image = this.GetComponent<Image>();
        rect = this.GetComponent<RectTransform>();
        //StartCoroutine("convergence");

        // 初期alfa
        Image.color = new Color(255, 255, 255, 0);
        // BPMを取得してスケーリング速度と不透明速度を計算
        m_fBPM = GameDirector.GetInstance.GetBPM;
        m_vScalingSpeed *= m_fSCALING_SPEED * (m_fBPM / m_fDEFAULT_BPM);
        m_fOpacitySpeed *= m_fOPACITY_SPEED * (m_fBPM / m_fDEFAULT_BPM);
    }

    private void Update()
    {
        if (m_fFINAL_SCALE <= rect.localScale.x)
        {
            // Scaleを小さくする
            rect.localScale -= m_vScalingSpeed * Time.deltaTime;
            // alfa値を上げる
            Image.color += new Color(0, 0, 0, m_fOpacitySpeed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator convergence()                                  
    {
        for (i = 2f; i >= 0.8f; i -= 0.01f) //Scaleをゆっくり小さくしていってアニメーション風にしてます
        {            
            rect.localScale = new Vector3(i, i, i);
            Image.color = new Color(255, 255, 255, alpha);      //アルファ値を徐々にあげてます
            alpha += 0.02f;
            yield return new WaitForSeconds(0.009f);   //ここの数値を小さくしたら収束速度が上昇
        }

        Destroy(this.gameObject);
    }
}
