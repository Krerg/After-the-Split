using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitcher : MonoBehaviour
{

    int state = 0;

    bool isSwitchPossible = false;

    public TriggerManager tm;

    // Start is called before the first frame update
    void Start()
    {
        SwitchPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Меняем время по W
        if (Input.GetKeyDown(KeyCode.W) && isSwitchPossible)
        {

            Fadable map1 = GameObject.Find("Map1").GetComponent<Fadable>();
            Fadable map2 = GameObject.Find("Map2").GetComponent<Fadable>();
            
            if (state == 1)
            {
                map1.EnableCollision();
                map1.EnableLight();
                map2.DisableCollision();
                map2.DisableLight();
                StartCoroutine(map1.FadeImage(false));
                StartCoroutine(map2.FadeImage(true));
                state = 0;
                tm.IsTom = true;
            }
            else
            {
                map2.EnableCollision();
                map2.EnableLight();
                map1.DisableCollision();
                map1.DisableLight();
                StartCoroutine(map1.FadeImage(true));
                StartCoroutine(map2.FadeImage(false));
                state = 1;
                tm.IsTom = false;
            }
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        GameObject player = GameObject.Find("Player");
        BoxCollider2D bc = player.GetComponentInChildren<BoxCollider2D>();
        
        if (state == 0)
        {
            StartCoroutine(player.transform.GetChild(0).GetComponent<Fadable>().FadeMeshImage(true));
            player.transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
            StartCoroutine(player.transform.GetChild(1).GetComponent<Fadable>().FadeMeshImage(false));
            player.transform.GetChild(1).GetComponent<AudioSource>().enabled = true;
            bc.size = new Vector2(0.5159993f, 1.023184f);
            bc.offset = new Vector2(-0.01571423f, 0.01005542f);
        } else
        {
            StartCoroutine(player.transform.GetChild(0).GetComponent<Fadable>().FadeMeshImage(false));
            player.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
            StartCoroutine(player.transform.GetChild(1).GetComponent<Fadable>().FadeMeshImage(true));
            player.transform.GetChild(1).GetComponent<AudioSource>().enabled = false;
            bc.size = new Vector2(0.5159993f, 0.7283263f);
            bc.offset = new Vector2(-0.01571423f, -0.1358368f);
        }
    }

    public void DisableSwitch()
    {
        isSwitchPossible = false;
        print("Нельзя переместиться");
    }

    public void EnableSwitch()
    {
        isSwitchPossible = true;
        print("Можно переместиться");
    }

    public bool isTom()
    {
        return state == 0;
    }
}
