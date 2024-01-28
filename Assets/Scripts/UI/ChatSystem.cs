using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private Image _faceImage;

    [SerializeField] private Sprite cat_angry;
    [SerializeField] private Sprite cat_happy;
    [SerializeField] private Sprite cat_boring;
    [SerializeField] private Sprite cat_devil;


    [SerializeField] private GameObject _chatBox;
    private float _chatTimer = 0f;
    [SerializeField] private float _chatDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _chatBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _chatTimer -= Time.deltaTime;
        if (_chatTimer <= 0)
        {
            _chatBox.SetActive(false);
        }
    }

    public void ShowFaceImage(Sprite image)
    {
        _faceImage.sprite = image;
        _chatBox.SetActive(true);
        _chatTimer = _chatDuration;
    }

    public void DoBadThings()
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                ShowFaceImage(cat_happy);
                break;
            case 1:
                ShowFaceImage(cat_devil);
                break;
        }
    }
}
