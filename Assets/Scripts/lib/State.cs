using System.Threading.Tasks;
using UnityEngine;

// Used to do things in a specific state
public abstract class State
{
    // This state's owner
    public StateMachine state_machine;

    // What happens when entering this state
    public abstract void start();

    // What happens when leaving this state
    public abstract void end();

    // What happens every tick in this state
    public abstract void update(float dt);
}
