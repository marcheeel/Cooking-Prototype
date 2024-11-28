using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
