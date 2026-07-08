using UnityEngine;

public class ResetPlayerAvater : MonoBehaviour
{
    [SerializeField] private FaceSwapper faceSwapper;

    // Resets the avatar of the player ball
    public void ResetAvatar()
    {
        if (faceSwapper != null)
        {
            faceSwapper.ResetToDefault();
        }
    }
}
