using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    // The games state machine
    public StateMachine sm = null;
    // the text box on screen
    public TextMeshProUGUI screen_text = null;

    public static event Action<bool> OnWin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sm != null)
        {
            sm.Add(GameStates.Paused, new GamePausedState());
            sm.Add(GameStates.Play, new GamePlayState());
            sm.Add(GameStates.Restart, new GameRestartState());
            sm.Add(GameStates.Win, new GameWinState());

            sm.Change(GameStates.Paused);
        }
    }

    // Update is called once per frame
    public void Change_Text(string text)
    {
        screen_text.text = text;
    }
}
