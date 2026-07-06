using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spring : MonoBehaviour
{
    [SerializeField] float springConstant;
    [SerializeField] float displacement; // positive displacement means being stretched, negative means compressed
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 displacementDirection = transform.right;
            Debug.Log(displacementDirection);

            BallPhysics.BallPhysicsInstance.ApplyForce(springConstant * displacement * displacementDirection); 

            rigidbody2D.MovePosition(rigidbody2D.position + displacementDirection * displacement); // TODO: delete this later
            displacement = 0;
        }
    }
}
