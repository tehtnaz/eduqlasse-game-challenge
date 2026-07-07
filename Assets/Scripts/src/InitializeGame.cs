using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InitializeGame : MonoBehaviour
{
    // Canvas that holds the entire UI
    [SerializeField] public Transform canvas;
    // For debugging, unlocks all the leves if set to true
    [SerializeField] public bool unlock = false;

    // Game initializes all levels and the level buttons
    void Start()
    {
        if (unlock)
        {
            return;
        }

        if (!PlayerPrefs.HasKey("selected_face"))
        {
            PlayerPrefs.SetInt("selected_face", 0);
            PlayerPrefs.Save();
        }

        for (int level_num = 1; level_num <= 8; level_num++)
        {
            string key = $"level_{level_num}_complete";

            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetInt(key, 0);
            }
        }
        PlayerPrefs.SetInt($"level_1_complete", 1);
        PlayerPrefs.Save();

        for (int i = 1; i <= 8; i++)
        {
            Transform buttonTransform = canvas.Find($"Button ({i})");

            if (buttonTransform != null)
            {
                Button button = buttonTransform.GetComponent<Button>();

                if (PlayerPrefs.GetInt($"level_{i}_complete", 0) == 0)
                {
                    // Grab the text from the button
                    TMP_Text label = button.GetComponentInChildren<TMP_Text>();
                    string current = label.text;

                    // Lock it
                    label.text = "Locked";
                    button.enabled = false;
                }
            }
        }
    }
}
