using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [field: SerializeField] private int _maxColumnAmount;
    public int MaxColumnAmount { get { return _maxColumnAmount; } set { _maxColumnAmount = value; } }

    [field: SerializeField] private int _currentColumnAmount;
    public int CurrentColumnAmount { get { return _currentColumnAmount; } set { if (value <= MaxColumnAmount) _currentColumnAmount = value; } }

    [field: SerializeField] private int _maxRowAmount;
    public int MaxRowAmount { get { return _maxRowAmount; } set { _maxRowAmount = value; } }

    [field: SerializeField] private int _currentRowAmount;
    public int CurrentRowAmount { get { return _currentRowAmount; } set { if(value <= MaxRowAmount) _currentRowAmount = value; } }

}
