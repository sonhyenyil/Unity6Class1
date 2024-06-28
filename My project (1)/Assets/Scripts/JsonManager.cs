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
        #region 여러가지 Json변환방식
        //itemData = (TextAsset)Resources.Load("ItemData"); //장점 사용하기가 가장 간단하고 쉽게 형태변형이 가능하다
        //                                                  //형태변환이 안된다면 에러가 발생하며 이후의 코드가 멈추게된다.

        //itemData = Resources.Load<TextAsset>("ItemData");
        //itemData = Resources.Load("ItemData") as TextAsset;//형태변환이 불가능하다면 해당값에 null을 넣어주며 에러가 발생하지 않는다.(실행시에 확인됨)
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
