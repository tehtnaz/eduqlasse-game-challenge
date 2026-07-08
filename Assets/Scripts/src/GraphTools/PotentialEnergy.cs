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
        graph.SetDataRange(0, rigidbody.mass * Physics2D.gravity.y * (maxHeight - minHeight));
    }
    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(rigidbody.mass * Physics2D.gravity.y * (rigidbody.position.y - minHeight));
    }
}
