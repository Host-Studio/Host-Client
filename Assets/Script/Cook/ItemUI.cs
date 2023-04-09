using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string item;
    public bool isDroppedOnCook = false;
    /********************
        # 8 -> # CDM
    ********************/
    private void OnMouseEnter() {
        CookDataManager.Instance.SendFlavorData(int.Parse(Regex.Replace(item, @"\D", "")));
    }

    // /// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     // flavor description
    //     CookDataManager.Instance.SendFlavorData(int.Parse(Regex.Replace(item.imgId, @"\D", "")));

    //     // 드래그 직전 현재 위치와 부모 저장
    //     original = transform.position;
    //     origin_parent = transform.parent;
    //     transform.SetParent(canvas);

    //     // 드래그 중인 아이템 정보 갱신
    //     CookDataManager.Instance.DraggingItem(item);
    // }

    // /// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
    // public void OnDrag(PointerEventData eventData)
    // {
    //     this.transform.position = eventData.position;
    // }

    // /// 현재 오브젝트의 드래그를 종료할 때 1회 호출
    // public void OnEndDrag(PointerEventData eventData)
    // {
        
    //     if(transform.parent == canvas)
    //     {
    //         transform.SetParent(origin_parent);
    //     }
        
    //     this.transform.position = original;
    // }


    private Vector3 original;
    private Transform origin_parent;
    private Transform canvas;
    // Start is called before the first frame update
    private void Awake() 
    {
        canvas		= FindObjectOfType<Canvas>().transform;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
