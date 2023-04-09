using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

namespace Repair
{
    public class RepairManager : MonoBehaviour
    {
        private static RepairManager    instance = null;
        public static RepairManager     Instance
        {
            get
            {
                if (instance == null)
                    return null;
                else
                    return instance;
            }
        }

        private GuestDB.WeaponInfo      weaponInfo;
        public GuestDB.WeaponInfo       WeaponInfo { get { return weaponInfo; } }
        public string                   strOwnerName;
        public List<Sprite>             weaponImgList = new List<Sprite>();

        public void Awake()
        {
            if (null == instance)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            // 임시 설정
            SetRepairManager("톨문드", new GuestDB.WeaponInfo("도끼", 0, 1, 0, true, 2));
        }

        public void SetRepairManager(string ownerName, GuestDB.WeaponInfo weapon)
        {
            strOwnerName = String.Copy(ownerName);
            weaponInfo = weapon;
        }

        public void RemoveState(string state, int value=0)
        {
            switch(state)
            {
                case "룬":
                case "Rune":
                    weaponInfo.state.iRuneLevel++;
                    break;
                case "내구도":
                case "Begin":
                    weaponInfo.state.iDurabilityState++;
                    break;
                case "공격력":
                case "Brave":
                    weaponInfo.state.iDamageState++;
                    break;
                case "방어력":
                case "Bless":
                    weaponInfo.state.iDefenseState++;
                    break;
            }
        }
    }
}