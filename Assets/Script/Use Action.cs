using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseAction : MonoBehaviour
{
    private bool chosen = true;
    private bool locked = false;

    private int active = 4;

    private IEnumerator coroutine;

    private enum State {Movement, Action, Skip, ChangePlayer};
    private enum AvailableAction {Grab, Pass, Throw, Glove, Boot, Shield, Consumable, Tackle, Steal, Return}
    
    private State[] state = new State[] {State.Movement, State.Action, State.ChangePlayer, State.Skip };
    private AvailableAction[] choice =
        new AvailableAction[] {AvailableAction.Grab, AvailableAction.Pass, AvailableAction.Throw, AvailableAction.Glove, AvailableAction.Boot, AvailableAction.Shield, AvailableAction.Consumable, AvailableAction.Return };

    [SerializeField] private Image[] baseWheel;
    [SerializeField] private Image[] actionWheel;

    private int step = 0;

    void Start()
    {
        step = 0;
        for (int i = 0; i < actionWheel.Length; i++)
        {
            actionWheel[i].color = new Color32(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < baseWheel.Length; i++)
        {
            if (step == i)
                baseWheel[i].color = new Color32(255, 255, 255, 255);
            else
                baseWheel[i].color = new Color32(150, 150, 150, 100);
        }

        if (Input.GetKeyDown("right") && chosen && !locked)
        {
            step += 1;
            if (step > state.Length - 1)
            {
                step = 0;
            }
        }
        else if (Input.GetKeyDown("left") && chosen && !locked)
        {
            step -= 1;
            if (step < 0)
            {
                step = state.Length - 1;
            }
        }
        else if ((Input.GetKeyDown("space")) && chosen && !locked)
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
        active = 4;
        chosen = false;

        while (!chosen)
        {

            for (int i = 0; i < actionWheel.Length; i++)
            {
                if (active == i)
                    actionWheel[i].color = new Color32(255, 255, 255, 255);
                else
                    actionWheel[i].color = new Color32(100, 100, 100, 100);
            }

            yield return null;

            if (Input.GetKeyDown("right") && !locked)
            {
                active += 1;
                if (active > choice.Length - 1)
                {
                    active = 0;
                }
            }
            else if (Input.GetKeyDown("left") && !locked)
            {
                active -= 1;
                if (active < 0)
                {
                    active = choice.Length - 1;
                }
            }
            else if ((Input.GetKeyDown("space")) && !locked)
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
                    case AvailableAction.Glove:
                        // Player choose to use boot ability
                        Debug.Log("Glove");
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
                    case AvailableAction.Tackle:
                        // Try to tackle an opponent on the ground
                        Debug.Log("Tackle");
                        break;
                    case AvailableAction.Steal:
                        // Steal the ball to an adjacent player
                        Debug.Log("Steal");
                        break;
                    case AvailableAction.Return:
                        // Player choose to use boot ability
                        Debug.Log("Return");
                        break;
                }
            }
        }
        for (int i = 0; i < actionWheel.Length; i++)
        {
            actionWheel[i].color = new Color32(0, 0, 0, 0);
        }
    }

    void HideAll()
    {
        locked = true;

        for (int i = 0; i < actionWheel.Length; i++)
        {
            actionWheel[i].color = new Color32(255, 255, 255, 0);
        }

        for (int i = 0; i < baseWheel.Length; i++)
        {
            baseWheel[i].color = new Color32(150, 150, 150, 100);
        }
    }

    void ShowAll()
    {
        locked = false;
        
        if (state[step] == State.Action)
        {
            for (int i = 0; i < actionWheel.Length; i++)
            {
                if (active == i)
                    actionWheel[i].color = new Color32(255, 255, 255, 255);
                else
                    actionWheel[i].color = new Color32(100, 100, 100, 100);
            }
        }


        for (int i = 0; i < baseWheel.Length; i++)
        {
            if (step == i)
                baseWheel[i].color = new Color32(255, 255, 255, 255);
            else
                baseWheel[i].color = new Color32(150, 150, 150, 100);
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
