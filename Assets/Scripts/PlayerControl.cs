using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static int speed = 10;
    public AudioSource _audio;
    public AudioClip carCrash;
    
    private float initialX;
    private float initialY;
    private float initialZ;

    public void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
        //carCrash = Resources.Load<AudioClip>("Car Crash Sound Effect");
    }

    public void Update()
    {
        //_audio.PlayOneShot(engineSound);
        transform.position += Vector3.forward * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !transform.position.x.Equals(-20))
        {
            transform.position = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !transform.position.x.Equals(-5))
        {
            transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.gameObject);
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("collision");
            _audio.PlayOneShot(carCrash, 1f);
           // transform.position = new Vector3(initialX, initialY, initialZ);
           SceneManager.LoadSceneAsync("Menu");
        }
    }
}