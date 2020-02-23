using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TextChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color pressedColor;
    [SerializeField] string defaultText;
    [SerializeField] string highlightedText;


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<TextMeshProUGUI>().color = highlightedColor;
        this.GetComponent<TextMeshProUGUI>().text = highlightedText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<TextMeshProUGUI>().color = defaultColor;
        this.GetComponent<TextMeshProUGUI>().text = defaultText;
    }

    public void PressButton()
    {
        this.GetComponent<TextMeshProUGUI>().color = pressedColor;

    }
}
