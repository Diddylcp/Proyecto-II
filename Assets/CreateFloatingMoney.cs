using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloatingMoney : MonoBehaviour
{
   
   public GameObject floatingText;

    public void AddFloatingText(int money)
    {
        if (money != 0)
        {
            GameObject objInst = Instantiate(floatingText);
            objInst.transform.SetParent(gameObject.transform.parent);
           
        }
    }
    
}
