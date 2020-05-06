using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{

    public string itemName;

    private TriggerManager triggerManager;

    // Start is called before the first frame update
    void Start()
    {
        triggerManager = GameObject.Find("TriggerManager").GetComponent<TriggerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            triggerManager.setCurrentObjectToAction(itemName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerManager.resetCurrentObjectToAction();
        }
    }

}
