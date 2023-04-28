using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowMouseObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isClicked = false;
    private Vector3 offset;

    private void Update()
    {
        if (isClicked)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;
            transform.position = targetPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}