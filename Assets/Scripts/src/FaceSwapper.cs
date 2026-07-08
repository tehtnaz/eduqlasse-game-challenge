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
    // Enables randoms
    [SerializeField] private bool randomizeFace = false;

    private int currentIndex = 0;
    private int amountUnlocked = 0;

    private void Start()
    {
        if (randomizeFace)
        {
            Randomize();
            MusicManager.Instance.PlayMusic(SongNames.Select_Level);
        }
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

    public void Randomize()
    {
        // random face from ALL faces, ignoring unlock gating
        int randomFace = Random.Range(0, faces.Length);
        SetFace(randomFace);

        // random colour
        float hue = Random.value;
        float sat = Random.Range(0.5f, 1f);
        float val = Random.Range(0.7f, 1f);

        Color colour = Color.HSVToRGB(hue, sat, val);
        if (circleBackground != null)
        {
            circleBackground.color = colour;
        }

        // persist the colour so it matches everywhere the ball appears
        PlayerPrefs.SetFloat("hue", hue);
        PlayerPrefs.SetFloat("sat", sat);
        PlayerPrefs.SetFloat("val", val);
        PlayerPrefs.Save();
    }

    // Fully Reset Player
    public void ResetToDefault()
    {
        // face 0
        SetFace(0);

        // white = full value, zero saturation (HSV 0, 0, 1)
        float hue = 0f;
        float sat = 0f;
        float val = 1f;

        if (circleBackground != null)
        {
            circleBackground.color = Color.HSVToRGB(hue, sat, val);
        }

        PlayerPrefs.SetFloat("hue", hue);
        PlayerPrefs.SetFloat("sat", sat);
        PlayerPrefs.SetFloat("val", val);
        PlayerPrefs.Save();
    }
}
