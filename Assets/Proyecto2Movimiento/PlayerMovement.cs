using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 7f;
    public float turnSpeed = 10f;

    [Range(0f, 1f)]
    public float animationBlend = 0;
    public LayerMask groundLayers;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Move();
        CheckGround();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
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



        if(moveDirection.magnitude > 0){
            anim.SetBool("IsMoving", true);
        }else{
            anim.SetBool("IsMoving", false);
        }

        anim.SetFloat("Speed", animationBlend);
    }

    public void ChangeAnimationBlend(float value){
        animationBlend = value;
    }

    void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); 
        anim.SetTrigger("jump");
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
