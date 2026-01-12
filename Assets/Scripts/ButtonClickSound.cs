using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public static ButtonClickSound instance;
    public AudioSource clickSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        clickSound.Play();
    }
}
