using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float camera_speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
    }
    
    void Turn()
    {
        float rotV = camera_speed * Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-1 * rotV, 0f, 0f);
    }
}
