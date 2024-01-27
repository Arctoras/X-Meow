using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private Image _faceImage;
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
}
