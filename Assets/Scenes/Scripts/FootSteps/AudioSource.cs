using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioSource : MonoBehaviour
{
    AudioSource stepSource;
    AudioClip[] stepClip;
    void Start()
    {
        stepSource = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        
    }

    void PlayStep(float volume)
    {
        //stepSource. = stepClip[ Random.Range(0, stepClip.Length) ];
        //stepSource.Play();
    }
}
