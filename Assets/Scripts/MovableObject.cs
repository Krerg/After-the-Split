using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.layer = 8;
    }

    Rigidbody2D rb;
    public void ChangeVelocity(ref float initialVelocity)
    {
        rb = GetComponent<Rigidbody2D>();
        initialVelocity *= Player.movingObjectsAcceleration;
    }
}
