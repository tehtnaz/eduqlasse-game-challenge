using System.Reflection.Metadata;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayState : State
{

    public async override Task end()
    {
        state_machine.game.ChangePlayUIVisibility(false);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
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

        state_machine.game.ChangePlayUIVisibility(true);

        state_machine.game.Change_Text("");
    }

    public override void update(float dt)
    {
        // slow motion effect
        if (Keyboard.current.sKey.isPressed){
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
        }


        if (Keyboard.current.qKey.isPressed)
        {
            state_machine.Change(GameStates.Restart);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            state_machine.Change(GameStates.Paused);
        }
    }
}
