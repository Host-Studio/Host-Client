using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// J : 포션 제조 씬에서 도구(갈기, 끓이기, 빠히)에 부착하는 스크립트
public class PotionTool : MonoBehaviour, IDropHandler
{
    [SerializeField] private Vector2 moveRange; // J : (아이템이 떨어지기 시작하는 위치, 멈추는 위치) <-y축 기준

    // 필요한 컴포넌트
    [SerializeField] private Animator animator;
    [SerializeField] private PotionManager PotionManager;

    // 이 슬롯에 무언가 마우스 드롭
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            Debug.Log(DragSlot.instance.dragSlot.item.name + " 드롭!");

            DropItem.instance.PotionDrop(transform.position.x, moveRange);
            animator.SetTrigger("Work");
            PotionManager.processed++;
        }
    }
}
