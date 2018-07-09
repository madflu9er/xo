using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField]
    private BotLogic _botLogic;
    [SerializeField]
    private Field _field;
    public static Game instance = null;
    public bool playerTurn = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        playerTurn = true;
        
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("RenderField", 0f, 0.5f); 
	}

    private void RenderField()
    {
        _field.ChechTheField();
        if (!playerTurn)
        {
            _botLogic.MachineTurn();
            Debug.Log("MashineTURN!!!!");
        }
    }

}
