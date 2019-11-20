using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        Invoke("OnShow", 3.0f);
    }

    void OnShow()
    {
        gameObject.SetActive(true);
    }
}
