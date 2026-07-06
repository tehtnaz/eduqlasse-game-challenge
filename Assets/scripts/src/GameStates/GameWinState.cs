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
    }

    public override void update(float dt)
    {
        // unused here
    }
}
