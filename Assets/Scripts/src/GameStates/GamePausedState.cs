using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePausedState : State
{
    public async override Task end()
    {
        state_machine.game.ChangePauseUIVisibility(false);
    }

    public override void start()
    {
        // call the pause state on player
        BallPhysics.BallPhysicsInstance.Pause();

        // call pause on springs, bad performance, try chaching these
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spring"))
        {
            obj.GetComponent<Spring>().Pause();
        }

        state_machine.game.ChangePauseUIVisibility(true);

        state_machine.game.Change_Text("Right Click to Unpause");
    }

    public override void update(float dt)
    {
        if (Keyboard.current.qKey.isPressed)
        {
            state_machine.Change(GameStates.Restart);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            state_machine.Change(GameStates.Play);
        }
    }
}
