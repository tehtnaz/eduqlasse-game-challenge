using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameRestartState : State
{
    public async override Task end()
    {
        // unused here
    }

    public override void start()
    {
        state_machine.game.Change_Text("Restarting");

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public override void update(float dt)
    {
        // unused here
    }
}
