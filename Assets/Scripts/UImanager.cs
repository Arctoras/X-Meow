using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score:{score}";
        }
    }

    public void SetGameOverScreenVisibility(bool isVisble)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(isVisble);
        }
    }
}
