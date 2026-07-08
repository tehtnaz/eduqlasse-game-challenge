using TMPro;
using UnityEngine;

public class KineticEnergy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] BarGraph graph;
    [SerializeField] float maxExpectedVelocity;

    void Start()
    {
        graph.SetDataRange(0, 0.5f * rigidbody.mass * (maxExpectedVelocity * maxExpectedVelocity));
    }

    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(0.5f * rigidbody.mass * rigidbody.linearVelocity.sqrMagnitude);
    }
}
