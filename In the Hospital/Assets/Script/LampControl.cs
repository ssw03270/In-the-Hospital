using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampControl : MonoBehaviour
{
    public static float sponeTimer = 0f;
    public static float warpTimer = 0f;
    float duration = 1f;

    Color colorStart1 = Color.black;
    Color colorEnd1 = Color.white;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SponeLamp();
        WarpLamp();
    }

    void WarpLamp()
    {
        if (PlayerControl.isWarp)
        {
            var material = GetComponent<Renderer>().material;

            duration = 3 / warpTimer;
            var amplitude = Mathf.PingPong(Time.time, duration);
            amplitude = amplitude / duration * 0.5f + 0.5f;
            if (warpTimer > 2)
            {
                amplitude = 0;
            }
            material.SetColor("_EmissionColor", Color.Lerp(colorStart1, colorEnd1, amplitude));
        }
        

    }
    void SponeLamp()
    {
        if (!LightControl.isSpone)
        {
            var material = GetComponent<Renderer>().material;
            float lerp = sponeTimer / 7;
            material.SetColor("_EmissionColor", Color.Lerp(colorStart1, colorEnd1, lerp));
        }
        
    }
}
