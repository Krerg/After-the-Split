using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTrigger : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;

    public string itemName;

    private TriggerManager triggerManager;

    public string useHint;

    private bool wasHintShown = false;

    public float typingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        triggerManager = GameObject.Find("TriggerManager").GetComponent<TriggerManager>();
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
            
            triggerManager.setCurrentObjectToAction(itemName);
            if (!wasHintShown && triggerManager.getCanUseItem())
            {
                textDisplay.text = "";
                StartCoroutine(Type());
                wasHintShown = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textDisplay.text = "";
            triggerManager.resetCurrentObjectToAction();
        }
    }

}
