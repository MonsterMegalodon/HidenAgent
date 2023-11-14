using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour
{
    [SerializeField] AbilityComponent owningAbilityComponent;
    [SerializeField] AbilityWidget abilityWidgetPrefab;
    [SerializeField] RectTransform widgetRoot;
    List<AbilityWidget> abilityWidgets = new List<AbilityWidget>();
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
}
