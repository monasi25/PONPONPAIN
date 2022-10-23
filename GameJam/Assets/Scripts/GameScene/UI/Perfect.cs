using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perfect : MonoBehaviour
{
    private float i = 0f;  
    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine("Anime1");
    }

    private IEnumerator Anime1()
    {
        for (i = 1f; i >= 0f; i -= 0.01f) //Scaleをゆっくり小さくしていってアニメーション風にしてます
        {
            this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(i, i, i);            
            yield return new WaitForSeconds(0.004f);   //ここの数値を小さくしたら収束速度が上昇
        }

        Destroy(this.gameObject);
    }
}
