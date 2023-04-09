using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public int processed;  // J : 몇 개의 재료를 가공했는지

    private Dictionary<string, int> inventoryDict = new Dictionary<string, int>();  // J : 인벤토리

    [SerializeField] private Inventory Inventory;
    [SerializeField] private Animator backgroundAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // J : 임의로 재료 생성
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

    // J : 배합 장면으로 넘어가기
    public void LoadMixingScene()
    {
        // J : 3개의 즙을 완성해야만 이동 가능
        if (processed >= 3)  backgroundAnimator.SetTrigger("Click");
    }
}
