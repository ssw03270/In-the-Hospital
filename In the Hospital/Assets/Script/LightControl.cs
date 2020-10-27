using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    float duration = 1f;
    float originalRange;
    float warpTimer = 0f;
    float sponeTimer = 0f;

    public static bool isSpone = false;


    Light lt;

    // Start is called before the first frame update
    void Start()
    {
        isSpone = false;
        lt = GetComponent<Light>();
        originalRange = lt.range;
        lt.range = 0;
    }

    // Update is called once per frame
    void Update()
    {
        WarpLight();
        SponeLight();
    }
    void WarpLight()
    {
        if (PlayerControl.isWarp)
        {
            warpTimer += Time.deltaTime;
            LampControl.warpTimer = warpTimer;

            duration = 3 / warpTimer;
            var amplitude = Mathf.PingPong(Time.time, duration);

            // Transform from 0..duration to 0.5..1 range.
            amplitude = amplitude / duration * 0.5f + 0.5f;

            // Set light range.
            lt.range = originalRange * amplitude;
        }
        if (warpTimer > 2)
        {
            lt.range = 0;
        }
        if (warpTimer > 3)
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
    void SponeLight()
    {

        if (lt.range < originalRange && !isSpone)
        {
            sponeTimer += Time.deltaTime * 2;
            lt.range = sponeTimer;
            LampControl.sponeTimer = sponeTimer;
        }
        else
        {
            isSpone = true;
        }

    }
}
