using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

	[SerializeField]
    private Image[] _fieldCells;

    [SerializeField]
    private Sprite _X;

    [SerializeField]
    private Sprite _O;

    [SerializeField]
    private Text winnerText;

    public GameObject win;
    public static Field instanse = null;
    public int[,] _binarField = new int[3, 3];

    public Sprite X
    {
        get
        {
            return _X;
        }

        set
        {
            _X = value;
        }
    }

    public Sprite O
    {
        get
        {
            return _O;
        }

        set
        {
            _O = value;
        }
    }


    void Awake()
    {
        win.SetActive(false);
        if (instanse == null)
        {
            instanse = this;
        }
        else if (instanse != this)
        {
            Destroy(gameObject);
        } 
    }

    public void CheckTheWinCombinationOnRow()
    {
        int sumOfStepsX, sumOfStepsO;
        for (int i = 0; i < 3; i++)
        {
            sumOfStepsO = 0; sumOfStepsX = 0;
            for (int j = 0; j < 3; j++)
            {
                if (_binarField[i, j] == 1)
                {
                    sumOfStepsX++;
                }
                if (_binarField[i, j] == 2)
                {
                    sumOfStepsO++;
                }
            }
            if (sumOfStepsX == 3)
            {
                DrawPlayerWin();
            }
            else if (sumOfStepsO == 3)
            {
                DrawComputerWin();
            }
        }
    }

    public void CheckTheWinCombinationOnCollumn()
    { 
        int sumOfStepsX, sumOfStepsO;
        for (int i = 0; i < 3; i++)
        {
            sumOfStepsO = 0; sumOfStepsX = 0;
            for (int j = 0; j < 3; j++)
            {
                if (_binarField[j, i] == 1)
                {
                    sumOfStepsX++;
                }
                if (_binarField[j, i] == 2)
                {
                    sumOfStepsO++;
                }
            }
            if (sumOfStepsX == 3)
            {
                DrawPlayerWin();
            }
            else if (sumOfStepsO == 3)
            {
                DrawComputerWin();
            }
        }
    }

    public void CheckTheWinCombinationOnMainDiagonal()
    {
        int sumOfStepsO = 0, sumOfStepsX = 0;
        for (int i = 0; i < 3; i++)
        {
            if (_binarField[i, i] == 1)
            {
                sumOfStepsX++;
            }
            else if (_binarField[i, i] == 2)
            {
                sumOfStepsO++;
            }
        }
        if (sumOfStepsX == 3)
        {
            DrawPlayerWin();
        }
        else if (sumOfStepsO == 3)
        {
            DrawComputerWin();
        }
    }

    public void CheckTheWinCombinationOnSupportDiagonalDiagonal()
    {
        int sumOfStepsO = 0, sumOfStepsX = 0, k = 2;
        for (int i = 0; i <= k; i++)
        {
            if (_binarField[i, k-i] == 1)
            {
                sumOfStepsX++;
            }
            else if (_binarField[i, k-i] == 2)
            {
                sumOfStepsO++;
            }
        }
        if (sumOfStepsX == 3)
        {
            DrawPlayerWin();
        }
        else if (sumOfStepsO == 3)
        {
            DrawComputerWin();
        }
    }

    private void DrawPlayerWin()
    {
        win.SetActive(true);
        winnerText.text = "YOU WIN";
        Time.timeScale = 0;
    }

    private void DrawComputerWin()
    {
        win.SetActive(true);
        winnerText.text = "COMPUTER WIN";
        Time.timeScale = 0;
    }

    public void ChechTheField() 
    {
        for (int i = 0; i <3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int indexOfCell = i * 3 + j; // перевод с индекации двумерного масива в одномерный
                if (_binarField[i, j] == 0)
                {
                    _fieldCells[indexOfCell].color = new Color(255f, 255f, 255f); 
                }
                else if (_binarField[i,j] == 1)
                {
                    _fieldCells[indexOfCell].sprite = X; 
                }
                else if (_binarField[i,j] == 2)
                {
                    _fieldCells[indexOfCell].sprite = O; 
                }
            }
        }
        CheckTheWinCombinationOnRow();
        CheckTheWinCombinationOnCollumn();
        CheckTheWinCombinationOnMainDiagonal();
        CheckTheWinCombinationOnSupportDiagonalDiagonal();
    }
}
