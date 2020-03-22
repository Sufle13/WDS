using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObj : MonoBehaviour
{
   public KeyCode take;
   void OnTriggerStay(Collider other)
   {
        if(other.GetComponent<ItemProperty>() != null)                                         //проверка содержит ли предмет какое либо свойтво(можно ли его поднять и т.д)
        {
          GuidanceReaction(true);
          if (Input.GetKeyDown(take)) other.GetComponent<ItemProperty>().Take();
        }
       
   }

    void GuidanceReaction(bool vis)       //реакция на то что игрок видит предмет перед собой
    {
        if(vis)
        {
            //Debug.Log("видит");
        }
        else
        {
            //Debug.Log("не видит");
        }
    }


}
