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
        text.text = "Magic Tower 600$\n\n +300 health \n +10 damage \n Your magic soldiers will make \n damage in area ";
    }

    public void OnHoverWarriorEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Warrior Tower 600$\n\n +500 health \n Your warrior soldiers will return \n 25% of the damage taken";
    }
    public void OnHoverArcherEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Archer Tower 600$\n\n +300 health \n Your warrior soldiers will return ";
    }
    public void OnHoverVelocityEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Velocity Tower 400$\n\n +300 health \n +0.5 dps \n +4 Attack Range ";
    }
    public void OnHoverEconomyEnhancement()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.GetComponent<CanvasRenderer>().SetAlpha(1f);
        text.text = "Economy Tower 450$\n\n +300 health \n +5 coins per second";
    }
    public void OnLeftMouse()
    {
        text.GetComponent<CanvasRenderer>().SetAlpha(0f);
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
}
