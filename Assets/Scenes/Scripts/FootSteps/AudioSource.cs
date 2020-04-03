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
    //void Update()
    //{
    //    As.clip = stepClip[Random.Range(0, stepClip.Length)];
    //    As.Play();
    //}

    //AudioSource As;
    //AudioClip[] stepClip;
    void PlayStep(float volume)
    {
        //stepSource. = stepClip[ Random.Range(0, stepClip.Length) ];
        

    }
}
