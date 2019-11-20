using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        Invoke("OnShow", 12.0f);
    }

    void OnShow()
    {
        gameObject.SetActive(true);
    }
}
