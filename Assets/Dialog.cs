﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public bool timeB;
    public float timeF;

    void Start()
    {
        timeF = 10f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            textDisplay.text = "";
        }

        if (timeB)
        {
            timeF = timeF - Time.deltaTime;
            if (timeF <= 0)
            {
                textDisplay.text = "";
                timeB = false;
            }
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence(int X)
    {
        if(index < sentences.Length - 1)
        {
            index = X;
            textDisplay.text = "";
            StartCoroutine(Type());
            timeB = true;
            timeF = 10f;
        }
        else
        {
            textDisplay.text = "";
        }


    }

}
