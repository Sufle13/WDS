using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSwitcher : MonoBehaviour
{
    
    [SerializeField] Transform bone;
    [SerializeField] float FanSpeedCurrent;
    [SerializeField] float FanSpeedLimit;
    [SerializeField] float HeistUp;
    [SerializeField] float HeistDown;
    [SerializeField] bool xRotation;
    [SerializeField] bool yRotation;
    [SerializeField] bool zRotation;
    [SerializeField] bool FanState = true;

    void VelocityUp(ref float VelocityCurrent) //v=v0+at
    {
        if (VelocityCurrent < FanSpeedLimit)
        {
            VelocityCurrent = VelocityCurrent + HeistUp * Time.deltaTime;
        }

    }

    void VelocityDown(ref float VelocityCurrent) //v=v0-at
    {
        if (VelocityCurrent > 0)
        {
            VelocityCurrent = VelocityCurrent - HeistDown * Time.deltaTime;
        }

    }

    
    void Start()
    {
        /*Bugaga*/
    }

    void Update()
    {

        if (FanState)
        {
            VelocityUp(ref FanSpeedCurrent);
            bone.transform.Rotate(Convert.ToInt32(xRotation) * FanSpeedCurrent * Time.deltaTime, Convert.ToInt32(yRotation) * FanSpeedCurrent * Time.deltaTime, Convert.ToInt32(zRotation) * FanSpeedCurrent * Time.deltaTime);
        }
        else
        {
            VelocityDown(ref FanSpeedCurrent);
            bone.transform.Rotate(Convert.ToInt32(xRotation) * FanSpeedCurrent * Time.deltaTime, Convert.ToInt32(yRotation) * FanSpeedCurrent * Time.deltaTime, Convert.ToInt32(zRotation) * FanSpeedCurrent * Time.deltaTime);
        }
        
    }
    
}