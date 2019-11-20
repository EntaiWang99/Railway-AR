using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UITips : MonoBehaviour
{
    DateTime t1, t2;
    int i = 1;
    public static Vector3 vec3, pos;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PointerDown()
    {
        vec3 = Input.mousePosition;//获取当前鼠标的位置
        pos = transform.GetComponent<RectTransform>().position;//获取自己所在的位置
    }

    /// <summary>
    /// 鼠标拖拽时候会被触发的事件
    /// </summary>
    public void Drag()
    {
        Vector3 off = Input.mousePosition - vec3;
        //此处Input.mousePosition指鼠标拖拽结束的新位置
        //减去刚才在按下时的位置，刚好就是鼠标拖拽的偏移量
        vec3 = Input.mousePosition;//刷新下鼠标拖拽结束的新位置，用于下次拖拽的计算
        pos = pos + off;//原来image所在的位置自然是要被偏移
        transform.GetComponent<RectTransform>().position = pos;//直接将自己刷新到新坐标
    }

    /// <summary>
    /// 此函数接口将赋予给“弹出对话框”按钮的onClick事件
    /// </summary>
    public void onShow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 此函数接口将赋予给“确认”按钮的onClick事件
    /// </summary>
    public void ondestroy()
    {
        //gameObject.SetActive(false);

        t2 = DateTime.Now;
        if (t2 - t1 < new TimeSpan(0, 0, 0, 0, 300))
        {
            //Debug.Log("哎呀竟然双击了好棒~~~~");
            //DestroyImmediate(this.gameObject);
            gameObject.SetActive(false);
        }
        t1 = t2;

    }
}
