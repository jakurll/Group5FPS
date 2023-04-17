using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    //Get speedBoost value and get speed value from movement script
    public float speedBoost = 1.0f;
    public float speedLimit = 10.0f;

    private int numSpeedpickup = 0;
    Movement movement;

    void Awake()
    {
        movement = FindObjectOfType<Movement>();
    }
    // Destroy the pickup game object and add
    // speed boost to movement speed of character
    void OnTriggerEnter(Collider col)
    {
        numSpeedpickup += 1;
        if (numSpeedpickup == 1)
        {
          movement.speed += speedBoost;
          StartCoroutine("SpeedDuration");
        }
    }

    IEnumerator SpeedDuration()
    {
        yield return new WaitForSeconds(speedLimit);
        movement.speed -= speedBoost;
        Destroy(gameObject);
    }
}
