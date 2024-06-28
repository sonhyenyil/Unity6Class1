using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance;


    List<cItemData> itemDatas;
    // Start is called before the first frame update

    private void Awake()
    {
        #region �������� Json��ȯ���
        //itemData = (TextAsset)Resources.Load("ItemData"); //���� ����ϱⰡ ���� �����ϰ� ���� ���º����� �����ϴ�
        //                                                  //���º�ȯ�� �ȵȴٸ� ������ �߻��ϸ� ������ �ڵ尡 ���߰Եȴ�.

        //itemData = Resources.Load<TextAsset>("ItemData");
        //itemData = Resources.Load("ItemData") as TextAsset;//���º�ȯ�� �Ұ����ϴٸ� �ش簪�� null�� �־��ָ� ������ �߻����� �ʴ´�.(����ÿ� Ȯ�ε�)
        #endregion
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        initJsonDatas();
    }
    private void initJsonDatas() 
    {
        TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        itemDatas = JsonConvert.DeserializeObject<List<cItemData>>(itemData.ToString());
    }

    public string GetSpriteNameFromIdx(string idx) 
    {
        if (itemDatas == null) return string.Empty;
        return itemDatas.Find(x => x.idx == idx).sprite;
    }
}
