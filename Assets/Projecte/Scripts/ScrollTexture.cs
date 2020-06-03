using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] float scrollX = 0.5f;
    [SerializeField] float scrollY = 0.5f;

    float offsetX = 0;

    void Update()
    {
        offsetX += Time.deltaTime * 10;
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}
