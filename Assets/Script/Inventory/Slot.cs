using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; // J : ������ ����
    public int itemCount; // J : ������ ����
    private Image itemImage;  // J : ������ �̹���

    [SerializeField] private GameObject itemImg;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        itemImage = itemImg.GetComponent<Image>();
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // �� ���Կ� ���� ���콺 ���
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.SetActive(true);
        itemImage.sprite = item.itemImage;
        countText.text = itemCount.ToString();
    }

    // J : �� ������ ����(��ġ) �ٲٱ�
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

    // J : �̹� ������ �ִ� �������� ȹ�� or ����� ���
    // J : ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        countText.text = itemCount.ToString();

        // �������� ��� ����� ���
        if (itemCount <= 0)
            ClearSlot();
    }

    // J : �ش� ���Կ� �ִ� �������� ��� ����� ���
    // J : ���� �ʱ�ȭ
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
