using RayFire;
using UnityEngine;

public class ClickableBox : MonoBehaviour
{
    public AudioClip soundEffect;
    public GameObject visualEffectPrefab;
    
    private RayfireRigid _rayfireRigid;
    private AudioSource _audioSource;

    [SerializeField] private GameObject m_CircleTip;
    private float _circleTimer;
    
    void Start()
    {
        _rayfireRigid = GetComponent<RayfireRigid>();
        _audioSource = GetComponent<AudioSource>();

        if (m_CircleTip != null) m_CircleTip.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (m_CircleTip != null) m_CircleTip.SetActive(false);

        if (!GameManager.Instance.isGameStarted)
        {
            PlaySound();
            PlayVisualEffect();
            GameManager.Instance.BlockClicked();
            _rayfireRigid.Demolish();
        }
    }

    private void Update()
    {
        if (m_CircleTip != null)
        {
            _circleTimer += Time.deltaTime;
            if (_circleTimer > 5f)
            {
                m_CircleTip.SetActive(true);
            }
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
