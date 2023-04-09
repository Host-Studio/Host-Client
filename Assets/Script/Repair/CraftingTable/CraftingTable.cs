using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    public class CraftingTable : MonoBehaviour
    {
        public Image weaponImg;

        void OnEnable()
        {
            SetWeaponImage(RepairManager.Instance.WeaponInfo.name);
        }

        public void SetWeaponImage(string weaponName)
        {
            foreach(Sprite sprite in RepairManager.Instance.weaponImgList)
            {
                if(sprite.name.Contains(weaponName))
                {
                    weaponImg.sprite = sprite;
                }
            }
        }
    }
}