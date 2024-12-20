using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Player Components ")]
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transform;
    private Animator m_animator;

    [Header(" Movement Settings ")]
    [SerializeField] private float speed;    
    private int direction = 1;

    [Header(" Jump Settings ")]
    [SerializeField] private float jumpForce;

    [Header(" Extra Jumps Settings ")]
    [SerializeField] private int extraJumps;
    [SerializeField] private int counterExtraJumps;

    [Header(" Animator Values ")]
    private int idSpeed = Animator.StringToHash("speed");
    private int idIsGrounded = Animator.StringToHash("isGrounded");

    [Header(" Raycast Settings ")]
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_gatherInput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();

        leftFoot = GameObject.Find("LeftFoot").GetComponent<Transform>();
        rightFoot = GameObject.Find("RightFoot").GetComponent<Transform>();

        counterExtraJumps = extraJumps;
    }

    private void Update()
    {
        SetAnimatorValues();
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));
        m_animator.SetBool(idIsGrounded, isGrounded);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        CheckGround();
    }

    private void Move()
    {
        Flip();

        float x = m_gatherInput.ValueX * speed;
        float y = m_rigidbody2D.linearVelocityY;
        m_rigidbody2D.linearVelocity = new Vector2(x, y);
    }

    private void Flip()
    {
        if(m_gatherInput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void Jump()
    {
        if(m_gatherInput.IsJumping)
        {
            float x = speed * m_gatherInput.ValueX;
            float y = jumpForce;
            if (isGrounded)
            {                
                m_rigidbody2D.linearVelocity = new Vector2(x, y);                
            }
            if(counterExtraJumps > 0)
            {
                m_rigidbody2D.linearVelocity = new Vector2(x, y);
                counterExtraJumps--;
            }
        }
        m_gatherInput.IsJumping = false;
    }

    private void CheckGround()
    {
        RaycastHit2D leftFootRay = Physics2D.Raycast(leftFoot.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightFootRay = Physics2D.Raycast(rightFoot.position, Vector2.down, rayLength, groundLayer);

        if (leftFootRay || rightFootRay)
        {
            isGrounded = true;
            counterExtraJumps = extraJumps;
        }
        else
        {
            isGrounded = false;
        }
    }
}
