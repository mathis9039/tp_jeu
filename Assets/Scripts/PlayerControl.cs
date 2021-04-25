using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static int speed = 40;
    private AudioSource _audio;
    [SerializeField] private AudioClip carCrash;
    private bool isCrashed;

    public void Start()
    {
        isCrashed = false;
        _audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!isCrashed)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.gameObject);
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("collision");
            _audio.PlayOneShot(carCrash, 100);
            isCrashed = true;
            StartCoroutine(Abc());
        }
        if (other.gameObject.CompareTag("END"))
        {
            Debug.Log("END LEVEL");
            StartCoroutine(EndLevel());
        }
        if (other.gameObject.CompareTag("FINISH"))
        {
            Debug.Log("END GAME");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator Abc()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Menu");
    }
    
    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("SecondGame");
    }

        IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Menu");
    }
}