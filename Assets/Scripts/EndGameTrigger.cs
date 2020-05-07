using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameTrigger : MonoBehaviour
{
    public TextMeshProUGUI hint;
    private Camera camera;

    private string endWordToDevs = @"Огромное спасибо также программистам:
        Krerg (Aleksandr Mylnikov), Bionix, BeetleMaster.";

    public float typingSpeed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Type()
    {
        foreach (char letter in endWordToDevs.ToCharArray())
        {
            hint.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            camera.EndGame();
            hint.text = "";
            hint.text = endWordToDevs;
        }
    }
}
