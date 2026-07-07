using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class GamePausedState : State
{
    public async override Task end()
    {
        // unused here
    }

    public override void start()
    {
        // call the pause state on player
        BallPhysics.BallPhysicsInstance.Pause();

        state_machine.game.Change_Text("Left Click to Unpause");
    }

    public override void update(float dt)
    {
        if (Keyboard.current.qKey.isPressed)
        {
            state_machine.Change(GameStates.Restart);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            state_machine.Change(GameStates.Play);
        }
    }
}
