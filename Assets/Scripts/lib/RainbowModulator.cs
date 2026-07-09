using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RainbowModulator : MonoBehaviour
{
    // Cycle speed
    [SerializeField] private float speed = 0.3f;

    private SpriteRenderer spriteRenderer;
    private float hue;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        hue += speed * Time.deltaTime;
        // wrap back to red
        if (hue > 1f) hue -= 1f;

        spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}
