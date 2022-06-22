using UnityEngine;

namespace Managers
{
  public class SoundManager : MonoBehaviour
  {
    [Space, Header("Sources")] 
    [SerializeField] private AudioSource _levelMusicSource;
    [SerializeField] private AudioSource _ambientMusicSource;
    [SerializeField] private AudioSource _soundEffectsSource;

    [Space, Header("Music clips")] 
    [SerializeField] private AudioClip _levelMusic;
    [SerializeField] private AudioClip _ambientMusic;

    [Space, Header("Volume")] 
    [SerializeField, Range(0, 1)] private float _levelMusicVolume = 0.6f;
    [SerializeField, Range(0, 1)] private float _ambientMusicVolume = 0.3f;
    [SerializeField, Range(0, 1)] private float _soundFxVolume = 0.8f;

  
    public static SoundManager Instance;
    

    private void Awake() =>
      Singleton();

    private void Start()
    {
      SetVolume();
      SetMusicSourceSettings();
      PlayLevelMusic();
    }
    
    
    public void PlaySFX(AudioClip clip)
    {
      _soundEffectsSource.pitch = Random.Range(0.9f, 1.1f);
      _soundEffectsSource.PlayOneShot(clip);
    }

    
    private void PlayLevelMusic()
    {
      _levelMusicSource.Play();
      _ambientMusicSource.Play();
    }
    private void Singleton()
    {
      if (Instance == null)
        Instance = this;
      else
        Destroy(gameObject);
    }
    
    private void SetMusicSourceSettings()
    {
      _levelMusicSource.loop = true;
      _levelMusicSource.clip = _levelMusic;
      _ambientMusicSource.loop = true;
      _ambientMusicSource.clip = _ambientMusic;
    }



    private void SetVolume()
    {
      _levelMusicSource.volume = _levelMusicVolume;
      _ambientMusicSource.volume = _ambientMusicVolume;
      _soundEffectsSource.volume = _soundFxVolume;
    }
    
  }
}