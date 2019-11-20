using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_video : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }
}
