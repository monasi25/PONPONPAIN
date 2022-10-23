using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    [SerializeField] private float m_fMoveSpeed = 3; // デフォルトで1秒間に移動する量

    private const float m_fOFFSCREEN_POS_Z  = -30;  // 画面外位置
    private const float m_fDEFAULT_BPM      = 60;   // BPM60をデフォルトとする
    
    private float       m_fBPM;                         // 実際のBPM
    private Vector3     m_vMoveSpeed = Vector3.forward; // 移動速度
    private Rigidbody   m_rb;

    void Start()
    {
        // 実際のBPMをもとに移動速度を決定する
        m_fBPM = GameDirector.GetInstance.GetBPM;
        m_vMoveSpeed *= m_fMoveSpeed * (m_fBPM / m_fDEFAULT_BPM);

        m_rb = GetComponent<Rigidbody>();
    }
   
    private void FixedUpdate()
    {
        //m_rb.velocity = new Vector3(0f, 0f, 8.5f);
        m_rb.MovePosition(transform.position + transform.TransformDirection(m_vMoveSpeed) * Time.deltaTime);
    }

    /// <summary>
    /// 画面外に出たら真を返す
    /// </summary>
    /// <returns></returns>
    public bool IsOffScreen()
    {
        if (m_fOFFSCREEN_POS_Z <= transform.position.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
