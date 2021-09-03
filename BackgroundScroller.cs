using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeedBackground = 0.01f;

    Material myMaterial;
    Vector2 offset; 

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, scrollingSpeedBackground);
    }

    void Update()
    {
        myMaterial.mainTextureOffset = myMaterial.mainTextureOffset + offset * Time.deltaTime;
    }

}
