using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static int speed = 10;
    private AudioSource _audio;
    [SerializeField] private AudioClip engineSound;
    private float initialX;
    private float initialY;
    private float initialZ;

    public void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    public void Update()
    {
        _audio.PlayOneShot(engineSound);
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
            transform.position = new Vector3(initialX, initialY, initialZ);
        }
    }
}