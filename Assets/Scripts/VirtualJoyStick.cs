using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image bgImg; // joystick background
    private Image joystickImg; // joystick
    private Vector3 inputVector; // 이동 벡터값

    
    //InPointerEventData >> 터치위치, 드래그 여부, 카메라, 클릭횟수, 클릭시간, 마우스 클릭정보 등
    //when drag, Operation
    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    
    //when touch started, Operation
    public void OnPointerDown(PointerEventData eventData)
    {
        //OnDrag(eventData);
    }

    //when touch ended, Operation
    public void OnPointerUp(PointerEventData eventData)
    {
        //Handle.rectTransform.anchoredPosition = Vector2.zero;
    }
}