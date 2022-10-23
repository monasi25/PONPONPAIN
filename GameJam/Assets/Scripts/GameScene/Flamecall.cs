using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flamecall : MonoBehaviour
{

    [SerializeField]
    private GameObject flame;
    [SerializeField] private GameObject Perfect;
    [SerializeField] private GameObject Great;
    [SerializeField] private GameObject Good;
    [SerializeField] private GameObject Bad;
    [SerializeField] private GameObject ComboObj;
    private GameObject Obj;
    private GameObject Obj1;
    private GameObject Obj2;
    private GameObject Obj3;
    private GameObject Obj4;
    private GameObject Obj5;
    private GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void BoxFlame()            //この関数を呼び出すとボックスフレーム生成
    {
        Obj = Instantiate(flame);
        Obj.transform.SetParent(canvas.transform, false);
    }
    public void PerfectCall()
    {
        Obj1 = Instantiate(Perfect);
        Obj1.transform.SetParent(canvas.transform, false);
    }

    public void GreatCall()
    {
        Obj2 = Instantiate(Great);
        Obj2.transform.SetParent(canvas.transform, false);
    }

    public void GoodCall()
    {
        Obj3 = Instantiate(Good);
        Obj3.transform.SetParent(canvas.transform, false);
    }

    public void BadCall()
    {
        Obj4 = Instantiate(Bad);
        Obj4.transform.SetParent(canvas.transform, false);
    }

    public void Combo(int n)
    {
        Obj5 = Instantiate(ComboObj);
        Obj5.transform.SetParent(canvas.transform, false);
        Obj5.GetComponent<Text>().text = n.ToString() + " Combo";
    }
}
