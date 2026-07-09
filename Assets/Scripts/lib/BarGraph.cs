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
        if (rectTransform == null) return;

        float range = maxValue - minValue;

        // zero range return
        if (Mathf.Approximately(range, 0f)) return;

        float normalized = (value - minValue) / range;

        // guard against NaN/Infinity from any other source too
        if (float.IsNaN(normalized) || float.IsInfinity(normalized)) return;

        rectTransform.localScale = new Vector3(rectTransform.localScale.x, normalized, 1f);
    }

    // The following is useful for testing the script
    // [SerializeField] float dataValue;
    // void Update()
    // {
    //     SetDataValue(dataValue);
    // }
}
