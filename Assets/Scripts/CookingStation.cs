using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CookingStation : MonoBehaviour
{
    public bool canInteract;
    
    public List<GameObject> addedIngredients;
    public GameObject[] dishes;
    [SerializeField] float cookingTime = 5f; // Tiempo de cocción en segundos
    [SerializeField] GameObject currentCookedDish = null; // Referencia al plato instanciado
    [SerializeField] bool isCooking = false; // Bandera para indicar si se está cocinando
    
    [SerializeField] int appleCount;
    [SerializeField] int pearCount;

    private void Start()
    {
        if (dishes.Length == 0)
        {
            Debug.LogError("No dishes to cook!");
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
            if (!isCooking && Input.GetKeyDown(KeyCode.E) && addedIngredients.Count > 0)
            {
                if (appleCount > 0 || pearCount > 0)
                {
                    StartCoroutine(CookingCoroutine());
                }
                else
                {
                    Debug.Log("Can't cook something with that!");
                }
            }
        }
    }

    IEnumerator CookingCoroutine()
    {
        isCooking = true;
        yield return new WaitForSeconds(cookingTime);

        if (appleCount > 0 && pearCount > 0) 
        {
            currentCookedDish = Instantiate(dishes[0], transform.position, Quaternion.identity);
            Debug.Log("Cooking... Apple and Pear pie");

            appleCount--;
            pearCount--;
        }
        else if (appleCount > 0) 
        {
            currentCookedDish = Instantiate(dishes[1], transform.position, Quaternion.identity);
            Debug.Log("Cooking... Apple pie");
            appleCount--;
        }
        else if (pearCount > 0) 
        {
            currentCookedDish = Instantiate(dishes[2], transform.position, Quaternion.identity);
            Debug.Log("Cooking... Pear pie");
            pearCount--;
        }
        
        RemoveUsedIngredients();

        yield return new WaitForSeconds(cookingTime);

        if (currentCookedDish != null && !addedIngredients.Contains(currentCookedDish))
        {
            Destroy(currentCookedDish);
        }

        isCooking = false;
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
        else if (ingredient.name == "Apple Pie(Clone)" || ingredient.name == "Pear Pie(Clone)"
                                                       || ingredient.name == "Apple and Pear Pie(Clone)")
        {
            // Corrigiendo la llamada recursiva
            addedIngredients.Remove(ingredient);
            currentCookedDish = null;
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
