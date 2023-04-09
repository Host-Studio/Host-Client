using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType  // J : ������ ����
    {
        Potion,
        Cook,
    }
    
    public string itemName; // J : �̸�
    public ItemType itemType; // J : ����
    public Sprite itemImage; // J : �κ��丮�� ��Ÿ�� �̹���
}