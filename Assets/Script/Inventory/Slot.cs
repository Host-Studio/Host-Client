using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; // J : 아이템 정보
    public int itemCount; // J : 아이템 개수
    private Image itemImage;  // J : 아이템 이미지

    [SerializeField] private GameObject itemImg;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        itemImage = itemImg.GetComponent<Image>();
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // 이 슬롯에 무언가 마우스 드롭
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.SetActive(true);
        itemImage.sprite = item.itemImage;
        countText.text = itemCount.ToString();
    }

    // J : 두 슬롯의 정보(위치) 바꾸기
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    // J : 이미 가지고 있는 아이템을 획득 or 사용한 경우
    // J : 아이템 개수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        countText.text = itemCount.ToString();

        // 아이템을 모두 사용한 경우
        if (itemCount <= 0)
            ClearSlot();
    }

    // J : 해당 슬롯에 있던 아이템을 모두 사용한 경우
    // J : 슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.SetActive(false);
        itemImage.sprite = null;
        countText.text = "0";
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            int itemId = int.Parse(item.itemImage.name.Replace("food", ""));
            CookDataManager.Instance.SendFlavorData(itemId);
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            CookDataManager.Instance.DelFlavorData();
        }
    }
}
