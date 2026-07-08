using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartState : State
{
    public override void end()
    {
        state_machine.game.DisableIntro();
    }

    public override void start()
    {
        state_machine.game.Change_Text("");
        BallPhysics.BallPhysicsInstance.Pause();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spring"))
        {
            obj.GetComponent<Spring>().Pause();
        }
    }

    public override void update(float dt)
    {
        // null here
    }
}
