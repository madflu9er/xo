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

            if (field._binarField[1, 1] != 0)
            {
                XYWeight theXYCoordForTurn = getMaxWeightXYWeight(CheckRow(), CheckColomn());
                PaintO(theXYCoordForTurn.xCoord, theXYCoordForTurn.yCoord);
            }
            else
            {
                PaintO(1, 1);
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

    private XYWeight CheckRow()
    { 
        int sumOfStepsX, sumOfStepsO;
        XYWeight RowXYWeight = new XYWeight();
        for (int i = 0; i < 3; i++)
        { 
            sumOfStepsX = 0; sumOfStepsO = 0;
            for (int j = 0; j < 3; j++)
            {
                if (field._binarField[i, j] == 1)
                {
                    sumOfStepsX++;
                }
                else if (field._binarField[i, j] == 2)
                {
                    sumOfStepsO++;
                }
            }
            if (sumOfStepsX + sumOfStepsO < 3 && sumOfStepsX > RowXYWeight.weightValue)
            {
                RowXYWeight.weightValue = sumOfStepsX;
                RowXYWeight.xCoord = i;
            }
        }
        RowXYWeight.yCoord = GetTheYPosition(RowXYWeight.xCoord);
        return RowXYWeight;
    }

    private XYWeight CheckColomn()
    { 
        int sumOfStepsX, sumOfStepsO;
        XYWeight ColumnXYWeight = new XYWeight(0, 0 ,0);
        for (int i = 0; i < 3; i++)
        { 
            sumOfStepsX = 0;
            sumOfStepsO = 0;
            for (int j = 0; j < 3; j++)
            {
                if (field._binarField[j, i] == 1)
                {
                    sumOfStepsX++;
                }
                else if (field._binarField[j, i] == 2)
                {
                    sumOfStepsO++;
                }
            }
            if (sumOfStepsX > ColumnXYWeight.weightValue && sumOfStepsX + sumOfStepsO < 3)
            {
                ColumnXYWeight.weightValue = sumOfStepsX;
                ColumnXYWeight.yCoord = i;
            }
           
        }
        ColumnXYWeight.xCoord = GetTheXPosition(ColumnXYWeight.yCoord);
        return ColumnXYWeight;
        
    }


    public int GetTheXPosition(int y)
    {
        int xIndex;
            for (int k = 0; k < 3; k++)
        {
            if (field._binarField[k, y] == 0)
            {
                xIndex = k;
                return xIndex;
            }
        }
        return 0;
    }

    public int GetTheYPosition(int x)
    {
        int yIndex;
        
            for (int k = 0; k < 3; k++)
            {
                if (field._binarField[x, k] == 0)
                {
                    yIndex = k;
                    return yIndex;
                }
            }
        return 0;
    }

    private XYWeight getMaxWeightXYWeight(XYWeight row, XYWeight column/*, XYWeight diagonal, XYWeight supportDiagonal*/) {
        XYWeight max;
        max = row;
        if (max.weightValue < column.weightValue) {
            max = column;
        }
        //if (max.weightValue < diagonal.weightValue) {
        //    max = diagonal;
        //}
        //if (max.weightValue < supportDiagonal.weightValue) {
        //    max = supportDiagonal;
        //}
        return max;
    }
}
