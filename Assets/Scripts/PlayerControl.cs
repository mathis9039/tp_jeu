﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static int speed;
    private AudioSource _audio;
    [SerializeField] private AudioClip carCrash;
    private bool isCrashed;

    public void Start()
    {
        isCrashed = false;
        _audio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            speed = 15;
        }
        else
        {
            speed = 25;
        }
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

            StartCoroutine(Crash());
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

    IEnumerator Crash()
    {
        yield return new WaitForSeconds(0.15f);
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        else
        {
            SceneManager.LoadSceneAsync("Level2");
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Level2");
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Menu");
    }
}