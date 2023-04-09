using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Potion item")]
public class PotionItem : Item
{
    public enum StateType
    {
        Ingredient,     // J : ���� �� ���
        RawMaterial,    // J : ���� �� ���
        CraftedPotion,  // J : ����
    }

    public enum ProcessType // J : ���� ����� Ÿ��
    {
        None,   // J : Ingredient, CraftdPotion
        Mill,   // J : ����
        Boil,   // J : ����
        Grind,  // J : ����
    }

    public StateType state;
    public ProcessType process;
}