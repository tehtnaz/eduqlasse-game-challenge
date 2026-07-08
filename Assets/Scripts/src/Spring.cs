using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Spring : MonoBehaviour
{
    [SerializeField] SpringLockSide springLock = SpringLockSide.LockNone;
    
    [SerializeField] float springConstant;
    public float SpringConstant
    {
        get
        {
            return springConstant;
        } 
        private set
        {
            springConstant = value;
        }
    }
    [SerializeField] float displacement; // positive displacement means being stretched, negative means compressed
    public float Displacement {
        get
        {
            return displacement;
        }
        private set
        {
            SetDisplacement(value);
        }
    } 
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetSpringConstant(float newSpringConstant)
    {
        springConstant = newSpringConstant;
    }

    public void Pause()
    {
        boxCollider.enabled = false;
    }
    public void Unpause()
    {
        boxCollider.enabled = true;
    }

    // cool curve function i came up with on desmos
    float SpringCurve(float x)
    {
        const float t = 2.5f;
        const float inverseT = 1/2.5f;
        return -t*(x-1)*(x-1)*(x-inverseT);
    }

    IEnumerator AnimateSpring()
    {
        // disable collider so we dont hit ball again
        boxCollider.enabled = false;

        // animate change in displacement over some time (useful for graphs)
        float startingDisplacement = displacement;
        const float timeToAnimate = 0.5f;
        for(float t = 0; t < timeToAnimate; t += Time.deltaTime)
        {
            SetDisplacement(startingDisplacement * SpringCurve(t / timeToAnimate));
            yield return new WaitForEndOfFrame();
        }

        // assure displacement is exactly zero
        SetDisplacement(0);

        // re-enable collider
        boxCollider.enabled = true;
        
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
        if (collision.collider.CompareTag("Player") && displacement != 0)
        {
            Vector2 displacementDirection = transform.right;
            Debug.Log(displacementDirection);

            // apply spring force on ball singleton
            BallPhysics.BallPhysicsInstance.ApplyForce(-springConstant * displacement * displacementDirection); 

            StartCoroutine(AnimateSpring());
        }
    }
}
