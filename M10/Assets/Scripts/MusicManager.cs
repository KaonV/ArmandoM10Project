using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource cookSound;
    public AudioSource musicBase;
    public AudioSource newIngredientSound;
    public AudioSource failAudioSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicBase.Play();
    }

    private void FailSound()
    {
        failAudioSound.Play();
    }


    private void CookSound()
    {
        cookSound.Play();
    }

    private void CookNewIngredient()
    {
        newIngredientSound.Play();
    }
}
