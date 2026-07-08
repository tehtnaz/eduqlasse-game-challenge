using UnityEngine;

public class BreakWithEnergy : MonoBehaviour
{
    [SerializeField] float energyToBreak;

    // this hack uses a trigger instead of a normal collider bc the force i used to negate the bounce was still being negated by the bounce for some reason
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if(0.5f * collider.attachedRigidbody.mass * collider.attachedRigidbody.linearVelocityY * collider.attachedRigidbody.linearVelocityY > energyToBreak)
            {
                // impact is enough to "break" the object
                float speedToReduce = Mathf.Sqrt(energyToBreak * 2.0f / collider.attachedRigidbody.mass);

                // this calculation ensures its reduced whether the velocity is negative or not
                Vector2 velocityToReduce = collider.attachedRigidbody.linearVelocity * (1.0f - speedToReduce / collider.attachedRigidbody.linearVelocity.magnitude);

                collider.attachedRigidbody.AddForce(-velocityToReduce * collider.attachedRigidbody.mass);

                Destroy(this.gameObject);
            }
        }
    }
}
