using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private bool m_isPlayingMusic = false;
    [SerializeField] private AudioSource AudioSource;
    void Update()
    {
        if (!m_isPlayingMusic && GameDirector.GetInstance.GetSetIsPlayingMusic)
        {
            StartMusic();
        }

        else if (m_isPlayingMusic && AudioSource.time == 0.0f && !AudioSource.isPlaying)
        {
            FinishMusic();
        }

    }

    /// <summary>
    /// 音楽再生
    /// </summary>
    private void StartMusic()
    {
        AudioSource.Play();
        m_isPlayingMusic = true;
        Debug.Log("ミュージックスタート");
    }

    /// <summary>
    /// 音楽終了を知らせる
    /// </summary>
    private void FinishMusic()
    {
        GameDirector.GetInstance.GetSetIsPlayingMusic = false;
        m_isPlayingMusic = false;
        Debug.Log("おわり");
    }
}
