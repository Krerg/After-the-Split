using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI hint;
    public TextMeshProUGUI levelHint;
    public string[] sentences;
    public int index;
    public float typingSpeed;

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            textDisplay.text = "";
            hint.text = "";
            levelHint.text = "";
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

        }
        else
        {
            textDisplay.text = "";
        }


    }

}
