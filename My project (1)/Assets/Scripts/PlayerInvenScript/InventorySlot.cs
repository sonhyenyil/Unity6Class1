using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//아이템을 드래그 시작하거나 드래그해서 놓으면 이 스크립트가 아이템을 파악하여 자식으로 분류해줌
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    //IPointerEnterHandler -> 특정 영역에 마우스 포인터가 들어갈때 동작하는 이벤트
{
    Image imgSlot;
    RectTransform rect;
    private void Awake()
    {
        imgSlot = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgSlot.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgSlot.color = Color.white;
    }


    /// <summary>
    /// 이벤트 시스템으로 인해 드래그 되는 대상이 이 스크립트 위에서 드롭되게 되면
    /// 해당 그롭 오브젝트를 나의 자식으로 변경합니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) 
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.position = rect.position;
        }
    }
}
