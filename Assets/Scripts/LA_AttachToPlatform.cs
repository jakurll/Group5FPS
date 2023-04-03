using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LA_AttachToPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true && other.transform.parent != this.transform)
        {
            other.transform.parent = this.transform;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent == this.transform)
        {
            other.transform.parent = null;
        }
    }
}
