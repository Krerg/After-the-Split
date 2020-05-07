﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    float speed = 3f;

    public Transform target;

    private bool isEndGame = false;

    float tempSize;

    //Фактор и скорость наезжания
    public float ZoomSize;
    float zoomSize;
    public float zoomSpeed;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        tempSize = cam.orthographicSize;

        zoomSize = cam.orthographicSize;

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        //Слушаем event, чтобы приблизиться к подобранному обьекту
        GameEventSystem.current.onObjectPickedUp += ZoomToObject;

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_position;
        if (isEndGame)
        {
            GetComponent<Camera>().orthographicSize = 7;
            target_position = new Vector3(36f, -15.44f, target.position.z);
        } else
        {
            target_position = new Vector3(target.position.x, target.position.y + 1.7f, target.position.z);
        }
         
        target_position.z = transform.position.z - 1;

        Vector3 approximate_target_position = Vector3.Lerp(transform.position, target_position, speed * Time.deltaTime);
        transform.position = approximate_target_position;
        if (approximate_target_position.x < -5f)
        {
            transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        }

        if (approximate_target_position.x > 35f)
        {
            transform.position = new Vector3(35f, transform.position.y, transform.position.z);
        }

        if (approximate_target_position.y > -13f)
        {
            transform.position = new Vector3(transform.position.x, -13f, transform.position.z);
        }

        if (approximate_target_position.y < -27f)
        {
            transform.position = new Vector3(transform.position.x, -27f, transform.position.z);
        }

        if (cam.orthographicSize - zoomSize > 0.5)
        {
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
        }
        else if (zoomSize - cam.orthographicSize > 0.5)
        {
            cam.orthographicSize += Time.deltaTime * zoomSpeed;
        }

    }

    public float zoomOutTime;
    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(zoomOutTime);
        zoomSize = tempSize;
        target = tempTarget;
    }


    Transform tempTarget;
    private void ZoomToObject(Transform obj)
    {
        tempTarget = this.target;
        this.target = obj;
        zoomSize = ZoomSize;
        StartCoroutine(ZoomOut());
    }

    public void EndGame()
    {
        isEndGame = true;
    }
}
