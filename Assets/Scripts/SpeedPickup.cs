using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    //Get speedBoost value and get speed value from movement script
    public float speedBoost = 1.0f;
    Movement movement;

    void Awake()
    {
        movement = FindObjectOfType<Movement>();
    }

    // Destroy the pickup game object and add
    // speed boost to movement speed of character
    void OnTriggerEnter(Collider col)
    {
        movement.speed += speedBoost;
        Destroy(gameObject);
    }
}
