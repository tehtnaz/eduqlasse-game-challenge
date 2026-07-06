using System;
using System.Reflection.Metadata;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // The games state machine
    public StateMachine sm = null;
    // The text box on screen
    public TextMeshProUGUI screen_text = null;
    // Return to level select button
    public Button return_button = null;
    // Local level Tracker
    public int local_level = 0;

    // for the call about winning
    void OnEnable() => WinBasket.OnWin += HandleWin;
    void OnDisable() => WinBasket.OnWin -= HandleWin;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        return_button.gameObject.SetActive(false);

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

    // Reader function that listens for the signal
    private void HandleWin(bool didWin)
    {
        if (didWin)
        {
            sm.Change(GameStates.Win);
        }
    }
}
