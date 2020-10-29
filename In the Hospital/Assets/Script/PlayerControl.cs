using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip walkAudio;

    private float moveSpeed = 2f;
    private float cameraSpeed = 2f;
    
    public static bool isWarp;

    // Start is called before the first frame update
    void Start()
    {
        isWarp = false;
        audioPlayer = GetComponent<AudioSource>();

        audioPlayer.clip = walkAudio;
        audioPlayer.Stop();
        audioPlayer.loop = true;
        audioPlayer.time = 0;
        audioPlayer.volume = 0.5F;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Audio();
    }
    void Audio()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if(!audioPlayer.isPlaying)
                audioPlayer.Play();
        }
        else
        {
            audioPlayer.Stop();
            audioPlayer.time = 0;
        }
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
    void Turn()
    {
        float rotH = cameraSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles += new Vector3(0f, rotH, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Warp")
        {
            isWarp = true;
        }
    }
}
