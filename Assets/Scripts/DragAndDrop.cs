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
        
        RaycastHit hit; // Reutilizar hit variable
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
            }
            else if (hit.transform.tag == "Basket" && Input.GetKeyDown(KeyCode.Space))
            {
                isDragging = !isDragging;

                if (isDragging)
                {
                    GameObject newObject = Instantiate(hit.transform.GetComponent<Basket>().ingredientToGive,
                        hit.transform.position, Quaternion.identity);
                    newObject.GetComponent<Rigidbody>().isKinematic = true;
                    newObject.transform.SetParent(hands);
                    newObject.transform.localPosition = Vector3.zero;
                    grabbedObject = newObject.transform;
                }
            }

        }
    }
}
