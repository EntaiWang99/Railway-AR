using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myzhuanxiangjia : MonoBehaviour
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
        GUI.Box(new Rect(Screen.width - 250, 3, 100, 30), "维修信息");
        result1 = "  类型：转向架";
        result2 = "  维修次数：2次";
        result3 = "  维修记录："+"\n"+
                  "  2013年1月1日  王恩泰  轮轴维修"+"\n"+
                  "  2015年10月1日  王恩泰  弹簧检修";
        result = result1 + "\n" + result2 + "\n" + result3;
        GUI.TextArea(new Rect(Screen.width - 250, 30, 250, 100), result);

    }
}
