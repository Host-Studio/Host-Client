using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class Hammering : MonoBehaviour
    {
        public GameObject Quenching;
        public Image weaponImg;

        [SerializeField] private GameObject[] TargetObjects;
        private int clickedTarget = 0;
        private string type = "";

        void OnEnable()
        {
            SetWeaponImage(RepairManager.Instance.WeaponInfo.name);
        }

        void OnDisable()
        {
            foreach(GameObject go in TargetObjects)
            {
                go.SetActive(true);
                clickedTarget = 0;
                type = "";
            }
        }

        public void ClickTarget()
        {
            clickedTarget++;

            if(clickedTarget >= TargetObjects.Length)
            {
                StartCoroutine(ActiveFalseAfter(1f));
            }
        }

        public void SetType(string ptype)
        {
            type = String.Copy(ptype);

            print(type);
        }

        IEnumerator ActiveFalseAfter(float time)
        {
            yield return new WaitForSeconds(time);

            Quenching.SetActive(true);
            Quenching.GetComponent<Quenching>().SetType(type);
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