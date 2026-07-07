using UnityEngine;

public class FaceSwapper : MonoBehaviour
{
    // To swap between the sprites
    [SerializeField] private SpriteRenderer faceRenderer;
    // Background circle
    [SerializeField] private SpriteRenderer circleBackground;
    // Holds all the different sprite faces
    [SerializeField] private Sprite[] faces;
    // Holds if you want to unlock everything
    [SerializeField] private bool fullUnlock = false;

    private int currentIndex = 0;
    private int amountUnlocked = 0;

    private void Start()
    {
        if (fullUnlock)
        {
            amountUnlocked = faces.Length;
        }
        else
        {
            amountUnlocked = PlayerPrefs.GetInt("levels_complete", 0);
        }

        if (circleBackground != null)
        {
            float hue = PlayerPrefs.GetFloat("hue", 0f);
            float sat = PlayerPrefs.GetFloat("sat", 0f);
            float val = PlayerPrefs.GetFloat("val", 1f);

            Color colour = Color.HSVToRGB(hue, sat, val);
            circleBackground.color = colour;
        }


        int savedFace = PlayerPrefs.GetInt("selected_face", 0);
        currentIndex = PlayerPrefs.GetInt("selected_face", 0);
        SetFace(savedFace);
    }

    public void SetFace(int index)
    {
        currentIndex = index;
        faceRenderer.sprite = faces[index];

        PlayerPrefs.SetInt("selected_face", currentIndex);
        PlayerPrefs.Save();
    }

    // next face browsing
    public void NextFace()
    {
        // wraps around
        int next = currentIndex + 1;

        if (next > amountUnlocked)
        {
            next = 0;
        }
        SetFace(next);
    }

    public void PreviousFace()
    {
        int prev = currentIndex - 1;

        if (prev < 0)
        {
            prev = amountUnlocked;
        }

        SetFace(prev);
    }
}
