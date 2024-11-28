using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] float rayDistance = 10;
    [SerializeField] Transform hands;
    public Transform grabbedObject;
    [SerializeField] bool isDragging = false;
    [SerializeField] LayerMask raycastLayerMask;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDragging)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.SetParent(null);
            grabbedObject = null;
            
            isDragging = !isDragging;
        }
        
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, raycastLayerMask))
        {
            if (hit.transform.tag == "Draggable" && Input.GetKeyDown(KeyCode.Space))
            {
                isDragging = !isDragging;

                if (isDragging)
                {
                    grabbedObject = hit.transform;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.SetParent(hands);
                    grabbedObject.localPosition = Vector3.zero;
                }
                else
                {
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                    grabbedObject.SetParent(null);
                    grabbedObject = null;
                    isDragging = !isDragging;
                }
            }
        }
    }
}
