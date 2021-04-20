using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static int speed = 5;

    public void Start()
    {
    }

    public void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        }
    }
    void OnTreggerEnter(Collider col)
    {
        
    }
}