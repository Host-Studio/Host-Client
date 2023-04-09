using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingButton : MonoBehaviour, IDropHandler
{
    // 요리 종류
    [SerializeField] private GameObject category;
    // 해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            DropItem.instance.CookDrop();
            CookDataManager.CookObject operation = new CookDataManager.CookObject();
            operation.id = category.name;
            CookDataManager.Instance.ItemSelected(CookDataManager.Instance.draggingItem, operation);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
