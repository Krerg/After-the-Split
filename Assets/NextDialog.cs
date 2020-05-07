using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialog : MonoBehaviour
{
    public bool showed;
    public int newIndex;
    public GameObject dialogManager;
    public bool I_need_tom;
    public bool tom;
    private DimensionSwitcher ds;
    private TriggerManager triggerManager;
    // Start is called before the first frame update
    void Start()
    {
        tom = true;
        ds = GameObject.Find("DimensionSwitcher").GetComponent<DimensionSwitcher>();
        triggerManager = GameObject.Find("TriggerManager").GetComponent<TriggerManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (showed == false)
        {
            if (col.gameObject.tag == "Player")
            {
                if (I_need_tom == ds.isTom())
                {
                    if (newIndex == 3 && triggerManager.isSecondOpen == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if (newIndex == 4 && triggerManager.isSecondOpen == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if (newIndex == 4 && triggerManager.isSecondOpen == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if (newIndex == 6 && triggerManager.isSecondOpen == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if (newIndex == 7 && triggerManager.isKeyPut == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if (newIndex == 9 && triggerManager.isBoxUnlock == true)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                    else if(newIndex == 0 || newIndex == 1 || newIndex == 2 || newIndex == 5 || newIndex == 8 || newIndex == 10)
                    {
                        dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                        showed = true;
                    }
                }
            }
        }

    }
}

