using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSpeed : MonoBehaviour
{
    [SerializeField] Animator Animator;

    private const float m_fDEFAULT_BPM = 60;
    private float m_fBPM;
    float speed = 0.41f; //デフォルト値
    // Start is called before the first frame update
    void Start()
    {
        m_fBPM = GameDirector.GetInstance.GetBPM;
        speed *= (m_fBPM / m_fDEFAULT_BPM);
        Debug.Log(speed);
        Animator.SetFloat("Speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
