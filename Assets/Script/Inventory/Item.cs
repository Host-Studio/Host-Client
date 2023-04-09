using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType  // J : 아이템 유형
    {
        Potion,
        Cook,
    }
    
    public string itemName; // J : 이름
    public ItemType itemType; // J : 유형
    public Sprite itemImage; // J : 인벤토리에 나타날 이미지
}