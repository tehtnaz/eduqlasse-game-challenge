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
        // modify range by 1.25 so we can display more kinetic while keeping proportions the same between kinetic and potentials 
        graph.SetDataRange(0, rigidbody.mass * Physics2D.gravity.y * (maxHeight - minHeight)*2.696f);
    }
    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(rigidbody.mass * Physics2D.gravity.y * (rigidbody.position.y - minHeight));
    }
}
