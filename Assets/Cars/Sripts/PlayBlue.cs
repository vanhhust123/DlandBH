﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBlue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerMove.priorityofBlue == 0)
        {
            if (other.gameObject.tag == "Cut")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("AddScore");
                Destroy(other.gameObject);
                if (GameController.IsSoundOff == false)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                }
            }
            if (other.gameObject.tag == "Punch")
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Leaf")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver1");
            }
        }
        else if (PlayerMove.priorityofBlue == 1)
        {
            if (other.gameObject.tag == "Cut")
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Punch")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver1");
            }
            if (other.gameObject.tag == "Leaf")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("AddScore");
                Destroy(other.gameObject);
                if (GameController.IsSoundOff == false)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                }
            }
        }
        else if (PlayerMove.priorityofBlue == 2)
        {
            if (other.gameObject.tag == "Cut")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver1");
            }
            if (other.gameObject.tag == "Punch")
            {
                GameObject.FindGameObjectWithTag("GameController").SendMessage("AddScore");
                Destroy(other.gameObject);
                if (GameController.IsSoundOff == false)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                }
            }
            if (other.gameObject.tag == "Leaf")
            {
                Destroy(other.gameObject);
            }

        }
    }
}
