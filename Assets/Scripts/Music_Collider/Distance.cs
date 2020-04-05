using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    //[SerializeField] AudioSource[] baseMusic;
    //[SerializeField] AudioSource[] newMusic;

    private float distancePercentage = 0f;
    private float distance = 0f;
    [SerializeField] float deviation = 0f;
    [SerializeField] string tagObject = "Player";


    private void OnTriggerEnter(Collider player)
    {
        if(player.tag.Equals(tagObject))
        {
            distance = (player.transform.position - gameObject.transform.position).magnitude;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.tag.Equals(tagObject))
        {
            distance = 0f;
        }
    }
    private void OnTriggerStay(Collider player)
    {
        if(player.tag.Equals(tagObject))
        {
            distancePercentage = 1 - (player.transform.position - gameObject.transform.position).magnitude / distance;
            distancePercentage = distancePercentage + deviation * distancePercentage;
        }
    }

    public float GetDistancePercentage()
    {
        return Mathf.Clamp(distancePercentage, 0f, 1f);
    }
}
