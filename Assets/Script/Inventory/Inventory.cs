using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slots;  // J : ���� �迭

    [SerializeField] private GameObject Grid;  // J : Slot���� �θ�


    void Awake()
    {
        slots = Grid.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)   // J : �̹� �κ��丮�� �ִ� ������
                {
                    slots[i].SetSlotCount(_count);  // J : ���� ������Ʈ
                    return;
                }
            }
        }

        // J : �κ��丮�� ���� �������̹Ƿ� ���� ���� ��ĭ�� �߰�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    // private void SetInventory()
    // {
    //     foreach (KeyValuePair<string, int> slot in inventoryDict)
    //     {
    //         Item item = Resources.Load<Item>("Item/" + slot.Key);
    //         Inventory.AcquireItem(item, slot.Value);
    //     }
    // }
}
