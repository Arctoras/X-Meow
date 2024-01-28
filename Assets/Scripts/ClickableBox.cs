using RayFire;
using UnityEngine;

public class ClickableBox : MonoBehaviour
{
    public AudioClip soundEffect;
    public GameObject visualEffectPrefab;
    
    private RayfireRigid _rayfireRigid;
    private AudioSource _audioSource;
    
    void Start()
    {
        _rayfireRigid = GetComponent<RayfireRigid>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            PlaySound();
            PlayVisualEffect();
            GameManager.Instance.BlockClicked();
            _rayfireRigid.Demolish();
        }
    }

    void PlaySound()
    {
        if (soundEffect != null)
        {
            _audioSource.PlayOneShot(soundEffect);
        }
    }

    void PlayVisualEffect()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // 设置一个适当的z值
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        // 在鼠标位置实例化特效
        Instantiate(visualEffectPrefab, worldPosition, Quaternion.identity);
    }
}
