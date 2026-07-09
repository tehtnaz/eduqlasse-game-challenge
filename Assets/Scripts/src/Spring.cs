using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Spring : MonoBehaviour
{
    [SerializeField] SpringLockSide springLock = SpringLockSide.LockNone;
    
    // this is double so we can try to improve accuracy
    [SerializeField] double springConstant;

    public float SpringConstant
    {
        get
        {
            return (float)springConstant;
        } 
        private set
        {
            springConstant = value;
        }
    }

    // this is double so we can try to improve accuracy
    [SerializeField] double displacement; // positive displacement means being stretched, negative means compressed
    public float Displacement {
        get
        {
            return (float)displacement;
        }
        private set
        {
            SetDisplacement(value, false);
        }
    } 
    SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D triggerBox;
    [SerializeField] BoxCollider2D boxCollider;
    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpringConstant(float newSpringConstant)
    {
        springConstant = newSpringConstant;
    }

    public void SetForceAtCurrentDisplacement(float force)
    {
        springConstant = force / -displacement;
    }

    public void Pause()
    {
        triggerBox.enabled = false;
        boxCollider.enabled = false;
    }
    public void Unpause()
    {
        triggerBox.enabled = true;
        boxCollider.enabled = true;

        Debug.Log("Expected energy: " + (0.5f*springConstant*displacement*displacement).ToString() + " J");
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
        // pretend we are paused so we dont hit ball again nor activate this script again
        Pause();

        // lock left side so it visually makes sense while animating (left, non-pushing side is fixed)
        springLock = SpringLockSide.LockLeft;

        // animate change in displacement over some time (useful for graphs)
        float startingDisplacement = (float)displacement;
        const float timeToAnimate = 0.5f;
        for(float t = 0; t < timeToAnimate; t += Time.deltaTime)
        {
            SetDisplacement(startingDisplacement * SpringCurve(t / timeToAnimate), false);
            yield return new WaitForEndOfFrame();
        }

        // assure displacement is exactly zero
        SetDisplacement(0, false);

        // re-enable colliders
        Unpause();
        
    }

    public void SetDisplacementAndAlignBall(float newDisplacement)
    {
        SetDisplacement(newDisplacement, true);
    }

    void SetDisplacement(double newDisplacement, bool alignBall)
    {
        // distance the centre of the object is displaced with new spring displacement, scaled with transform
        double centreOffsetScaled = 0.5 * (newDisplacement - displacement) * transform.localScale.x;


        switch (springLock)
        {
            case SpringLockSide.LockLeft:
                transform.position += (float)centreOffsetScaled * transform.right;
                if (alignBall)
                {
                    BallPhysics.BallPhysicsInstance.transform.position += 2.0f * (float)centreOffsetScaled * transform.right;
                }
            break;
            case SpringLockSide.LockRight:
                transform.position -= (float)centreOffsetScaled * transform.right;
            break;
            // LockNone doesn't apply any change in position
        }

        displacement = newDisplacement;

        // update visual stretch according to displacement
        // increased scale decreases visual stretch 
        spriteRenderer.size = new Vector2(1.0f + ((float)displacement + 0.3f) / transform.localScale.x, 1.0f);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && displacement != 0)
        {
            Vector2 displacementDirection = transform.right * Mathf.Sign(-(float)displacement);
            Debug.Log(displacementDirection);

            // apply spring force on ball singleton
            BallPhysics.BallPhysicsInstance.ApplyKineticEnergy(springConstant * displacement * displacement, displacementDirection); 

            StartCoroutine(AnimateSpring());
        }
    }
}
