using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHealth = 100f;
    public Material _shader { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _shader = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
