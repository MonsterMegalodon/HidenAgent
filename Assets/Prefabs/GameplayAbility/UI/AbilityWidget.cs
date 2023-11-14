using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image icon;
    [SerializeField] Image cooldownWheel;

    [SerializeField] float highlightedScale = 1.5f;
    [SerializeField] float scaleSpeed = 20f;
    [SerializeField] float highlightOffset = 200f;

    [SerializeField] RectTransform widgetRoot;

    Vector3 goalScale = Vector3.one;
    Vector3 goalOffset = Vector3.zero;

    //amt == 0 means not scaled up, amt == 1 means scaled to 1.5
    public void SetScaleAmount(float amt)
    {
        goalScale = Vector3.one * (1 + amt * (highlightedScale - 1));
        goalOffset = Vector3.left * amt * highlightOffset;
    }

    internal void Init(Ability ability)
    {
        this.ability = ability;
        icon.sprite = ability.GetIcon();
        ability.onCooldownStarted += StartCooldown;
    }

    private void StartCooldown(float cooldownDuration)
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration));
    }

    IEnumerator CooldownCoroutine(float cooldownDuration)
    {
        float cooldownTimeLeft = cooldownDuration;
        while (cooldownTimeLeft > 0)
        {
            cooldownTimeLeft -= Time.deltaTime;
            cooldownWheel.fillAmount = cooldownTimeLeft / cooldownDuration;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ActivateAbility()
    {
        ability.ActivateAbility();
    }

    private void Update()
    {
        widgetRoot.transform.localPosition = Vector3.Lerp(widgetRoot.transform.localPosition, goalOffset, Time.deltaTime * scaleSpeed);
        widgetRoot.transform.localScale = Vector3.Lerp(widgetRoot.transform.localScale, goalScale, Time.deltaTime * scaleSpeed);
    }
}
