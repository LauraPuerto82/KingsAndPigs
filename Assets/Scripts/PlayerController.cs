using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transform;
    private Animator m_animator;

    // Values
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private int direction = 1;

    // Animator values
    private int idSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_gatherInput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();        
    }

    private void Update()
    {
        SetAnimatorValues();
    }

    private void SetAnimatorValues()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
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
            m_rigidbody2D.linearVelocity = new Vector2(x, y);
            m_gatherInput.IsJumping = false;
        }
    }
}
