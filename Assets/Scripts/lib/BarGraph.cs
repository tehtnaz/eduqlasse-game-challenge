using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BarGraph : MonoBehaviour
{
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] float dataValue;

    void Update()
    {
        SetDataValue(dataValue);
    }

    // rescale bar graph according to new value
    public void SetDataValue(float value)
    {
        rectTransform.localScale = new Vector2(rectTransform.localScale.x, (value - minValue) / (maxValue - minValue));
    }
}
