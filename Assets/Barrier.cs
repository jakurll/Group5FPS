using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    bool canUnlock = false;
    PlayerInventory inventory;

    void Update()
    {
        if (canUnlock == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.hasKey = false;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.hasKey == true)
            {
                canUnlock = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetValues();
    }

    private void ResetValues()
    {
        canUnlock = false;
        inventory = null;
    }


}
