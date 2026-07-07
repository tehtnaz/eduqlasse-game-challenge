using UnityEngine;

public class PotentialEnergy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] BarGraph graph;

    // visible y-values are [-5,5] in units
    const float maxHeight = 5;
    const float minHeight = -5; 

    void Start()
    {
        // maybe change the data range later in case we display the energy units in the UI
        graph.SetDataRange(rigidbody.mass * Physics2D.gravity.y * minHeight, rigidbody.mass * Physics2D.gravity.y * maxHeight);
    }
    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(rigidbody.mass * Physics2D.gravity.y * rigidbody.position.y);
    }
}
