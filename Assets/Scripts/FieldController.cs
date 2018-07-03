using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldController : MonoBehaviour {
    [SerializeField]
    private Image[] cell;

    [SerializeField]
    private Sprite _x;

    [SerializeField]
    private Sprite _o;

    public static int[,] cellOfCode = new int[3,3];
    public int[] sumOfRow = new int[3];
    public int[] sumOfColumn = new int[3];
    public int[] sumOfDiagonal = new int[3];
    public static bool playerTurn = true;
    public int maxColumn;
    public int maxRow;


    public Sprite O
    {
        get
        {
            return _o;
        }

        set
        {
            _o = value;
        }
    }

    public Sprite X
    {
        get
        {
            return _x;
        }

        set
        {
            _x = value;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cellOfCode[i,j] = 0;
            }
        }
        playerTurn = true;
    }

    void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update () {

        ChechTheField();

        if (!playerTurn)
        {
            MachineTurn();
        }
    }

    public void ChechTheField() 
    {
        for (int i = 0; i <3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int indexOfCell = i * 3 + j; // перевод с индекации двумерного масива в одномерный
                if (cellOfCode[i, j] == 0)
                {
                    cell[indexOfCell].sprite = null; 
                }
                else if (cellOfCode[i,j] == 1)
                {
                    cell[indexOfCell].sprite = X; 
                }
                else if (cellOfCode[i,j] == 2)
                {
                    cell[indexOfCell].sprite = O; 
                }
            }
        }
    }

    public int MachineTurn()
    {
        CheckRow(playerTurn);
        CheckColomn(playerTurn);
        CheckDiagonal(playerTurn);

        playerTurn = true;
        return 0;
        
        
    }

    private void CheckRow(bool checkPlayer) //true = player, false = machine
    {
        maxRow = sumOfRow[0];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sumOfRow[i] += cellOfCode[i, j];
            }
            if (sumOfRow[i] > maxRow)
            {
                maxRow = sumOfRow[i];
            }
            
            Debug.Log("Сума эллементов " + (i+1) + " строки = " + sumOfRow[i] + "");
            
        }

        int index = Array.IndexOf(sumOfRow, maxRow);
        Debug.Log(index);
        for (int i = 0; i < 3; i++)
        {
            sumOfRow[i] = 0;
        }



    }
    private void CheckColomn(bool checkPlayer) 
    {
        maxColumn = sumOfColumn[0];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sumOfColumn[i] += cellOfCode[j, i];
            }
            
            
            Debug.Log("Сума эллементов " + (i + 1) + " столбца = " + sumOfColumn[i] + "");
            sumOfColumn[i] = 0;
        }
        Debug.Log("COLUMNMAX=" + maxColumn + "");
    }
    private void CheckDiagonal(bool checkPlayer) //true = player, false = machine
    {
        int maxDiagonal = sumOfDiagonal[0];
        for (int i = 0; i < 3; i++)
        {
            sumOfDiagonal[i] = cellOfCode[i, i];
            if (sumOfDiagonal[i] > maxDiagonal)
            {
                maxDiagonal = sumOfDiagonal[i];
            }
        }
        int index = Array.IndexOf(sumOfDiagonal, maxDiagonal);
        Debug.Log("DIAGONAL = "+index+"");

    }



}
