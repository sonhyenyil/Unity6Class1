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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //���� Layer�� �÷��̾���
        {
            inventoryManager.GetItem(itemidx);
            //�κ��丮 �Ŵ������� ���� ����Ǵ��� Ȯ��
        }
    }
}
