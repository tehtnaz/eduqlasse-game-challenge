using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallPhysics : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider;

    // To save the pre-paused velocity to return to that state afterwards
    private Vector2 savedVelocity;
    private float savedAngularVelocity;

    void Awake()
    {
        BallPhysicsInstance = this; // singleton
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // void FixedUpdate() {   }

    public void Pause()
    {
        // Cache motion
        savedVelocity = rigidbody2D.linearVelocity;
        savedAngularVelocity = rigidbody2D.angularVelocity;

        rigidbody2D.Sleep();
        circleCollider.enabled = false;
    }
    public void Unpause()
    {
        rigidbody2D.WakeUp();
        circleCollider.enabled = true;

        rigidbody2D.linearVelocity = savedVelocity;
        rigidbody2D.angularVelocity = savedAngularVelocity;
    }

    public void ApplyForce(Vector2 force)
    {
        Debug.Log("applying x: " + force.x + " y: " + force.y);
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }


    // Singleton implementation
    private static BallPhysics ballPhysicsInstance;
    public static BallPhysics BallPhysicsInstance
    {
        get
        {
            return ballPhysicsInstance;
        }
        set
        {
            if(ballPhysicsInstance == null)
            {
                ballPhysicsInstance = value;
            }
            else
            {
                // prevents singleton from being overriden
                Debug.LogWarning("Multiple BallPhysics instances detected! The duplicate will de destroyed.");
                Destroy(value);
            }
        }
    }
    void OnDestroy()
    {
        if(this == ballPhysicsInstance) ballPhysicsInstance = null;
    }
}
