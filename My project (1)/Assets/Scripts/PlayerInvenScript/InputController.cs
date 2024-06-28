using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;   
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && inventoryManager != null) 
        {
            inventoryManager.InActiveInventory();
        }
    }
}
