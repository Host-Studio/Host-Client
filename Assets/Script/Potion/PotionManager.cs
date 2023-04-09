using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public int processed;  // J : �� ���� ��Ḧ �����ߴ���

    private Dictionary<string, int> inventoryDict = new Dictionary<string, int>();  // J : �κ��丮

    [SerializeField] private Inventory Inventory;
    [SerializeField] private Animator backgroundAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // J : ���Ƿ� ��� ����
        inventoryDict["Potion_Processing_Material_1008"] = 1;
        inventoryDict["Potion_Processing_Material_1009"] = 2;
        inventoryDict["Potion_Processing_Material_1010"] = 3;


        SetInventory();
    }

    private void SetInventory()
    {
        foreach (KeyValuePair<string, int> slot in inventoryDict)
        {
            Item item = Resources.Load<Item>("Item/" + slot.Key);
            Inventory.AcquireItem(item, slot.Value);
        }
    }

    // J : ���� ������� �Ѿ��
    public void LoadMixingScene()
    {
        // J : 3���� ���� �ϼ��ؾ߸� �̵� ����
        if (processed >= 3)  backgroundAnimator.SetTrigger("Click");
    }
}
