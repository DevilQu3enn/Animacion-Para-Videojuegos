using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFinal : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 7f;
    public float turnSpeed = 10f;
    public float continueComboTime = 1f;
    private float comboTime;

    private Rigidbody rb;
    private Animator anim;

    private int attackIndex = 0;
    private bool comboActive;
    private bool attackInputLocked;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if(!comboActive)
            Move();
        Attack();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed;

        Vector3 velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = velocity;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }



        if (moveDirection.magnitude > 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    void Attack(){
        if(Input.GetButtonDown("Fire1")){
            if(!comboActive){
                attackIndex = 0;
            }else{
                if(attackIndex + 1 > 2){
                    attackIndex = 0;
                }else{
                    attackIndex++;
                }
            }
            anim.SetTrigger("ContinueAttack");
            anim.SetBool("IsAttacking", true);
            comboActive = true;
            comboTime = continueComboTime;
        }

        if(comboActive){
            comboTime -= Time.deltaTime;
            if(comboTime <= 0){
                comboActive = false;
                anim.SetBool("IsAttacking", false);
            }
        }
    }
}
