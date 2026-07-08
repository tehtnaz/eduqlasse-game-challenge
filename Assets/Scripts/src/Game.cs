using System.Collections.Generic;
using TMPro;
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
    // The Level intro
    public GameObject IntroCanvas = null;
    // Local level Tracker
    public int local_level = 0;
    [Header("UI Objects")]
    // All UI which only wants to be shown when paused
    public List<GameObject> uiShownWhenPause;
    // All UI which only wants to be shown when playing
    public List<GameObject> uiShownWhenPlay;
    // Game Started
    public bool started = false;

    // for the call about winning
    void OnEnable() => WinBasket.OnWin += HandleWin;
    void OnDisable() => WinBasket.OnWin -= HandleWin;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroCanvas.SetActive(true);
        return_button.gameObject.SetActive(false);

        if (sm != null)
        {
            sm.Add(GameStates.Paused, new GamePausedState());
            sm.Add(GameStates.Play, new GamePlayState());
            sm.Add(GameStates.Restart, new GameRestartState());
            sm.Add(GameStates.Win, new GameWinState());
            sm.Add(GameStates.Start, new GameStartState());

            sm.Change(GameStates.Start);
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

    // Disable the intro object
    public void DisableIntro()
    {
        started = true;
        IntroCanvas.SetActive(false);
    }

    // hide or show gui which wants to be visible only when paused
    public void ChangePauseUIVisibility(bool enable)
    {
        foreach (GameObject item in uiShownWhenPause)
        {
            item.SetActive(enable);
        }
    }

    // hide or show gui which wants to be visible only when playing
    public void ChangePlayUIVisibility(bool enable)
    {
        foreach (GameObject item in uiShownWhenPlay)
        {
            item.SetActive(enable);
        }
    }
}
