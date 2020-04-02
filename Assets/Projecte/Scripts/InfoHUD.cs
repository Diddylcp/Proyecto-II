using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoHUD : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<CanvasRenderer>().SetAlpha(0f);
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverMageEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Magic Tower \n\n +300 health \n +10 damage \n +5 money per second \n Your magic soldiers will make \n damage in area \n ";
    }

    public void OnHoverWarriorEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Warrior Tower \n\n +500 health \n +5 money per second \n Your warrior soldiers will return \n 25% of the damage taken \n";
    }
    public void OnHoverArcherEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Archer Tower \n\n +300 health \n +0.5 dps \n +4 Attack Range \n +5 money per second \n";
    }
    public void OnHoverVelocityEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Velocity Tower \n\n +300 health \n +0.5 dps  \n +5 money per second \n +10% of velocity on troops.\n";
    }
    public void OnHoverEconomyEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Economy Tower \n\n +300 health \n +20 coins per second \n";
    }
    public void OnLeftMouse()
    {
        text.GetComponent<CanvasRenderer>().SetAlpha(0f);
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
}
