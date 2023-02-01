using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings: MonoBehaviour
{
    [field: SerializeField] private int _maxColumnAmount;
    public int MaxColumnAmount { get { return _maxColumnAmount; } set { 
            if (value > 4) _maxColumnAmount = value; 
            else _maxColumnAmount = 4; } }

    [field: SerializeField] private int _currentColumnAmount;
    public int CurrentColumnAmount { get { return _currentColumnAmount; } set { 
            if (value <= MaxColumnAmount && value > 4) _currentColumnAmount = value; 
            else _currentColumnAmount = 4; } }

    [field: SerializeField] private int _maxRowAmount;
    public int MaxRowAmount { get { return _maxRowAmount; } set { 
            if (value > 4) _maxRowAmount = value; 
            else _maxRowAmount = 4; } }

    [field: SerializeField] private int _currentRowAmount;
    public int CurrentRowAmount { get { return _currentRowAmount; } set { 
            if(value <= MaxRowAmount && value > 4) _currentRowAmount = value; 
            else _currentRowAmount = 4; } }

    public static event Action<GameSettings> OnGameSettingsChanged;
    public void EnableGameSettings()
    {
        OnGameSettingsChanged?.Invoke(this);
    }

}
