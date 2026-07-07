using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartState : State
{
    public async override Task end()
    {
        state_machine.game.DisableIntro();
    }

    public override void start()
    {
        BallPhysics.BallPhysicsInstance.Pause();
    }

    public override void update(float dt)
    {
        // unused here
    }
}
