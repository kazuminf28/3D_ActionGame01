using UnityEngine;
using UnityEngine.UI;

public class ButtonSE : MonoBehaviour
{
    public static ButtonSE Instance;

    [Header("UIクリック音")]
    public AudioClip clickSE;

    public AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    public void PlayClickSE()
    {
        if (clickSE != null)
        {
            audioSource.PlayOneShot(clickSE);
        }
    }
}
