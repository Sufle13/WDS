using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_control : MonoBehaviour
{
    [SerializeField] AudioSource[] baseMusic;
    [SerializeField] AudioSource[] newMusic;

    private void OnTriggerStay(Collider player)
    {
        foreach(var music in baseMusic)
        {
            music.volume = 1 - gameObject.GetComponent<Distance>().GetDistancePercentage();
        }
        foreach (var music in newMusic)
        {
            music.volume = gameObject.GetComponent<Distance>().GetDistancePercentage();
        }
    }
    private void OnTriggerExit(Collider player)
    {
        foreach (var music in baseMusic)
        {
            music.volume = 1f;
        }
        foreach (var music in newMusic)
        {
            music.volume = 0f;
        }
    }
}
