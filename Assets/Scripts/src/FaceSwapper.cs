using System;
using UnityEngine;

public class FaceSwapper : MonoBehaviour
{
    // To swap between the sprites
    [SerializeField] private SpriteRenderer faceRenderer;
    // Holds all the different sprite faces
    [SerializeField] private Sprite[] faces;

    private int currentIndex = 0;

    private void Start()
    {
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
        int next = (currentIndex + 1) % faces.Length;
        SetFace(next);
    }

    public void PreviousFace()
    {
        int prev = (currentIndex - 1 + faces.Length) % faces.Length;
        SetFace(prev);
    }
}
