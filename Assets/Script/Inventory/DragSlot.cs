using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;    // J : static 변수이므로 어디서든 접근 가능
    public Slot dragSlot;   // J : 드래그하는 슬롯

    [SerializeField] private Image itemImage;   // J : 본 오브젝트의 image 컴포넌트

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
