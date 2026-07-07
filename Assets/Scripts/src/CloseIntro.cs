using UnityEngine;

public class CloseIntro : MonoBehaviour
{
    [SerializeField]
    public StateMachine sm = null;
    public void CloseOnClick()
    {
        sm.Change(GameStates.Paused);
    }
}
