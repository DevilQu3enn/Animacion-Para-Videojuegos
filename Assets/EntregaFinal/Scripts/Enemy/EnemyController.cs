using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterStatus characterStatus;
    [SerializeField] private GameObject target;
    [SerializeField] private float attackRange;
    Rigidbody rb;
    Animator anim;


    bool isAttacking;
    bool isContinuingCombo;
    [SerializeField] private PlayerDamage playerPunchDamage;
    [SerializeField] private float softPunchDamage;
    [SerializeField] private float strongPunchDamage;
    [SerializeField] private float restTime;
    private float restTimer;

    private int attacksToPerform;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        characterStatus = GetComponent<CharacterStatus>();
    }
    private EnemyState currentState;
    public enum EnemyState
    {
        idle,
        chasing,
        attacking
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.chasing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.idle:
                Idle();
                break;
            case EnemyState.chasing:
                Chasing();
                break;
            case EnemyState.attacking:
                Attacking();
                break;
        }

    }

    private void Idle()
    {
        agent.isStopped = true;
        rb.velocity = Vector3.zero;

        if (restTimer > restTime)
        {
            currentState = EnemyState.chasing;
            restTimer = 0;
        }
        else
        {
            restTimer += Time.deltaTime;
        }
    }

    private void Chasing()
    {
        if (target != null)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            anim.SetBool("IsMoving", true);

            if (Vector3.Distance(target.transform.position, agent.transform.position) < attackRange)
            {
                agent.isStopped = true;
                rb.velocity = Vector3.zero;
                attacksToPerform = Random.Range(1, 5);
                currentState = EnemyState.attacking;
                anim.SetBool("IsMoving", false);
            }
        }
    }

    private void Attacking()
    {
        bool canAttack = Vector3.Distance(target.transform.position, agent.transform.position) > attackRange;

        if (isAttacking && !isContinuingCombo && !canAttack)
        {
            isContinuingCombo = true;
        }

        if(attacksToPerform <= 0){
            attacksToPerform = 0;
            currentState = EnemyState.idle;
            return;
        }


        if (canAttack)
        {
            if (!isAttacking)
                currentState = EnemyState.chasing;
        }
        else
        {
            Debug.Log("Attacking");
            rb.velocity = Vector3.zero;
            isAttacking = true;
            anim.SetBool("IsAttacking", isAttacking);
            anim.SetBool("PunchAttack", true);
            isContinuingCombo = false;
        }
    }

    public void EndAttack()
    {
        attacksToPerform--;
        characterStatus.UpdateStamina(10);
        if (!isContinuingCombo)
        {
            Debug.Log("Acabado ataque");
            isAttacking = false;
            anim.SetBool("IsAttacking", isAttacking);
            anim.SetBool("PunchAttack", false);
        }
        else
        {
            isContinuingCombo = false;
        }

    }

    public void SetUpDamage(bool isPunch, DamagePayload.Severity severity)
    {
        float damage = severity == DamagePayload.Severity.soft ? softPunchDamage : strongPunchDamage;
        playerPunchDamage.SetValues(damage, severity);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
