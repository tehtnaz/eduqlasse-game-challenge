using UnityEngine;

public class SpringRotator : MonoBehaviour
{
    public void SetZRotation(float degree)
    {
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }
}
