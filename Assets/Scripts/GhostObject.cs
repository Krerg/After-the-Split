using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostObject : MonoBehaviour
{

    private DimensionSwitcher ds;

    [HideInInspector]
    public Collider2D coll;

    public TextMeshProUGUI textDisplay;

    public float typingSpeed = 0.03f;

    private Coroutine curAnim;

    public string useHint = "Вы можете переместиться во времени при помощи кнопки W";

    // Start is called before the first frame update
    void Start()
    {
        ds = GameObject.Find("DimensionSwitcher").GetComponent<DimensionSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Type()
    {
        foreach (char letter in useHint.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            textDisplay.text = "";
            curAnim = StartCoroutine(Type());
            ds.EnableSwitch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(curAnim);
            textDisplay.text = "";
            ds.DisableSwitch();
        }
    }

}
