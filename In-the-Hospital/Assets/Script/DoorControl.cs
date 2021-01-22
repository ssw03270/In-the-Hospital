using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip openAudio;

    private bool isDoorEnter = false;
    private bool isOpen = false;
    private bool isAudio = false;

    private float doorOpenAngle = -90.0f; //Set either positive or negative number to open the door inwards or outwards
    private float openSpeed = 2.0f; //Increasing this value will make the door open faster

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;
    float waitTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlDoor();
    }
    void ControlDoor()
    {
        if (isOpen)
        {
            if (isAudio)
            {
                audioPlayer.PlayOneShot(openAudio, 0.7F);
                isAudio = false;
            }
            if (waitTime < 1)
            {
                waitTime += Time.deltaTime;
            }
            if (openTime < 1 && waitTime >= 1)
            {
                openTime += Time.deltaTime * openSpeed;
            }
        }
        else
        {
            if (openTime < 1)
            {
                openTime += Time.deltaTime * openSpeed;
            }
            else if(isAudio)
            {
                audioPlayer.PlayOneShot(openAudio, 0.7F);
                isAudio = false;
            }
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (isOpen ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && isDoorEnter)
        {
            isAudio = true;
            isOpen = !isOpen;
            currentRotationAngle = transform.localEulerAngles.y;
            waitTime = 0;
            openTime = 0;
        }
    }
    private void OnGUI()
    {
        if (isDoorEnter)
        {
            string ment = "";
            if (!isOpen)
            {
                ment = "Press 'F' to open the door";
            }
            else
            {
                ment = "Press 'F' to close the door";
            }
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 200, 30), ment);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isDoorEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isDoorEnter = false;
        }
    }
}
