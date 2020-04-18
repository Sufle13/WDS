using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    [SerializeField] GameObject bottomBlink;
    [SerializeField] string tagObject = "Player";
    private void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals(tagObject))
        {
            bottomBlink.active = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.tag.Equals(tagObject))
        {
            bottomBlink.active = false;
        }
    }
    private void OnTriggerStay(Collider player)
    {
        if (player.tag.Equals(tagObject))
        {
            if(!gameObject.GetComponent<AudioSource>().isPlaying)
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<UnderSwicher>().Swich();
            }
        }
    }
}
