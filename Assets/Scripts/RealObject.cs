using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class RealObject : MonoBehaviour
{   
    //Другая версия обьекта
    public GameObject ghostObject;

    //Реален ли обьект
    [HideInInspector]
    public bool isReal = true;

    GhostObject ghost;
    BoxCollider2D coll;
    SpriteRenderer rend;
    Rigidbody2D rb;

    /// <summary>
    /// Меняем текущее состояние обьекта
    /// </summary>
    public void ChangeCurrentState()
    {
        print("Yup");
        coll = this.GetComponent<BoxCollider2D>();
        if (ghost.isStateChangePossible)
        {
            isReal = !isReal;
            rend = GetComponent<SpriteRenderer>();
            rend.enabled = !rend.enabled;
            rb = GetComponent<Rigidbody2D>();
            rb.simulated = !rb.simulated;
        }
        //Что делать, если невозможно сменить состояние
        else
        {
            Debug.LogError("Can't Time-travel, Object collides with Ghost");
        }     
    }

    private void Awake()
    {
        ghost = ghostObject.GetComponent<GhostObject>();
        ghost.realObject = this.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeCurrentState();
        }
    }
}
