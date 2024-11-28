using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutStation : MonoBehaviour
{
    public bool canInteract;
    
    public List<GameObject> addedIngredients;
    public GameObject[] slicedIngredients;
    [SerializeField] GameObject currentSlicedIngredient = null; // Referencia al plato instanciado
    
    [SerializeField] int appleCount;
    [SerializeField] int pearCount;

    private void Start()
    {
        if (slicedIngredients.Length == 0)
        {
            Debug.LogError("Can't find the sliced ingredients");
        }
        
        addedIngredients.Clear();

        appleCount = 0;
        pearCount = 0;
        
        canInteract = false;
    }
    
    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && addedIngredients.Count > 0)
            {
                if (appleCount > 0) 
                {
                    currentSlicedIngredient = Instantiate(slicedIngredients[0], transform.position, Quaternion.identity);
                    Debug.Log("Apple sliced");
                    appleCount--;
                }
                else if (pearCount > 0) 
                {
                    currentSlicedIngredient = Instantiate(slicedIngredients[1], transform.position, Quaternion.identity);
                    Debug.Log("Pear sliced");
                    pearCount--;
                }
        
                RemoveUsedIngredients();
            }
        }
    }
    
    void AddIngredient(GameObject ingredient)
    {
        if (ingredient.name == "Apple" || ingredient.name == "Apple(Clone")
        {
            appleCount++;
            addedIngredients.Add(ingredient);
        }
        else if (ingredient.name == "Pear"|| ingredient.name == "Pear(Clone")
        {
            pearCount++;
            addedIngredients.Add(ingredient);
        }
        else
        {
            addedIngredients.Add(ingredient);
        }
    }
    
    void RemoveUsedIngredients()
    {
        for (int i = addedIngredients.Count - 1; i >= 0; i--)
        {
            var item = addedIngredients[i];
            if (item.name == "Apple" || item.name == "Apple(Clone" 
                || item.name == "Pear" || item.name == "Pear(Clone")
            {
                addedIngredients.RemoveAt(i);
                Destroy(item);
            }
        }
    }
    
    // Todos los GameObjects instanciados tienen su nombre + (Clone)
    // Ejemplo: Apple Pie(Clone)
    // Para eliminar el objeto instanciado, se debe eliminar el objeto con el mismo nombre
    // Ejemplo: Apple Pie

    void RemoveIngredient(GameObject ingredient)
    {
        if (ingredient.name == "Apple" || ingredient.name == "Apple(Clone")
        {
            appleCount--;
            addedIngredients.Remove(ingredient);
        }
        else if (ingredient.name == "Pear"|| ingredient.name == "Pear(Clone")
        {
            pearCount--;
            addedIngredients.Remove(ingredient);
        }
        else if (ingredient.name == "Sliced Apple(Clone)" || ingredient.name == "Sliced Pear(Clone)")
        {
            addedIngredients.Remove(ingredient);
            currentSlicedIngredient = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Draggable"))
        {
            AddIngredient(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            canInteract = !canInteract;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Draggable"))
        {
            RemoveIngredient(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            canInteract = !canInteract;
        }
    }
}
