using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{

    public GameObject lockedBox;

    private bool isFirstOpen = false;

    private bool isSecondOpen = false;

    private bool isKeyPut = false;

    private bool canUseItem = false;

    private bool isBoxUnlock = false;

    private string currentActionObject = "";

    // Start is called before the first frame update
    void Start()
    {
        
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
            case "KeyPut":
                isKeyPut = true;
                break;
            case "UnlockBox":
                lockedBox.GetComponent<LockedBox>().Unlock();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
                if (!isFirstOpen)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;
            case "SecondLock":
                if (isFirstOpen && !isSecondOpen)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;
            case "KeyPut":
                if (isFirstOpen && isSecondOpen && !isKeyPut)
                    canUseItem = true;
                else
                    canUseItem = false;                
                break;
            case "UnlockBox":
                if (isFirstOpen && isSecondOpen && isKeyPut && !isBoxUnlock)
                    canUseItem = true;
                else
                    canUseItem = false;
                break;

        }
    }
}
