using System.Reflection.Metadata;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayState : State
{

    public async override Task end()
    {
        // unused here
    }

    public override void start()
    {
        // Call unpause on player
        BallPhysics.BallPhysicsInstance.Unpause();

        // call unpause on springs, bad performance, try chaching these
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spring"))
        {
            obj.GetComponent<Spring>().Unpause();
        } 

        state_machine.game.Change_Text("");
    }

    public override void update(float dt)
    {
        if (Keyboard.current.qKey.isPressed)
        {
            state_machine.Change(GameStates.Restart);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            state_machine.Change(GameStates.Paused);
        }
    }
}
