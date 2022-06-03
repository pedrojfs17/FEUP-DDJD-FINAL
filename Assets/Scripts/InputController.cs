using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public int playerNumber { get; private set; }

    private GameControlls gameControlls;
    
    private GameLogic logic;

    // Start is called before the first frame update
    void Start()
    {
        Match result = Regex.Match(name, @"\d+$", RegexOptions.RightToLeft);
		
        if (result.Success) { 
			playerNumber = Int32.Parse(result.Value);
		} else {
            throw new UnityException("Player object must be at the format: Player<Player Number>");
        }

        GameObject gameLogic = GameObject.Find("GameLogic");
        logic = gameLogic.GetComponent<GameLogic>();

        gameControlls = new GameControlls();

        switch (playerNumber) {
            case 1:
                gameControlls.Player1.Enable();
                gameControlls.Player1.Action.performed += onAction;
                gameControlls.Player1.Blue.performed += onBlue;
                gameControlls.Player1.Orange.performed += onOrange;
                gameControlls.Player1.Green.performed += onGreen;
                gameControlls.Player1.Yellow.performed += onYellow;
                break;
            case 2:
                gameControlls.Player2.Enable();
                gameControlls.Player2.Action.performed += onAction;
                gameControlls.Player2.Blue.performed += onBlue;
                gameControlls.Player2.Orange.performed += onOrange;
                gameControlls.Player2.Green.performed += onGreen;
                gameControlls.Player2.Yellow.performed += onYellow;
                break;
            case 3:
                gameControlls.Player3.Enable();
                gameControlls.Player3.Action.performed += onAction;
                gameControlls.Player3.Blue.performed += onBlue;
                gameControlls.Player3.Orange.performed += onOrange;
                gameControlls.Player3.Green.performed += onGreen;
                gameControlls.Player3.Yellow.performed += onYellow;
                break;
            case 4:
                gameControlls.Player4.Enable();
                gameControlls.Player4.Action.performed += onAction;
                gameControlls.Player4.Blue.performed += onBlue;
                gameControlls.Player4.Orange.performed += onOrange;
                gameControlls.Player4.Green.performed += onGreen;
                gameControlls.Player4.Yellow.performed += onYellow;
                break;
            default:
                break;
        }
    }

    public void onAction(InputAction.CallbackContext context) 
    {
        logic.playerAction(this.gameObject);
    }

    public void onBlue(InputAction.CallbackContext context) 
    {
        logic.playerBlue(this.gameObject);
    }

    public void onOrange(InputAction.CallbackContext context) 
    {
        logic.playerOrange(this.gameObject);
    }

    public void onGreen(InputAction.CallbackContext context) 
    {
        logic.playerGreen(this.gameObject);
    }

    public void onYellow(InputAction.CallbackContext context) 
    {
        logic.playerYellow(this.gameObject);
    }

    void OnDestroy() {
        switch (playerNumber) {
            case 1:
                gameControlls.Player1.Disable();
                gameControlls.Player1.Action.performed -= onAction;
                gameControlls.Player1.Blue.performed -= onBlue;
                gameControlls.Player1.Orange.performed -= onOrange;
                gameControlls.Player1.Green.performed -= onGreen;
                gameControlls.Player1.Yellow.performed -= onYellow;
                break;
            case 2:
                gameControlls.Player2.Disable();
                gameControlls.Player2.Action.performed -= onAction;
                gameControlls.Player2.Blue.performed -= onBlue;
                gameControlls.Player2.Orange.performed -= onOrange;
                gameControlls.Player2.Green.performed -= onGreen;
                gameControlls.Player2.Yellow.performed -= onYellow;
                break;
            case 3:
                gameControlls.Player3.Disable();
                gameControlls.Player3.Action.performed -= onAction;
                gameControlls.Player3.Blue.performed -= onBlue;
                gameControlls.Player3.Orange.performed -= onOrange;
                gameControlls.Player3.Green.performed -= onGreen;
                gameControlls.Player3.Yellow.performed -= onYellow;
                break;
            case 4:
                gameControlls.Player4.Disable();
                gameControlls.Player4.Action.performed -= onAction;
                gameControlls.Player4.Blue.performed -= onBlue;
                gameControlls.Player4.Orange.performed -= onOrange;
                gameControlls.Player4.Green.performed -= onGreen;
                gameControlls.Player4.Yellow.performed -= onYellow;
                break;
            default:
                break;
        }
    }
}
