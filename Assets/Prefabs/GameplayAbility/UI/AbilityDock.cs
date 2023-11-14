using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityComponent owningAbilityComponent;
    [SerializeField] AbilityWidget abilityWidgetPrefab;
    [SerializeField] RectTransform widgetRoot;
    List<AbilityWidget> abilityWidgets = new List<AbilityWidget>();

    PointerEventData touchData;
    AbilityWidget highlightedAbilityWidget;
    private void Awake()
    {
        owningAbilityComponent.onNewAbilityAdded += AddNewAbilityWidget;
    }

    private void AddNewAbilityWidget(Ability ability)
    {
        AbilityWidget newWidget = Instantiate(abilityWidgetPrefab, widgetRoot);
        newWidget.Init(ability);
        abilityWidgets.Add(newWidget);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(highlightedAbilityWidget!=null)
        {
            highlightedAbilityWidget.ActivateAbility();
        }

        touchData = null;
    }

    private void Update()
    {
        if(touchData!=null)
        {
            highlightedAbilityWidget = GetWidgetFromTouchData();
        }
    }

    private AbilityWidget GetWidgetFromTouchData()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(touchData, results);
        foreach(RaycastResult result in results)
        {
            AbilityWidget foundWdiget = result.gameObject.GetComponent<AbilityWidget>();
            if(foundWdiget!=null)
            {
                return foundWdiget;
            }
        }
        return null;
    }
}
