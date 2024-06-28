using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] GameObject viewInventory;//�κ��丮 ��
    [SerializeField] GameObject fabItem;//�κ��丮�� ������ ������
    [SerializeField] Transform canvasInventory;//�κ��丮 ĵ������ �ҷ��ֱ����� ����
    public Transform CanvasInventory => canvasInventory;//ĵ���� �κ��丮 ������ �ҷ��ָ� Ư�� ĵ������ �����ؼ� �ҷ���

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
        listTrsInventory.Clear();//Ŭ��� ���� ������ ������ �ִ� �����͵� ���� ��

        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>();//������ ���� -> GetComponent�� ������ ������

        listTrsInventory.AddRange(childs);
        listTrsInventory.RemoveAt(0); //�ڱ��ڽ��� ����� ���� �ڽĵ��� �����͸� ���� �� ����
    }
    /// <summary>
    /// �κ��丮�� �����ִٸ� ������, �����ִٸ� ������ �Ұ�
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
    /// ����ִ� �κ��丮 �ѹ��� �����մϴ�. -1�� ���ϵȴٸ� ����ִ� ������ ���ٴ� �ǹ��Դϴ�.
    /// </summary>
    /// <returns>����ִ� ������ ���� ��ȣ</returns>
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
        return -1; //-1�� �����Ҽ� ���⿡ -1�� ���Ͻ� ����ִ� ������ ���ٴ� �ǹ�
    }

    public bool GetItem(string _idx)
    {
        int slotNum = getEmptyItemSlot();
        if (slotNum == -1)
        {
            return false;
        }

        GameObject go = Instantiate(fabItem, listTrsInventory[slotNum]);
        //������Ʈ���� _idx���� �����͸� �����ϸ� �Ŵ����� �� �ൿ�� ��������
        return true;

        
    }
}
