using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    //Возможна ли смена миров
    [HideInInspector]
    public bool isStateChangePossible = true;

    //Не-призрачный обьект
    public GameObject realObject;

    //Меняем позицию на позицию реального обьекта
    private void FixedUpdate()
    {
        this.transform.position = realObject.transform.position;

        if (realObject.GetComponent<RealObject>().isReal)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
    }

    //Настраиваеи обьект
    BoxCollider2D bc;
    private void Awake()
    {
        bc = this.gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
    }

    //Проверяем, возможна ли смена миров
    private void OnTriggerStay2D(Collider2D collision)
    {
        isStateChangePossible = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isStateChangePossible = true;
    }
}
