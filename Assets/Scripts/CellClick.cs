using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellClick : MonoBehaviour {

    // Use this for initialization


    public void ChangeImage()
    {
        {
            name = gameObject.tag;
            int x = int.Parse(name);
            FieldController.cellOfCode[x/3, x%3] = 1;
            FieldController.playerTurn = false;
            Debug.Log("i was cliced" + name + "");
        }
    }
}
