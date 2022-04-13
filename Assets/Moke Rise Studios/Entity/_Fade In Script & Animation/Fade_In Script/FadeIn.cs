using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    /*
		Attach this script to a GameObject with an AudioSource and enter a fade time. Easy Fade In will
		smoothly increase the audiosource's volume over this period of time until it reaches maximum
		volume, and then will destroy itself to prevent wasting a FixedUpdate() check.
	*/

    public float approxSecondsToFadeIn = 5.0f;

 

    AudioSource aud;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {

        //Controls Volume "0" is the lowest and " 1 " is the max. These values are FLOATS
        if (aud.volume < 1)
        {
            aud.volume = aud.volume + (Time.deltaTime / (approxSecondsToFadeIn + 1));
        }
        else
        {
            Destroy(this); //destroys so update doesnt waste resources checking for it every frame WILL DESTROY ITSELF AFTER FADE IN IS COMPLETE
        }
    }
}
