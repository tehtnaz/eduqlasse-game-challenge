using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class BallPhysics : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider;

    // To save the pre-paused velocity to return to that state afterwards
    private Vector2 savedVelocity;
    private float savedAngularVelocity;

    // For the boing sound effect
    [SerializeField] public AudioSource audioSource;
    // extra margin to ensure player is off screen
    [SerializeField] private float extraMargin = 0.15f;
    [SerializeField] bool applyPhysicsCorrection;
    // to see if the player is off screen
    public Camera mainCamera;

    void Awake()
    {
        BallPhysicsInstance = this; // singleton

        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        float worldRadius = circleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);

        Vector3 center = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 edge = mainCamera.WorldToViewportPoint(transform.position + Vector3.right * worldRadius);
        float viewportRadius = Mathf.Abs(edge.x - center.x);

        float m = viewportRadius + extraMargin;

        bool fullyOffScreen = center.x < -m || center.x > 1f + m || center.y < -m || center.y > 1f + m || center.z < 0;

        if (fullyOffScreen)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
    }

    public void Pause()
    {
        // Cache motion
        savedVelocity = rigidbody2D.linearVelocity;
        savedAngularVelocity = rigidbody2D.angularVelocity;

        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0;

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

    public void ApplyKineticEnergy(double energyMagnitude, Vector2 energyDirection)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        // for some reason, we can't multiply by 2.0f? (thought that's how it should be?)
        double linearChange = energyMagnitude / rigidbody2D.mass;

        // because of inaccurate unity physics we sometimes lose about 1.7% of the energy if the launch is angled, so we'll slightly correct for that
        if(applyPhysicsCorrection) linearChange *= 1.02;

        Vector2 sqrtChange = energyDirection * (float)Math.Sqrt(linearChange);

        rigidbody2D.linearVelocity += sqrtChange;

        Debug.Log("Real energy: " + (0.5f*rigidbody2D.mass*rigidbody2D.linearVelocity.sqrMagnitude).ToString() + " J");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("KillPlayer"))
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
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
            if (ballPhysicsInstance == null)
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
        if (this == ballPhysicsInstance) ballPhysicsInstance = null;
    }
}
