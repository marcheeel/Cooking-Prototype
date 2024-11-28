using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cooking : MonoBehaviour
{
    public List<GameObject> addedIngredients;
    public GameObject[] dishes;
    [SerializeField] float cookingTime = 5f; // Tiempo de cocción en segundos
    private GameObject currentCookedDish = null; // Referencia al plato instanciado
    private bool isCooking = false; // Bandera para indicar si se está cocinando
    
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
    }

    private void Update()
    {
        if (!isCooking && Input.GetKeyDown(KeyCode.E) && addedIngredients.Count > 0)
        {
            StartCoroutine(CookingCoroutine());
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
    
    void RemoveUsedIngredients()
    {
        var apple = addedIngredients.Find(item => item.name == "Apple");
        if (apple != null)
        {
            addedIngredients.Remove(apple);
            Destroy(apple);
        }
    
        var pear = addedIngredients.Find(item => item.name == "Pear");
        if (pear != null)
        {
            addedIngredients.Remove(pear);
            Destroy(pear);
        }
    }
    
    void AddIngredient(GameObject ingredient)
    {
        if (ingredient.name == "Apple")
        {
            appleCount++;
            addedIngredients.Add(ingredient);
        }
        else if (ingredient.name == "Pear")
        {
            pearCount++;
            addedIngredients.Add(ingredient);
        }
        else
        {
            RemoveIngredient(ingredient);
        }
    }
    
    void RemoveIngredient(GameObject ingredient)
    { 
        if (ingredient.name == "Apple")
        {
            appleCount--;
            addedIngredients.Remove(ingredient);

        }
        else if (ingredient.name == "Pear")
        {
            pearCount--;
            addedIngredients.Remove(ingredient);
        }
        else
        {
            RemoveIngredient(ingredient);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Draggable") && other.gameObject.name != "Apple Pie" 
                                                     && other.gameObject.name != "Pear Pie" 
                                                     && other.gameObject.name != "Apple and Pear pie")
        {
            AddIngredient(other.gameObject);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Draggable") && other.gameObject.name != "Apple Pie" 
                                                     && other.gameObject.name != "Pear Pie" 
                                                     && other.gameObject.name != "Apple and Pear pie")
        {
            RemoveIngredient(other.gameObject);
        }
    }
}
