using UnityEngine;

public class BgMusic : MonoBehaviour
{
    private static BgMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            AudioSource audio = GetComponent<AudioSource>();
            audio.loop = true;
            audio.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
