using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BarGraph : MonoBehaviour
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetDataRange(float minimum, float maximum)
    {
        minValue = minimum;
        maxValue = maximum;
    }

    // rescale bar graph according to new value
    public void SetDataValue(float value)
    {
        if (rectTransform != null)
        {
            float range = maxValue - minValue;
            if (Mathf.Approximately(range, 0f))
            {
                return;
            }

            rectTransform.localScale = new Vector2(rectTransform.localScale.x, (value - minValue) / (maxValue - minValue));
        }
    }

    // The following is useful for testing the script
    // [SerializeField] float dataValue;
    // void Update()
    // {
    //     SetDataValue(dataValue);
    // }
}
