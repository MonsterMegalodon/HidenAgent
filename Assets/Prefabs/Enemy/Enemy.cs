using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IMovementInterface, IBTTaskInterface, ITeamInterface
{
    [SerializeField] ValueGuage healthBarPrefab;
    [SerializeField] Transform healthBarAttachTransform;
    [SerializeField] DamageComponent damageComponent;
    [SerializeField] int teamID = 2;
    HealthComponet healthComponet;

    MovementComponent movementComponent;

    ValueGuage healthBar;

    Animator animator;

    Vector3 prevPosition;
    Vector3 velocity;

    NavMeshAgent agent;

    private void Awake()
    {
        healthComponet = GetComponent<HealthComponet>();
        healthComponet.onTakenDamage += TookDamage;
        healthComponet.onHealthEmpty += StartDealth;
        healthComponet.onHealthChanged += HealthChanged;

        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
        UIAttachComponent attachmentComp = healthBar.AddComponent<UIAttachComponent>();
        attachmentComp.SetupAttachment(healthBarAttachTransform);
        movementComponent = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();
        damageComponent.SetTeamInterface(this);
        agent = GetComponent<NavMeshAgent>();
    }

    public int GetTeamID() { return teamID; }

    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        healthBar.SetValue(currentHealth, maxHealth);
    }

    private void StartDealth(float delta, float maxHealth)
    {
        Destroy(healthBar.gameObject);
        animator.SetTrigger("die");
        GetComponent<AIController>().StopAILogic();
    }

    private void TookDamage(float currentHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"Took Damage {delta}, now health is: {currentHealth}/{maxHealth}");
    }

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CaclucateVelocity();
    }

    private void CaclucateVelocity()
    {
        velocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;
        animator.SetFloat("speed", velocity.magnitude);
    }

    public void RotateTowards(Vector3 direction)
    {
        movementComponent.RotateTowards(direction);
    }

    public void RotateTowards(GameObject target)
    {
        movementComponent.RotateTowards(target.transform.position - transform.position);
    }

    public void AttackTarget(GameObject target)
    {
        animator.SetTrigger("attack");
    }

    public void AttackPoint()
    {
        damageComponent.DoDamage();
    }

    public void DeathAnimationFinished()
    {

        Destroy(gameObject);
    }

    public float GetMoveSpeed()
    {
        return agent.speed;
    }

    public void SetMoveSpeed(float speed)
    {
       agent.speed = speed;
    }
}
