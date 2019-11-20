using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessage : MonoBehaviour
{
    public string result;
    public string result1;
    public string result2;
    public string result3;
    public GUISkin MyGUISkin3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.skin = MyGUISkin3;
        GUI.Box(new Rect(Screen.width - 250, 3, 100, 30), "列车信息");
        result1 = "   G60K罐车";
        result2 = " 车号：0872857";
        result3 = "  济南铁路局" + "\n" +
                  "     轻油" ;
        result = result1 + "\n" + result2 + "\n" + result3;
        GUI.TextArea(new Rect(Screen.width - 250, 30, 250, 100), result);

    }
}