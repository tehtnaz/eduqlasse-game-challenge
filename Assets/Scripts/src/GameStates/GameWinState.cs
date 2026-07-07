using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameWinState : State
{
    public async override Task end()
    {
        // unused here
    }

    public override void start()
    {
        state_machine.game.Change_Text("YOU WIIIIN!");
        state_machine.game.return_button.gameObject.SetActive(true);

        int next_level = state_machine.game.local_level + 1;

        PlayerPrefs.SetInt($"level_{next_level}_complete", 1);
        PlayerPrefs.Save();
    }

    public override void update(float dt)
    {
        // unused here
    }
}
