using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderSwicher : MonoBehaviour
{
    [SerializeField] GameObject mainSwicher;
    [SerializeField] bool lightState;
    [SerializeField] bool materialState;
    [SerializeField] bool animationState;
    void Update()
    {
        if(gameObject.GetComponent<AnimationSwicher>())
        {
            if (animationState && mainSwicher.GetComponent<MainSwicher>().fullState)
            {
                foreach (var animationSwicher in gameObject.GetComponents<AnimationSwicher>())
                {
                    animationSwicher.AnimationChange(true);
                }
            }
            else
            {
                foreach (var animationSwicher in gameObject.GetComponents<AnimationSwicher>())
                {
                    animationSwicher.AnimationChange(false);
                }
            }
        }
        
        if(gameObject.GetComponent<LightSwicher>())
        {
            if (lightState && mainSwicher.GetComponent<MainSwicher>().fullState)
            {
                foreach (var lightSwicher in gameObject.GetComponents<LightSwicher>())
                {
                    lightSwicher.LightChange(true);
                }
            }
            else
            {
                foreach (var lightSwicher in gameObject.GetComponents<LightSwicher>())
                {
                    lightSwicher.LightChange(false);
                }
            }
        }
        
        if(gameObject.GetComponent<MaterialSwicher>())
        {
            if (materialState && mainSwicher.GetComponent<MainSwicher>().fullState)
            {
                foreach (var materialSwicher in gameObject.GetComponents<MaterialSwicher>())
                {
                    materialSwicher.MaterialChange(true);
                }
            }
            else
            {
                foreach (var materialSwicher in gameObject.GetComponents<MaterialSwicher>())
                {
                    materialSwicher.MaterialChange(false);
                }
            }
        }
        
    }
}
