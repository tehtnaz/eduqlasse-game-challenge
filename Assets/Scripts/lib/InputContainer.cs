using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputContainer : MonoBehaviour
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] UnityEvent<float> OnInputUpdated;

    TMP_InputField inputField;
    Slider slider;
    float value;

    // Blocks slider's issue from being null when slider is rebuilt
    void Awake()
    {
        if (OnInputUpdated == null) OnInputUpdated = new UnityEvent<float>();

        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    void Start()
    {
        slider.maxValue = maxValue;
        slider.minValue = minValue;
    }

    // parse and reuse in slider update function
    public void OnFieldUpdate(string newValue)
    {
        OnSliderUpdate(float.Parse(newValue));
    }

    // clamp value, update all components and notify subscribers of update
    public void OnSliderUpdate(float newValue)
    {
        if(newValue == value) return;
        
        value = Mathf.Clamp(newValue, minValue, maxValue);

        // update both child component's values
        slider.value = value;
        inputField.SetTextWithoutNotify(value.ToString());

        OnInputUpdated.Invoke(value);
    }
    
}
