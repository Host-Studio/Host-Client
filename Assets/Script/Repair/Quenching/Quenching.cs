using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    public class Quenching : MonoBehaviour
    {
        public GameObject CraftingTable;
        public Image weaponImg;

        private string type;

        void OnEnable()
        {
            SetWeaponImage(RepairManager.Instance.WeaponInfo.name);
        }

        public void Finish()
        {
            RepairManager.Instance.RemoveState(type);
            StartCoroutine(ActiveFalseAfter(1f));
        }

        public void SetType(string ptype)
        {
            type = string.Copy(ptype);
        }

        IEnumerator ActiveFalseAfter(float time)
        {
            yield return new WaitForSeconds(time);

            CraftingTable.SetActive(true);
            gameObject.SetActive(false);
        }

        public void SetWeaponImage(string weaponName)
        {
            foreach (Sprite sprite in RepairManager.Instance.weaponImgList)
            {
                if (sprite.name.Contains(weaponName))
                {
                    weaponImg.sprite = sprite;
                }
            }
        }
    }

}