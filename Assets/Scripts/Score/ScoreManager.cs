using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour 
{
    [SerializeField] private TMP_Text _scoreTextObject;
    public TMP_Text ScoreTextObject { get { return _scoreTextObject; } }

    private int _currentScore;
    public int CurrentScore { get { return _currentScore; } set { if (value >= 0) _currentScore = value; else _currentScore = 0; } }


    private void Start()
    {
        BlockLineChecker.OnLinesChecked += AddScore;
    }
    public void AddScore(int score)
    {
        if(score > 0)
        {
            CurrentScore += score;
            ScoreTextObject.text = CurrentScore.ToString();
        }
    }
}
