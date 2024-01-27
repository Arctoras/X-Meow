using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSystem : MonoBehaviour
{
    [SerializeField] private Slider noiseBar;
    [SerializeField] private Image barFillImg;
    [SerializeField] private Sprite GreenSprite;
    [SerializeField] private Sprite YellowSprite;
    [SerializeField] private Sprite RedSprite;

    [SerializeField] private float noiseValue;
    [SerializeField] private float noiseMaxValue = 100;
    [SerializeField] private float noiseReduceSpeed = 1;

    private GameManager gameManager;



    float reduceTimer;
    bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
                    noiseValue -= noiseReduceSpeed;
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
            Debug.LogError("GameManager Not Find");
        }
    }

    public void FixedUpdate()
    {
        float barValue = Mathf.Clamp01(noiseValue * 1f / noiseMaxValue);
        noiseBar.value = Mathf.Lerp(noiseBar.value, barValue, 0.1f);
        if(barValue < 0.4f)
        {
            barFillImg.sprite = GreenSprite;
        }
        else if(barValue < 0.7f)
        {
            barFillImg.sprite = YellowSprite;
        }
        else
        {
            barFillImg.sprite = RedSprite;
        }
    }

    public void AddNoise(float value)
    {
        noiseValue += value;
    }
}
