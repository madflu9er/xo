using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

    public static Field instanse = null;

	[SerializeField]
    private Image[] _fieldCells;

    [SerializeField]
    private Sprite _X;

    [SerializeField]
    private Sprite _O;

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
        if (instanse == null)
        {
            instanse = this;
        }
        else if (instanse != this)
            Destroy(gameObject);
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
    }
}
