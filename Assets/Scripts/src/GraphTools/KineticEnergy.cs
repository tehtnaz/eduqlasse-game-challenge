using UnityEngine;

public class KineticEnergy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] BarGraph graph;

    void Start()
    {
        graph.SetDataRange(0, 0.5f * rigidbody.mass * (16 * 16));
    }

    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(0.5f * rigidbody.mass * rigidbody.linearVelocity.sqrMagnitude);
    }
}
