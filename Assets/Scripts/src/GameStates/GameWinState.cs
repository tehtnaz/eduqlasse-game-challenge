using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameWinState : State
{
    public override void end()
    {
        // unused here
    }

    public override void start()
    {
        // call the pause state on player
        BallPhysics.BallPhysicsInstance.Pause();

        const float volume_change = 0.5f;

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(SongNames.Winner, false);
            MusicManager.Instance.SetVolume(volume_change);
        }

        state_machine.game.Change_Text("YOU WIIIIN!");
        state_machine.game.return_button.gameObject.SetActive(true);

        int this_level = state_machine.game.local_level;
        int next_level = this_level + 1;

        PlayerPrefs.SetInt($"level_{next_level}_complete", 1);
        PlayerPrefs.SetInt("levels_complete", this_level);
        PlayerPrefs.Save();
    }

    public override void update(float dt)
    {
        // unused here
    }
}
