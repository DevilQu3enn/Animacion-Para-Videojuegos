using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerFinal : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 7f;
    public float turnSpeed = 10f;
    public float continueComboTime = 1f;


    private Rigidbody rb;
    private Animator anim;
    private CharacterStatus characterStatus;

    private bool isAttacking;
    private bool isContinuingCombo;
    private bool isAttackInputLocked;

    [SerializeField] private PlayerDamage playerPunchDamage;
    [SerializeField] private PlayerDamage playerKickDamage;

    [SerializeField] private float softPunchDamage;
    [SerializeField] private float strongPunchDamage;
    [SerializeField] private float softKickDamage;
    [SerializeField] private float strongKickDamage;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        characterStatus = GetComponent<CharacterStatus>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!isAttacking)
            Move();

        Attack();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Entrada de movimiento en base al forward del personaje
        Vector3 localMoveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 moveDirection = transform.TransformDirection(localMoveDirection) * moveSpeed;

        // Aplicar la velocidad al Rigidbody
        Vector3 velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = velocity;

        // Rotación del personaje solo si el movimiento es hacia adelante o lateral
        if (localMoveDirection.z >= 0 && localMoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // Activar o desactivar la animación de movimiento
        anim.SetBool("IsMoving", localMoveDirection.magnitude > 0);
    }



    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && characterStatus.Stamina >= 10 && !isAttackInputLocked)
        {
            isAttackInputLocked = true;
            characterStatus.UpdateStamina(10);
            if (isAttacking && !isContinuingCombo)
            {
                isContinuingCombo = true;
            }

            isAttacking = true;
            anim.SetBool("PunchAttack", true);
            anim.SetBool("IsAttacking", isAttacking);
        }

        if (Input.GetButtonDown("Fire2") && characterStatus.Stamina >= 15 && !isAttackInputLocked)
        {
            isAttackInputLocked = true;
            characterStatus.UpdateStamina(15);
            if (isAttacking && !isContinuingCombo)
            {
                isContinuingCombo = true;
            }

            isAttacking = true;
            anim.SetBool("PunchAttack", false);
            anim.SetBool("IsAttacking", isAttacking);
        }

    }

    public void UnlockAttackInput()
    {
        isAttackInputLocked = false;
    }

    public void EndAttack()
    {

        if (!isContinuingCombo)
        {
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
        if (isPunch)
        {
            float damage = severity == DamagePayload.Severity.soft ? softPunchDamage : strongPunchDamage;
            playerPunchDamage.SetValues(damage, severity);
        }
        else
        {
            float damage = severity == DamagePayload.Severity.soft ? softKickDamage : strongKickDamage;
            playerKickDamage.SetValues(damage, severity);
        }
    }
}
