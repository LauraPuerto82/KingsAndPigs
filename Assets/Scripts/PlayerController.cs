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
    }

    private void Move()
    {
        Flip();
        m_rigidbody2D.linearVelocity = new Vector2(m_gatherInput.ValueX * speed, m_rigidbody2D.linearVelocityY);
    }

    private void Flip()
    {
        if(m_gatherInput.ValueX * direction < 0)
        {
            m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
}
