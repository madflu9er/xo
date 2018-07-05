using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private BotLogic _botLogic;
    private Field _field;
    private bool playerTurn;
    public static Game instance = null;
    public bool PlayerTurn
    {
        get
        {
            return playerTurn;
        }

        set
        {
            playerTurn = value;
        }
    }

     void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        _botLogic = new BotLogic();
        PlayerTurn = true;
        
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("RenderField", 0f, 0.5f); 
	}

    private void RenderField()
    {
        _field.ChechTheField();
        if (!PlayerTurn)
        {
            // _botLogic.MachineTurn();
            Debug.Log("MashineTURN!!!!");
        }
    }

}
