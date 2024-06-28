using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField] string itemidx;

    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;

        List<cItemData> listItems = new List<cItemData>();

        cItemData testData = new cItemData();
        testData.idx = "00000001";
        testData.sprite = GetComponent<SpriteRenderer>().sprite.name;
        
        cItemData testData2 = new cItemData();
        testData.idx = "00000001";
        testData.sprite = GetComponent<SpriteRenderer>().sprite.name;

        listItems.Add(testData);
        listItems.Add(testData2);

        string value = JsonConvert.SerializeObject(listItems);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //닿은 Layer가 플레이어라면
        {
            inventoryManager.GetItem(itemidx);
            //인벤토리 매니저에게 내가 습득되는지 확인
        }
    }
}
