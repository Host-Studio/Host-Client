using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class TimingBar : MonoBehaviour
    {
        public Quenching quenching;

        [SerializeField] [Range(0f, 10f)]   private float speed = 1f;
        [SerializeField] [Range(0f, 5f)]    private float delayTime = 0.5f;
        [SerializeField] [Range(0f, 50f)]   private float reduceLen = 20f;


        [SerializeField] private Transform blueTarget;
        [SerializeField] private Image yellowLine;

        private float length;
        private RectTransform yellowLineRect;

        private float runningTime;
        private float xPos;
        private float yellowLineX, yellowLineY;
        private int count;
        private float blueTargetSizeDeltaX, blueTargetSizeDeltaY;

        private void OnEnable()
        {
            yellowLineRect = yellowLine.rectTransform;
            yellowLineX = yellowLineRect.anchoredPosition.x;
            yellowLineY = yellowLineRect.anchoredPosition.y;

            length = GetComponent<RectTransform>().sizeDelta.x - yellowLineX * 2;
            runningTime = 0f;
            xPos = 0f;
            count = 0;

            print(length);
            print(yellowLineX + " " + yellowLineY);

            StartCoroutine(MoveTimingBar());

            // 타겟의 가로와 세로
            blueTargetSizeDeltaX = blueTarget.GetComponent<RectTransform>().sizeDelta.x;
            blueTargetSizeDeltaY = blueTarget.GetComponent<RectTransform>().sizeDelta.y;
        }

        private void OnDisable()
        {
            blueTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(blueTargetSizeDeltaX, blueTargetSizeDeltaY);
        }

        private IEnumerator MoveTimingBar()
        {
            while(count < 3)
            {
                runningTime += Time.deltaTime * speed;
                xPos = Mathf.Sin(runningTime + Mathf.PI * 1.5f) * (length / 2);

                yellowLineRect.anchoredPosition = new Vector2(xPos + yellowLineX + length / 2, yellowLineY);

                // 클릭하고 2초 정지 후 타겟 크기 줄어듦
                if (Input.GetMouseButtonDown(0))
                {
                    if(CheckYellowLinePosition())
                    {
                        print("perfect");
                        count++;
                        ReduceBlueTargetLen();

                        yellowLineRect.anchoredPosition = new Vector2(yellowLineX, yellowLineY);
                        runningTime = 0;

                        yield return new WaitForSeconds(0.5f);
                    }
                }

                yield return null;
            }

            quenching.Finish();
        }

        private bool CheckYellowLinePosition()
        {
            float targetLeftWidth = blueTarget.GetComponent<RectTransform>().sizeDelta.x;
            //float targetLeftX = blueTarget.position.x - targetLeftWidth / 2;
            //float targetRightX = blueTarget.position.x + targetLeftWidth / 2;
            float targetLeftX = blueTarget.GetComponent<RectTransform>().anchoredPosition.x;
            float targetRightX = targetLeftX + targetLeftWidth;

            //float lineLeftX = yellowLine.transform.position.x;
            //float lineRightX = lineLeftX + yellowLine.GetComponent<RectTransform>().sizeDelta.x;

            float lineLeftX = yellowLineRect.anchoredPosition.x;
            float lineRightX = lineLeftX + yellowLineRect.sizeDelta.x;

            //print("target : " + targetLeftX + ", " + targetRightX);
            //print("line : " + lineLeftX + ", " + lineRightX);

            if (targetLeftX <= lineLeftX && lineRightX <= targetRightX)
            {
                return true;
            }

            return false;
        }

        private void ReduceBlueTargetLen()
        {
            float sizeDeltaX = blueTarget.GetComponent<RectTransform>().sizeDelta.x;
            float sizeDeltaY = blueTarget.GetComponent<RectTransform>().sizeDelta.y;

            blueTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaX - reduceLen, sizeDeltaY);
        }
    }
}
