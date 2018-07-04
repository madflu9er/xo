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
    public int sumOfDiagonal;
    public int sumOfSideDiagonal;
    public static bool playerTurn = true;
    public int xIndexOfCollumn;
    public int yIndexOfCollumn;
    public int xIndexOfRow;
    public int yIndexOfRow;
    public int xindexOfDiagonal;
    public int yIndexOfDiagonal;
    public int xindexOfSideDiagonal;
    public int yindexOfSideDiagonal;
    public bool flagRow = false;
    public bool flagColumn = false;
    public bool flagDiagonal = false;
    public bool isMachineMadeTurn = false;

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

    public void MachineTurn()
    {
        if (cellOfCode[1, 1] == 0)
        {
            cellOfCode[1, 1] = 2;
            playerTurn = true;
        }
        else
        {
            isMachineMadeTurn = false;
            var rowPair = CheckRow(playerTurn);
            var colmnPair = CheckColomn(playerTurn);
            var diagMainPair = CheckMainDiagonal(playerTurn);
            var diagSidePair = CheckSideDiagonal(playerTurn);
            Debug.Log("MachinTURN = " + isMachineMadeTurn + "");
            BlockOnEachLine(rowPair.Key, rowPair.Value);
            BlockOnEachLine(colmnPair.Key, colmnPair.Value);
            BlockOnEachLine(diagMainPair.Key, diagMainPair.Value);
            BlockOnEachLine(diagSidePair.Key, diagSidePair.Value);
            playerTurn = true;
        } 
                  
    }

    

    public void BlockOnEachLine(int x, int y)
    {
        if (cellOfCode[x, y] == 0 && !isMachineMadeTurn)
        {
            cellOfCode[x, y] = 2;
            Debug.Log("I made my turn");
            isMachineMadeTurn = true;
        }
    }
    /**
     * @return 1 element - X, 2 element - Y of position when you must put 
     */
    private KeyValuePair<int, int> CheckRow(bool checkPlayer) //true = player, false = machine
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (cellOfCode[i, j] == 1)
                {
                    sumOfRow[i] += 1;
                    if (sumOfRow[i] == 2)
                    {
                        xIndexOfRow = Array.IndexOf(sumOfRow, 2);
                        flagRow = true;
                        Debug.Log("i found this on "+ xIndexOfRow + " row");
                    }
                    
                }
                
            }
            // Debug.Log("В строке " + (i+1) + "количество Х = " + sumOfRow[i] + "");
            sumOfRow[i] = 0;
        }
        if (flagRow)
        {
            for (int k = 0; k < 3; k++)
            {
                if (cellOfCode[xIndexOfRow, k] == 0)
                {
                    yIndexOfRow = k;
                    Debug.Log("X:Y = " + xIndexOfRow + ":" + yIndexOfRow + "");
                }
                else  Debug.Log("NOT FOUND"); 
            }
        }
        
        //Debug.Log("X:Y = " + xIndexOfCollumn + ":" + yIndexOfCollumn + "");
        return new KeyValuePair<int, int>(xIndexOfRow, yIndexOfRow); 
    }
    private KeyValuePair<int, int> CheckColomn(bool checkPlayer) 
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (cellOfCode[j, i] == 1)
                {
                    sumOfColumn[i] += 1;
                    if (sumOfColumn[i] == 2)
                    {
                        yIndexOfCollumn = Array.IndexOf(sumOfColumn, 2);
                        flagColumn = true;
                    }
                }
            }
          //  Debug.Log("В столбце " + (i + 1) + " Количество Х = " + sumOfColumn[i] + "");
            sumOfColumn[i] = 0;
        }

        if (flagColumn)
        {
            for (int k = 0; k < 3; k++)
            {
                if (cellOfCode[k, yIndexOfCollumn] == 0)
                {
                    xIndexOfCollumn = k;
                    Debug.Log("X:Y = " + xIndexOfCollumn + ":" + yIndexOfCollumn + "");
                }
                else Debug.Log("Not Found");
            }
        }

        return new KeyValuePair<int, int>(xIndexOfCollumn, yIndexOfCollumn);
    }
    private KeyValuePair<int, int> CheckMainDiagonal(bool checkPlayer) //true = player, false = machine
    {
        sumOfDiagonal = 0;
        for (int i = 0; i < 3; i++)
        {
            if (cellOfCode[i, i] == 1)
            {
                sumOfDiagonal ++;
                if (sumOfDiagonal == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (cellOfCode[j, j] == 0)
                        {
                            xindexOfDiagonal = yIndexOfDiagonal = j;
                            Debug.Log("X:Y = " + xindexOfDiagonal + ":" + yIndexOfDiagonal + "");
                        }
                        else Debug.Log("EMPTY");
                    }
                }
                
            }
        }
        return new KeyValuePair<int, int>(xindexOfDiagonal, yIndexOfDiagonal);
    }
    private KeyValuePair<int, int> CheckSideDiagonal(bool checkPlayer)
    {
        sumOfSideDiagonal = 0;
        int k = 2;
        for (int i = 0; i <= k; i++)
        {
            if (cellOfCode[i, k - i] == 1)
            {
                sumOfSideDiagonal++;
                if (sumOfSideDiagonal == 2)
                {
                    for (int j = 0; j <= k; j++)
                    {
                        if (cellOfCode[j, k - j] == 0)
                        {
                            xindexOfSideDiagonal = j;
                            yindexOfSideDiagonal = k - j;
                            return new KeyValuePair<int, int>(xindexOfSideDiagonal, yindexOfSideDiagonal);
                        }
                    }
                }
            }

        }
        return new KeyValuePair<int, int>(xindexOfSideDiagonal, yindexOfSideDiagonal);
    }



}
