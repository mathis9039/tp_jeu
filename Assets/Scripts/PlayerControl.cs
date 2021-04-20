using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static int speed = 10;

    public void Start()
    {
    }

    public void Update()
    {
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
            transform.position = new Vector3(-15, 1.1f, -12.4f);
        }
    }
}