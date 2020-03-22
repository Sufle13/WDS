using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperty : MonoBehaviour
{
    enum Item
    {
        Medicine,
        Weapons
    }

    [SerializeField] Item item; 
    public void Take()        //функция взятия предмета
    {
        switch(item)
        {
            case Item.Medicine:
                //взяли аптечку
                //Debug.Log("взяли аптечку");
                break;
            case Item.Weapons:

                break;
        }
        Destroy(gameObject);
    }
}
