using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAction : MonoBehaviour
{
    private bool chosen = true;
    private IEnumerator coroutine;

    private enum State {Action, Movement, Skip, ChangePlayer};
    private enum AvailableAction {Grab, Pass, Throw, Boot, Shield, Consumable, Return}
    
    private State[] state = new State[] {State.Action, State.Movement, State.ChangePlayer, State.Skip };
    private AvailableAction[] choice =
        new AvailableAction[] {AvailableAction.Grab, AvailableAction.Pass, AvailableAction.Throw, AvailableAction.Boot, AvailableAction.Shield, AvailableAction.Consumable, AvailableAction.Return };
    
    private int step = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right") && chosen)
        {
            step += 1;
            if (step > state.Length - 1)
            {
                step = 0;
            }
        }
        else if (Input.GetKeyDown("left") && chosen)
        {
            step -= 1;
            if (step < 0)
            {
                step = state.Length - 1;
            }
        }
        else if ((Input.GetKeyDown("space")) && chosen)
        {
            switch (state[step])
            {
                case State.Action:
                    Debug.Log("Action");
                    // Cast "browsing action" function
                    StartCoroutine(Act());
                    break;
                case State.Movement:
                    Debug.Log("Movement");
                    break;
                case State.ChangePlayer:
                    Debug.Log("ChangePlayer");
                    break;
                case State.Skip:
                    Debug.Log("Skip");
                    break;
            }
        }
    }

    IEnumerator Act()
    {
        int active = 2;
        chosen = false;

        while (!chosen)
        {
            yield return null;
            if (Input.GetKeyDown("right"))
            {
                active += 1;
                if (active > choice.Length - 1)
                {
                    active = 0;
                }
            }
            else if (Input.GetKeyDown("left"))
            {
                active -= 1;
                if (active < 0)
                {
                    active = choice.Length - 1;
                }
            }
            else if ((Input.GetKeyDown("space")))
            {
                switch (choice[active])
                {
                    case AvailableAction.Grab:
                        // Player choose to grab
                        Debug.Log("Grab");
                        chosen = true;
                        break;
                    case AvailableAction.Pass:
                        // Player choose to pass
                        Debug.Log("Pass");
                        break;
                    case AvailableAction.Throw:
                        // Player choose to throw
                        Debug.Log("Throw");
                        break;
                    case AvailableAction.Boot:
                        // Player choose to use boot ability
                        Debug.Log("Boot");
                        break;
                    case AvailableAction.Shield:
                        // Player choose to use Shield ability
                        Debug.Log("Shield");
                        break;
                    case AvailableAction.Consumable:
                        // Player choose to use Consumable ability
                        Debug.Log("Consumable");
                        break;
                    case AvailableAction.Return:
                        // Player choose to use boot ability
                        Debug.Log("Return");
                        break;
                }
            }
        }
    }

    /*
    void RefreshActions(StatCharacter player)
    {
        player.actionPoint = 1;
        player.movementPoint = 1;
    }

    void RefreshAll()
    {
        // Get all player and cast Refresh action on all of them
    }

    void TimesUp()
    {
        // Cast refresh all
    }
    */
}
