    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{

    bool isActive;
    float timePerCent;
    public float timeCD;
    public float cdTimeCounter;
    public Image imgCD;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
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
                cdTimeCounter = 0;
                isActive = false;
            }

            FillCDHud();
        }

        
    }
    void FillCDHud()
    {
        timePerCent = cdTimeCounter / timeCD ;
        imgCD.fillAmount = timePerCent;


    }

    public void SetIsActive(bool a)
    {
        cdTimeCounter = timeCD;
        isActive = a;
    }
    public bool GetIsActive()
    {
        return isActive;
    }


}

