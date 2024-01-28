using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMeow : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            m_AudioSource.pitch = Random.Range(0.5f, 1.5f);
            m_AudioSource.Play();

            timer = Random.Range(3, 10);
        }
    }
}
