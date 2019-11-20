using UnityEngine;
using System.Collections;

public class Button1 : MonoBehaviour
{
    // 标志符，用于控制按钮文本
    public int flag = 0;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    // GUI函数
    void OnGUI()
    {
        // 隐藏按钮
        if (flag == 1)
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), "隐藏"))
            {
                flag++;
                flag %= 2;
            }
        }
        // 显示按钮
        else
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), "显示"))
            {
                flag++;
                flag %= 2;
            }
        }
        // 显示物体，但不影响按钮
        if (flag == 1)
        {
            transform.GetComponent<Renderer>().enabled = true;
        }
        // 隐藏物体，但不影响按钮
        else
        {
            transform.GetComponent<Renderer>().enabled = false;
        }
    }
}