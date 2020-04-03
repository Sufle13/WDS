using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwicher : MonoBehaviour
{
    [SerializeField] GameObject light = null;
    [SerializeField] float lightOff = 0f;
    [SerializeField] float lightOn = 0f;
    [SerializeField] bool lightState = false;

    public void LightChange(bool lightState)
    {
        if (light != null)
        {
            if (lightState)
            {
                light.GetComponent<Light>().intensity = lightOn * 0.08f;
                this.lightState = true;
            }
            else
            {
                light.GetComponent<Light>().intensity = lightOff;
                this.lightState = false;
            }
        }

    }
}
