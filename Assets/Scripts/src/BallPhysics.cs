using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallPhysics : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    void Awake()
    {
        BallPhysicsInstance = this; // singleton
        
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // void FixedUpdate() {   }

    public void Pause()
    {
        rigidbody2D.Sleep();
    }
    public void Unpause()
    {
        rigidbody2D.WakeUp();
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
