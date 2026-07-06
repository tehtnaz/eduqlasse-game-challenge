using System.Collections.Generic;
using UnityEngine;

// Controls what state this object is in
public class StateMachine : MonoBehaviour
{
    // All states and their respective instances
    Dictionary<string, State> states = new Dictionary<string, State>();
    // State we currently are in
    public State current_state = null;
    // The current game instance
    public Game game = null;

    // Add a new state to the dictionary
    public void Add(string state_name, State state_object)
    {
        state_object.state_machine = this;

        states.Add(state_name, state_object);
    }

    // Change what the current state we are currently in
    public async void Change(string state_name)
    {
        if (states.ContainsKey(state_name))
        {
            if (current_state == null)
            {
                current_state = states[state_name];
                current_state.start();
            }
            else
            {
                await current_state.end();
                current_state = states[state_name];
                current_state.start();
            }
        }
    }

    // Sends dt forward to update the state
    private void Update()
    {
        if (current_state != null)
        {
            current_state.update(Time.deltaTime);
        }
    }
}
