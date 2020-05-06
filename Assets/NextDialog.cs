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
    // Start is called before the first frame update
    void Start()
    {
        tom = true;
        ds = GameObject.Find("DimensionSwitcher").GetComponent<DimensionSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (showed == false)
        {
            if (col.gameObject.tag == "Player")
            {
                if (I_need_tom == ds.isTom())
                {
                    dialogManager.GetComponent<Dialog>().NextSentence(newIndex);
                    showed = true;
                }
            }
        }

    }
}

