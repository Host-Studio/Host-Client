using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;    // J : static �����̹Ƿ� ��𼭵� ���� ����
    public Slot dragSlot;   // J : �巡���ϴ� ����

    [SerializeField] private Image itemImage;   // J : �� ������Ʈ�� image ������Ʈ

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        itemImage.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
