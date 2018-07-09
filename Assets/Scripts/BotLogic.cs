using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLogic : MonoBehaviour{

    public static BotLogic instanse = null;
    [SerializeField]
    Field field = Field.instanse;

    [SerializeField]

    Game game = Game.instance;
    
    private bool isMachineMadeTurn;
    private int rowWeight;
    private int collumnWeight;
    private int mainDiagonalWeight;
    private int auxiliaryDiagonalWeight;
    private int index;



    private void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
        }
        else if (instanse != this)
            Destroy(gameObject);
    }

    void Start()
    {
        InvokeRepeating("MachineTurn", 0f, 0.2f);
    }
    public void MachineTurn()
    {
        if (!game.playerTurn)
        {
            XYWeight row = new XYWeight(CheckRow().Key, CheckRow().Value, rowWeight);
            XYWeight column = new XYWeight(CheckColomn().Key, CheckColomn().Value, collumnWeight);
            XYWeight mainDiagonal = new XYWeight(CheckMainDiagonal().Key, CheckMainDiagonal().Value, mainDiagonalWeight);
            XYWeight auxiliaryDiagonal = new XYWeight(CheckAuxiliaryDiagonal().Key, CheckAuxiliaryDiagonal().Value, auxiliaryDiagonalWeight);
            Debug.Log("INDEX = " + index + "");
            if (field._binarField[1, 1] == 1 || field._binarField[1, 1] == 2)
            {
                index = GetTheMaxChoiseWeight(row.weightValue, column.weightValue, mainDiagonal.weightValue, auxiliaryDiagonal.weightValue);
                if (index != 0)
                {
                    switch (index)
                    {
                        case 0:
                            PaintO(row.xCoord, row.yCoord);
                            break;
                        case 1:
                            PaintO(column.xCoord, column.yCoord);
                            break;
                        case 2:
                            PaintO(mainDiagonal.xCoord, mainDiagonal.yCoord);
                            break;
                        case 3:
                            PaintO(auxiliaryDiagonal.xCoord, auxiliaryDiagonal.yCoord);
                            break;
                    }
                }
                else MakeARandomChoise();
            }
            else
            {
                field._binarField[1, 1] = 2;
                game.playerTurn = true;
            }
        }
    }

    public void MakeARandomChoise()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (field._binarField[i, j] == 0)
                {
                    PaintO(i, j);
                    break;
                }
            }
        }
    }

    public void PaintO(int x, int y)
    {
        if (field._binarField[x, y] == 0 && game.playerTurn == false)
        {
            Debug.Log("X:Y = " + x + ":" + y + "");
            field._binarField[x, y] = 2;
            game.playerTurn = true;
        }
    }
    public int GetTheMaxChoiseWeight(int weight1, int weight2, int weight3, int weight4)
    {
        int max = 0;
        int[] weight = new int[4];
        weight[0] = weight1;
        weight[1] = weight2;
        weight[2] = weight3;
        weight[3] = weight4;
        for (int i = 0; i < 4; i++)
        {
            max = weight[0];
            if (weight[i] >= max)
            {
                max = weight[i];
            }
        }
        int index = Array.LastIndexOf(weight, max);
        return index;

    }
    public struct XYWeight
    {
        public int xCoord, yCoord, weightValue;
        public XYWeight(int x, int y, int weight)
        {
            xCoord = x;
            yCoord = y;
            weightValue = weight;
        }
    }

    private KeyValuePair<int, int> CheckRow() 
    {
        bool flagRow = false;
        int xIndex = 0, yIndex = 0;
        int[] sumOfRow = new int[3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (field._binarField[i, j] == 1)
                {
                    sumOfRow[i] += 1;
                    if (sumOfRow[i] == 2)
                    {
                        rowWeight = sumOfRow[i];
                        xIndex = Array.IndexOf(sumOfRow, 2);
                        flagRow = true;
                    }

                }

            }
            sumOfRow[i] = 0;
        }
        yIndex = GetTheYPosition(xIndex, flagRow);
        return new KeyValuePair<int, int>(xIndex, yIndex);     
    }

    private KeyValuePair<int, int> CheckColomn()
    {
        bool flagColumn = false;
        int xIndex = 0, yIndex = 0;
        int[] sumOfColumn = new int[3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (field._binarField[j, i] == 1)
                {
                    sumOfColumn[i] += 1;
                    if (sumOfColumn[i] == 2)
                    {
                        collumnWeight = sumOfColumn[i];
                        yIndex= Array.IndexOf(sumOfColumn, 2);
                        flagColumn = true;
                    }
                }
            }
            sumOfColumn[i] = 0;
        }
        xIndex = GetTheXPosition(yIndex, flagColumn);
        return new KeyValuePair<int, int>(xIndex, yIndex);
    }

    private KeyValuePair<int, int> CheckMainDiagonal()
    { 
        int xIndex = 0, yIndex = 0, sumOfMainDiagonal = 0;
        for (int i = 0; i < 3; i++)
        {
            if (field._binarField[i, i] == 1)
            {
                sumOfMainDiagonal++;
                if (sumOfMainDiagonal == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field._binarField[j, j] == 0)
                        {
                            xIndex = yIndex = j;
                            mainDiagonalWeight = sumOfMainDiagonal;
                            return new KeyValuePair<int, int>(xIndex, yIndex);
                        }
                    }
                }

            }
        }
        return new KeyValuePair<int, int>(0, 0);
    }

    private KeyValuePair<int, int> CheckAuxiliaryDiagonal()
    {
        int xIndex = 0, yIndex = 0, sumOfAuxiliaryDiagonal = 0;
        int k = 2;
        for (int i = 0; i <= k; i++)
        {
            if (field._binarField[i, k - i] == 1)
            {
                sumOfAuxiliaryDiagonal++;
                if (sumOfAuxiliaryDiagonal == 2)
                {
                    for (int j = 0; j <= k; j++)
                    {
                        if (field._binarField[j, k - j] == 0)
                        {
                            xIndex = j;
                            yIndex = k - j;
                            auxiliaryDiagonalWeight = sumOfAuxiliaryDiagonal;
                            return new KeyValuePair<int, int>(xIndex, yIndex);
                        }
                    }
                }
            }

        }
        return new KeyValuePair<int, int>(0, 0);
    }

    public int GetTheXPosition(int y, bool flagColumn)
    {
        int xIndex;
        if (flagColumn)
        {
            for (int k = 0; k < 3; k++)
            {
                if (field._binarField[k, y] == 0)
                {
                    xIndex = k;
                    return xIndex;
                }
            }
        }
        return 0;
    }
    public int GetTheYPosition(int x, bool flagRow)
    {
        int yIndex;
        if (flagRow)
        {
            for (int k = 0; k < 3; k++)
            {
                if (field._binarField[x, k] == 0)
                {
                    yIndex = k;
                    return yIndex;
                }
            }
        }
        return 0;
    }


}
