using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //delegate - 
    public delegate void OnInputValueChanged(Vector2 inputVal);
    public delegate void OnStickTapped();

    public event OnInputValueChanged onInputValueChanged;
    public event OnStickTapped onStickTapped;

    [SerializeField]
    RectTransform thumbstick;
    [SerializeField]
    RectTransform background;
    [SerializeField]
    float deadZone = 0.2f;
    bool wasDragging = false;
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 touchPos = eventData.position;
        Vector3 thumbstickLocalOffset = Vector3.ClampMagnitude(touchPos - background.position, background.sizeDelta.x/2f);

        thumbstick.localPosition = thumbstickLocalOffset;
        Vector2 outputVal = thumbstickLocalOffset / background.sizeDelta.y * 2f;
        if (outputVal.magnitude > deadZone) // out put value has to be bigger than the dead zone before triggering the input value delegate.
        {
            onInputValueChanged?.Invoke(outputVal);
        }
        wasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.localPosition = Vector2.zero;
        thumbstick.localPosition = Vector2.zero;
        onInputValueChanged?.Invoke(Vector2.zero);
        //put it here
        if(wasDragging)
        {
            wasDragging = false;
            return;
        }

        onStickTapped?.Invoke();
    }
}
