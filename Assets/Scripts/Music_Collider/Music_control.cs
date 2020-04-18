using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_control : MonoBehaviour
{
    [SerializeField] AudioSource[] baseMusic;
    [SerializeField] AudioSource[] newMusic;
    [SerializeField] float multiply = 1f;
    [SerializeField] float standartVolumeBaseMusic =1f;
    [SerializeField] float standartVolumeNewMusic = 0f; 
    private void OnTriggerStay(Collider player)
    {
        foreach(var music in baseMusic)
        {
            music.volume = standartVolumeBaseMusic - gameObject.GetComponent<Distance>().GetDistancePercentage() * multiply;
        }
        foreach (var music in newMusic)
        {
            music.volume = standartVolumeNewMusic + gameObject.GetComponent<Distance>().GetDistancePercentage() * multiply;
        }
    }
    private void OnTriggerExit(Collider player)
    {
        foreach (var music in baseMusic)
        {
            music.volume = standartVolumeBaseMusic;
        }
        foreach (var music in newMusic)
        {
            music.volume = standartVolumeNewMusic;
        }
    }
}
