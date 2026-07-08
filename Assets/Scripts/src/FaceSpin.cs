using UnityEngine;

public class FaceSpin : MonoBehaviour
{
    // the ball's rigidbody
    [SerializeField] private Rigidbody2D playerBody;
    // the face sprite to rotate
    [SerializeField] private Transform faceTransform;
    // Spin amount
    [SerializeField] private float spinFactor = 20f;

    void Update()
    {
        float speed = playerBody.linearVelocity.magnitude * Mathf.Sign(playerBody.linearVelocityX);

        // rotate with how fast the player circle is moving
        faceTransform.Rotate(0f, 0f, -speed * spinFactor * Time.deltaTime);

    }
}
