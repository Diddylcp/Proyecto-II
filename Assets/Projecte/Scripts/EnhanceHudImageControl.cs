using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnhanceHudImageControl : MonoBehaviour
{
    [SerializeField] private Sprite buttonHoverImage;
    [SerializeField] private Sprite buttonNormalImage;

    private Image imageEnhance;
    // Start is called before the first frame update
    void Start()
    {
        imageEnhance = this.GetComponent<Image>();
    }

    // Update is called once per frame
   public void ImageToHover()
    {
        imageEnhance.sprite = buttonHoverImage;
    }
    public void ImageToNormal()
    {
        imageEnhance.sprite = buttonNormalImage;
    }
}
