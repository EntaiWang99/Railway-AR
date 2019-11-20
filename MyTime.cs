using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MyTime : MonoBehaviour
{
    public GUISkin MyGUISkin;
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
        GUI.skin = MyGUISkin;
        DateTime NowTime = DateTime.Now.ToLocalTime();
        //GUI.TextArea(new Rect(3, 3, 200, 20), NowTime.ToString("yyyy - MM - dd HH: mm:ss"), "Button");
        GUI.Label(new Rect(3, 3, 250, 40), NowTime.ToString("yyyy - MM - dd HH: mm:ss"), "Button");

        //GUI.Box(new Rect(Screen.width - 500, 0, 500, Screen.height / 2), "\n" + "     文字信息","Label");
        //GUI.Box(new Rect(Screen.width - 500, Screen.height / 2, 500, Screen.height / 2), "\n"+"     视频信息","Label");
    } 
}
