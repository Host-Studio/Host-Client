using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Purifying : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // 드래그
        public static GameObject itemBeingDragged;
        private Vector3 startPosition;

        // 성수 탐지용 레이
        private float distance;
        private RaycastHit2D rayHit;
        private Ray2D ray;

        // 호버링
        [SerializeField] private Image gaugeBar;
        private bool hovering = false;
        private string strOldType = "Old";
        private string strNewType = "New";
        private float hoveringTime = 2f;
        private int iLayerMask;

        private bool done = false;
        private string resultType = "";

        void OnEnable()
        {
            print("스타트");

            gaugeBar.fillAmount = 0;
            iLayerMask = ~(LayerMask.GetMask("Ignore Raycast"));

            hovering = false;
            strOldType = "Old";
            strNewType = "New";
            hoveringTime = 2f;
            done = false;
            resultType = "";
        }

        void FixedUpdate()
        {
            if (done)
            {
                return;
            }

            if (hovering)
            {
                if (strOldType != strNewType)
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
            transform.position = Input.mousePosition;

            rayHit = Physics2D.Raycast(Input.mousePosition, Vector3.forward, Mathf.Infinity, iLayerMask);
            if (rayHit)
            {
                string tstrFireName = rayHit.transform.name;
                switch (tstrFireName)
                {
                    case "Burn":
                    case "Corrosion":
                    case "Mind Break":
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

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            transform.position = startPosition;

            hovering = false;
        }

        public void Finish()
        {

        }
    }
}
