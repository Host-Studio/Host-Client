using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Heating : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // 다음 단계 Hammering
        public GameObject Hammering;

        // 이미지
        public Image weaponImg;

        // Canvas
        private RectTransform rect;

        // 드래그
        public static GameObject itemBeingDragged;
        private Vector3 startPosition;

        // 불꽃 탐지용 레이
        private RaycastHit2D rayHit;

        // 호버링
        [SerializeField] private Image gaugeBar;
        private bool hovering = false;
        private string strOldType = "Old";
        private string strNewType = "New";
        private float hoveringTime = 2f;
        private int iLayerMask;

        private bool done = false;
        private string resultType = "";

        void Start()
        {
            rect = GetComponent<RectTransform>();
            iLayerMask = ~(LayerMask.GetMask("Ignore Raycast"));
        }

        void OnEnable()
        {
            gaugeBar.fillAmount = 0;
            hovering = false;
            strOldType = "Old";
            strNewType = "New";
            hoveringTime = 2f;
            done = false;
            resultType = "";

            SetWeaponImage(RepairManager.Instance.WeaponInfo.name);
        }

        void FixedUpdate()
        {
            if(done)
            {
                return;
            }

            if (hovering)
            {
                if(strOldType != strNewType)
                {
                    // 새로 시작
                    strOldType = string.Copy(strNewType);

                    gaugeBar.fillAmount = 0f;
                }

                else
                {
                    // 기존 작업
                    gaugeBar.fillAmount += 1 / hoveringTime * Time.deltaTime;
                }
            }
            else
            {
                gaugeBar.fillAmount = 0f;

                strOldType = "Old";
                strNewType = "New";
            }

            if (gaugeBar.fillAmount >= 1f - 0.00000001f)
            {
                done = true;
                resultType = string.Copy(strOldType);

                print(resultType);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect.anchoredPosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            transform.position = startPosition;

            hovering = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            hovering = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            hovering = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.parent.name == "Flame")
            {
                string tstrFireName = collision.transform.name;
                switch (tstrFireName)
                {
                    case "Begin":
                    case "Brave":
                    case "Bless":
                    case "Clear":
                        strNewType = string.Copy(tstrFireName);
                        hovering = true;
                        break;
                    default:
                        hovering = false;
                        break;
                }
            }
            else
            {
                hovering = false;
            }
        }

        public void MoveToHammering()
        {
            Hammering.SetActive(true);
            Hammering.GetComponent<Hammering>().SetType(resultType);

            transform.parent.gameObject.SetActive(false);
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
