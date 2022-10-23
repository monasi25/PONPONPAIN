using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resultsyori : MonoBehaviour
{
    [SerializeField] private GameObject GameObject;
    E_RANK eFinalRank;
    // Start is called before the first frame update
    void Start()
    {
        eFinalRank = GameDirector.GetInstance.GetFinalRank;
        Debug.Log("最終ランク : " + eFinalRank);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eFinalRank == E_RANK.A)
        {
            GameObject.GetComponent<Text>().text = "A";
        }

        if (eFinalRank == E_RANK.B)
        {
            GameObject.GetComponent<Text>().text = "B";
        }

        if (eFinalRank == E_RANK.C)
        {
            GameObject.GetComponent<Text>().text = "C";
        }

        if (eFinalRank == E_RANK.D)
        {
            GameObject.GetComponent<Text>().text = "D";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeManager.Instance.LoadScene("Title", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
