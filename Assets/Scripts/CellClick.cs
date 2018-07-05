using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellClick : MonoBehaviour{

    // Use this for initialization

    public void ChangeImage()
    {
        
        {
            Field field = Field.instanse;
            Game game = Game.instance;
            string name = gameObject.tag;
            int x = int.Parse(name);
            if (field._binarField[x / 3, x % 3] != 1 && field._binarField[x / 3, x % 3] != 2)
            {
                field._binarField[x / 3, x % 3] = 1;
                game.PlayerTurn = false;
                Debug.Log(game.PlayerTurn);
                Debug.Log("i was cliced" + name + "");
            }
            
        }
    }
}
