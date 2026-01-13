using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TouchMove2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Visual")]
    public Transform localob;
    public Animator anim;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StopMove();
    }
    void FixedUpdate()
    {
        Move();
    }

    // --------------------
    // MOVE LOGIC
    // --------------------

    void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;

        if (moveDirection != Vector2.zero)
        {
            anim.Play("ChickenMove");
        }
        else
        {
            anim.Play("ChickenState");
        }
    }

    // --------------------
    // BUTTON EVENTS
    // --------------------

    public void MoveUpDown()
    {
        moveDirection = Vector2.up;
    }

    public void MoveDownDown()
    {
        moveDirection = Vector2.down;
    }

    public void MoveLeftDown()
    {
        moveDirection = Vector2.left;
        if (localob) localob.localScale = new Vector3(-1, 1, 1);
    }

    public void MoveRightDown()
    {
        moveDirection = Vector2.right;
        if (localob) localob.localScale = new Vector3(1, 1, 1);
    }

    public void StopMove()
    {
        moveDirection = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }
}
