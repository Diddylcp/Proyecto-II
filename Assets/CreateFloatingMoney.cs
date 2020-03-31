using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloatingMoney : MonoBehaviour
{
   
   public MoneyText floatingText;
   public TowerScript tower;
    string moneyToGive;

    public void AddFloatingText(int money)
    {
        if (money != 0)
        {
            moneyToGive = "+" + tower.stats.moneyPerSecond ;
            MoneyText objInst = Instantiate(floatingText);
            objInst.transform.SetParent(gameObject.transform.parent);
            objInst.textMoney.text = moneyToGive;


        }
    }
    
}
