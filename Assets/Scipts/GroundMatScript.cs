using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMatScript : MonoBehaviour
{
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        rend.material.mainTextureOffset += new Vector2(0.1f * Time.deltaTime, 0f);
    }
}
