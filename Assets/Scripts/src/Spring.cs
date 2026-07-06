using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Spring : MonoBehaviour
{
    [SerializeField] float springConstant;
    float displacement; // positive displacement means being stretched, negative means compressed
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    [SerializeField] SpringLockSide springLock = SpringLockSide.LockNone;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDisplacement(float newDisplacement)
    {
        // distance the centre of the object is displaced with new spring displacement, scaled with transform
        float centreOffsetScaled = 0.5f * (newDisplacement - displacement) * transform.localScale.x;
        switch (springLock)
        {
            case SpringLockSide.LockLeft:
                transform.position += centreOffsetScaled * transform.right;
            break;
            case SpringLockSide.LockRight:
                transform.position -= centreOffsetScaled * transform.right;
            break;
            // LockNone doesn't apply any change in position
        }

        displacement = newDisplacement;

        // update visual stretch according to displacement
        // increased scale decreases visual stretch 
        spriteRenderer.size = new Vector2(1.0f + displacement / transform.localScale.x, 1.0f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 displacementDirection = transform.right;
            Debug.Log(displacementDirection);

            BallPhysics.BallPhysicsInstance.ApplyForce(springConstant * displacement * displacementDirection); 

            SetDisplacement(0);
        }
    }
}
