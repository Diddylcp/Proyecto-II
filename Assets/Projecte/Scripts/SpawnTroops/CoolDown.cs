    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{

    bool isActive;
    float timePerCent;
    public float timeCD;
    public float cdTimeCounter; 

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        cdTimeCounter = timeCD;
  
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            cdTimeCounter -= Time.deltaTime;
            if (cdTimeCounter <= 0)
            {
                cdTimeCounter = timeCD;
                isActive = false;
                Debug.Log("YouCAN!!");
            }

            FillCDHud();
        }

        
    }
    void FillCDHud()
    {
        timePerCent = cdTimeCounter / timeCD * 100;
            //aasda


    }

    public void SetIsActive(bool a)
    {
        isActive = a;
    }
    public bool GetIsActive()
    {
        return isActive;
    }


}

