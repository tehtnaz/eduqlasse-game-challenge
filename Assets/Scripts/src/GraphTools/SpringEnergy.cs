using UnityEngine;

public class SpringEnergy : MonoBehaviour
{
    [SerializeField] Spring spring;
    [SerializeField] BarGraph graph;

    void Start()
    {
        // maybe change the data range later in case we display the energy units in the UI
        graph.SetDataRange(0, 0.5f * spring.SpringConstant * spring.Displacement * spring.Displacement);
    }
    // Update is called once per frame
    void Update()
    {
        graph.SetDataValue(0.5f * spring.SpringConstant * spring.Displacement * spring.Displacement);
    }
}
