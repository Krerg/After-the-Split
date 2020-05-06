﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Fadable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                foreach (SpriteRenderer child in children)
            {
                
                    // set color with i as alpha
                    child.color = new Color(child.color.r, child.color.g, child.color.b, i);
                    
                }
                yield return null;
            }
              
        }
        // fade from transparent to opaque
        else
        {
            SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                foreach (SpriteRenderer child in children)
            {

                    // set color with i as alpha
                    child.color = new Color(child.color.r, child.color.g, child.color.b, i);

                }
                yield return null;
            }
        }
    }

    public IEnumerator FadeMeshImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            SkeletonAnimation[] children = GetComponentsInChildren<SkeletonAnimation>();
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                foreach (SkeletonAnimation child in children)
                {

                    // set color with i as alpha
                    child.skeleton.A =  i;

                }
                yield return null;
            }

        }
        // fade from transparent to opaque
        else
        {
            SkeletonAnimation[] children = GetComponentsInChildren<SkeletonAnimation>();
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                foreach (SkeletonAnimation child in children)
                {

                    // set color with i as alpha
                    child.skeleton.A = i;

                }
                yield return null;
            }
        }
    }

    public void DisableCollision()
    {
        BoxCollider2D[] children = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D child in children)
        {
            child.enabled = false;
        }
        Rigidbody2D[] childrenRigidBody = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D child in childrenRigidBody)
        {
            child.velocity = Vector3.zero;
            child.isKinematic = true;
        }
    }

    public void EnableCollision()
    {
        BoxCollider2D[] children = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D child in children)
        {
            child.enabled = true;
        }

        Rigidbody2D[] childrenRigidBody = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D child in childrenRigidBody)
        {
            if(child.name == "LockedBox")
            {
                continue;
            }
            child.isKinematic = false;
        }

    }

    public void DisableLight()
    {
        Light2D[] children = GetComponentsInChildren<Light2D>();
        foreach (Light2D child in children)
        {
            child.enabled = false;
        }
    }

    public void EnableLight()
    {
        Light2D[] children = GetComponentsInChildren<Light2D>();
        foreach (Light2D child in children)
        {
            child.enabled = true;
        }
    }

}
