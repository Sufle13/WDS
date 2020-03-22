using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float intensityX = 1;
    [SerializeField] float intensityY = 1;
    private void F_HeadBob(float intensityX ,float intensityY)
    {
        transform.localPosition = new Vector3(Mathf.Cos(player.position.x + player.position.y + player.position.z) * intensityX,Mathf.Abs(Mathf.Sin(player.position.x + player.position.y + player.position.z) * intensityY) + 1, 0);
    }

    void Update()
    {
        F_HeadBob(intensityX, intensityY);
    }
}
