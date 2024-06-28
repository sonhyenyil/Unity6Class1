using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject viewInventory;//인벤토리 뷰
    [SerializeField] GameObject fabItem;//인벤토리에 생성될 프리펩
    [SerializeField] Transform canvasInventory;//인벤토리 캔버스를 불러주기위한 변수
    public Transform CanvasInventory => canvasInventory;//캔버스 인벤토리 변수를 불러주면 특정 캔버스를 지정해서 불러옴

    List<Transform> listTrsInventory = new List<Transform>();
    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitInventory();
    }
    private void InitInventory() 
    {
        listTrsInventory.Clear();//클리어를 하지 않으면 이전에 있던 데이터도 담기게 됨

        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>();//문제가 있음 -> GetComponent는 본인을 포함함

        listTrsInventory.AddRange(childs);
        listTrsInventory.RemoveAt(0); //자기자신을 지우면 이제 자식들의 데이터만 담을 수 있음
    }
    /// <summary>
    /// 인벤토리가 열려있다면 닫히게, 닫혀있다면 열리게 할것
    /// </summary>


    public void InActiveInventory() 
    {
        //if (viewInventory.activeSelf == true) // or (viewInventort.activeSelf)
        //{
        //    viewInventory.SetActive(false);
        //}
        //else
        //{
        //    viewInventory.SetActive(true);
        //}

        viewInventory.SetActive(!viewInventory.activeSelf);
    }

    /// <summary>
    /// 비어있는 인벤토리 넘버를 리턴합니다. -1이 리턴된다면 비어있는 슬롯이 없다는 의미입니다.
    /// </summary>
    /// <returns>비어있는 아이템 슬롯 번호</returns>
    private int getEmptyItemSlot()
    {
        int count = listTrsInventory.Count;

        for(int iNum = 0; iNum < count; iNum++) 
        {
            Transform trsSolt = listTrsInventory[iNum];
            if (trsSolt.childCount == 0)
            {
                return iNum;
            }
        }
        return -1; //-1은 존재할수 없기에 -1이 리턴시 비어있는 슬롯이 없다는 의미
    }

    public bool GetItem(string _idx)
    {
        int slotNum = getEmptyItemSlot();
        if (slotNum == -1)
        {
            return false;
        }

        GameObject go = Instantiate(fabItem, listTrsInventory[slotNum]);
        //오브젝트에게 _idx정보 데이터를 전당하면 매니저가 할 행동은 마무리됨
        return true;

        
    }
}
