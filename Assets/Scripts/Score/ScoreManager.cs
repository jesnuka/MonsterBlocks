using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour 
{
    [SerializeField] private TMP_Text _scoreTextObject;

    // Multiplier that can be changed by different values, such as game difficulty
    [field:SerializeField] private int _scoreMultiplier;
    public int ScoreMultiplier { get { return _scoreMultiplier; } set { if (value >= 0) _scoreMultiplier = value; else _scoreMultiplier = 1; } }

    [field: SerializeField] private int _lineScore;
    public int LineScore { get { return _lineScore; } set { if (value >= 0) _lineScore = value; else _lineScore = 0; } }

    private int _currentScore;
    public int CurrentScore { get { return _currentScore; } set { _currentScore = value; } }


    private void Start()
    {
        BlockLineChecker.onLinesCleared += AddLineScore;
    }
    public void AddLineScore(int lineCount, int lowestRow)
    {
        if(lineCount > 0)
        {
            for(int i = 0; i <= lineCount; i++)
                CurrentScore += (LineScore * ScoreMultiplier * i);

        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreTextObject.text = CurrentScore.ToString();
    }
}
