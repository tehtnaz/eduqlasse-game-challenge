using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingBackground : MonoBehaviour
{
    // diagonal
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0.1f);
    // how many repeats
    [SerializeField] private Vector2 tiling = new Vector2(10f, 10f);

    private RawImage rawImage;
    private Vector2 offset;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;

        float aspect = rawImage.rectTransform.rect.width / rawImage.rectTransform.rect.height;

        float tileY = 10f;
        // Forces them to stay square
        float tileX = tileY * aspect;
        // uvRect: (offsetX, offsetY, width, height) in UV space
        rawImage.uvRect = new Rect(offset.x, offset.y, tileX, tileY);
    }
}