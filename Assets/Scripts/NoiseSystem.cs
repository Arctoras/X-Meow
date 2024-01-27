using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSystem : MonoBehaviour
{
    [SerializeField] private Slider noiseBar;
    [SerializeField] private int noiseValue;
    [SerializeField] private int noiseMaxValue = 100;

    [SerializeField] private GameManager gameManager;

    float reduceTimer;
    bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            reduceTimer -= Time.deltaTime;
            if (reduceTimer <= 0)
            {
                if (noiseValue > 0)
                {
                    noiseValue -= 1;
                }
                reduceTimer = 0.1f;
            }

            if(noiseValue >= noiseMaxValue)
            {
                isOver = true;
                StartCoroutine(TriggerGameOver());
            }
        }
       
    }

    IEnumerator TriggerGameOver()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Game Over!");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.LogWarning("GameManager Not Binded");
        }
    }

    public void FixedUpdate()
    {
        float barValue = Mathf.Clamp01(noiseValue * 1f / noiseMaxValue);
        noiseBar.value = Mathf.Lerp(noiseBar.value, barValue, 0.1f);
    }

    public void AddNoise(int value)
    {
        noiseValue += value;
    }
}
