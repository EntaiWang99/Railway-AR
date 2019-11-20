using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Videoplay : MonoBehaviour
{
    MovieTexture movie;
    // Start is called before the first frame update
    void Start()
    {
        //movie = (MovieTexture)GetComponent<RawImage>().mainTexture;
        //movie.Play();
        //Invoke("Movie_start", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void Movie_start()
    //{
    //    movie.Play();
    //}
    public void OnPlay()
    {
        movie.Play();
    }

    public void OnStop()
    {
        movie.Pause();
    }

}
