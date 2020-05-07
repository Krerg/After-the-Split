using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerManager : MonoBehaviour
{

    public TextMeshProUGUI levelHint;

    public GameObject lockedBox;

    public bool isFirstOpen = false;

    public bool isSecondOpen = false;

    public bool isKeyPut = false;

    private bool isKeyGet = false;

    private bool canUseItem = false;

    public bool isBoxUnlock = false;

    private string currentActionObject = "";

    public bool IsTom = true;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowLevelHint", 40);
    }

    public void UseItem()
    {
        if(!canUseItem)
        {
            return;
        }
        switch (currentActionObject)
        {

            case "FirstLock":
                isFirstOpen = true;
                break;
            case "SecondLock":
                isSecondOpen = true;
                break;
            case "KeyLocker":
                if(!IsTom)
                {
                    isKeyPut = true;
                } else
                {
                    isKeyGet = true;
                }
                
                break;
           
            case "UnlockBox":
                lockedBox.GetComponent<LockedBox>().Unlock();
                isBoxUnlock = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelHint()
    {
        string levelHintText = "";
        if (!isFirstOpen)
        {
            levelHintText = "Играя за Сару вы можете проникать в более узкие проемы. Используя это вы можете найти скрытые ящики с предметами";
        } else if (!isSecondOpen)
        {
            levelHintText = "Играя за Сару вы можете проникать в более узкие проемы. Используя это вы можете найти скрытые ящики с предметами";
        } else if (!isKeyPut)
        {
            levelHintText = "Вы должны передать ключ Тому. Найдите место куда вы можете их положить";
        } else if (!isKeyGet)
        {
            levelHintText = "Возьмите ключ, который положила Сара и найдите от чего он.";
        } else if (!isBoxUnlock)
        {
            levelHintText = "Том уже упоминал о ящике?";
        }
        levelHint.text = "";
        levelHint.text = levelHintText;
        Invoke("ShowLevelHint", 35);
    }

    public bool getCanUseItem()
    {
        return canUseItem;
    }

    public void resetCurrentObjectToAction()
    {
        this.currentActionObject = "";
        canUseItem = false;
    }

    public void setCurrentObjectToAction(string objectName)
    {
        this.currentActionObject = objectName;
        switch (objectName)
        {
            case "FirstLock":
                if (!isFirstOpen && !IsTom)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;
            case "SecondLock":
                if (isFirstOpen && !isSecondOpen && !IsTom)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;
            case "KeyLocker":
                if (isFirstOpen && isSecondOpen && !isKeyPut && !IsTom)
                {
                    canUseItem = true;
                }
                else if(isFirstOpen && isSecondOpen && isKeyPut && IsTom && !isKeyGet)
                {
                    canUseItem = true;
                } else
                {
                    canUseItem = false;
                }
                break;
            case "UnlockBox":
                if (isFirstOpen && isSecondOpen && isKeyPut && isKeyGet && !isBoxUnlock && IsTom)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;

        }
    }
}
