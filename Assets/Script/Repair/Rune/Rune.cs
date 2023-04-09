using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    public class Rune : MonoBehaviour
    {
        public List<GameObject> runes;
        public Image weaponImg;

        private int randomIdx;
        private int enabledPointCount;

        void OnEnable()
        {
            enabledPointCount = 0;
            randomIdx = Random.Range(0, runes.Count);
            runes[randomIdx].SetActive(true);

            SetWeaponImage(RepairManager.Instance.WeaponInfo.name);
        }

        void OnDisable()
        {
            runes[randomIdx].SetActive(false);
        }

        public void EnabledPoint()
        {
            enabledPointCount++;

            if (enabledPointCount >= runes[randomIdx].GetComponent<Transform>().childCount)
            {
                RepairManager.Instance.RemoveState("·é");
                StartCoroutine(ActiveFalseAfter(1f));
            }
        }

        IEnumerator ActiveFalseAfter(float time)
        {
            yield return new WaitForSeconds(time);

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