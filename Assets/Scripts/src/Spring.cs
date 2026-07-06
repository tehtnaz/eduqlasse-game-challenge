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
            Vector2 normal = collision.GetContact(0).normal;
            Debug.Log(normal);
            BallPhysics.BallPhysicsInstance.ApplyForce(springConstant * displacement * normal); 

            rigidbody2D.MovePosition(rigidbody2D.position + normal * displacement); // maybe delete this later
            displacement = 0;
            
        }
    }
}
