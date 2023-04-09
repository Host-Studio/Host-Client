using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    public class Search : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject MonitorTextPanel;
        [SerializeField] private TMP_Text MonitorText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            RepairManager rp = RepairManager.Instance;

            string tstring = "";
            tstring += "������   " + MakeInfoToString(RepairManager.Instance.WeaponInfo.state.iDurabilityState);
            tstring += "\n���ݷ�   " + MakeInfoToString(RepairManager.Instance.WeaponInfo.state.iDamageState);
            tstring += "\n����   " + MakeInfoToString(RepairManager.Instance.WeaponInfo.state.iDefenseState);
            tstring += "\n�� ����  LV." + RepairManager.Instance.WeaponInfo.state.iRuneLevel;

            MonitorText.text = tstring;
            MonitorTextPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MonitorTextPanel.SetActive(false);
        }

        string MakeInfoToString(int value)
        {
            string result = "";
            switch(value)
            {
                case 0: result = "�ſ� ����"; break;
                case 1: result = "����"; break;
                case 2: result = "����"; break;
                case 3: result = "����"; break;
                case 4: result = "�ſ� ����"; break;
            }
            return result;
        }
    }
}