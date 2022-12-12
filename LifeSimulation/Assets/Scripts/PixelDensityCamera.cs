using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDensityCamera : MonoBehaviour
{
    public float pixelstoUnits=100;

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize=Screen.height/pixelstoUnits /2;
    }
}
