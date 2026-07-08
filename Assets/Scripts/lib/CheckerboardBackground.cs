using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0.1f); // diagonal
    [SerializeField] private Vector2 tiling = new Vector2(10f, 10f);        // how many repeats

    private RawImage rawImage;
    private Vector2 offset;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        // uvRect: (offsetX, offsetY, width, height) in UV space
        rawImage.uvRect = new Rect(offset.x, offset.y, tiling.x, tiling.y);
    }
}