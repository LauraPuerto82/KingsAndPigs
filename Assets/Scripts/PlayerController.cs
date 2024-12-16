using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transform;

    [SerializeField] private float speed;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_gatherInput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
    }    
    
    void FixedUpdate()
    {
        m_rigidbody2D.linearVelocity = new Vector2(m_gatherInput.ValueX * speed, m_rigidbody2D.linearVelocityY);
    }
}
