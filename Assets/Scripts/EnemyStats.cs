using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHealth = 100f;
    public Material _shader { get; set; }

    // Get material
    void Start()
    {
        _shader = GetComponentInChildren<Renderer>().material;
    }

    // If health is less than zero then set then non-active
    void Update()
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
