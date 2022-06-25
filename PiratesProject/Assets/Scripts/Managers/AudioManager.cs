using System.Collections;
using UnityEngine;

namespace Managers
{
  public class AudioManager : MonoBehaviour
  {
    [Space, Header("Sources")] [SerializeField]
    private AudioSource _levelMusicSource;

    [SerializeField] private AudioSource _ambientMusicSource;
    [SerializeField] private AudioSource _soundEffectsSource;

    [Space, Header("Music clips")] [SerializeField]
    private AudioClip _levelMusic;

    [SerializeField] private AudioClip _ambientMusic;

    [Space, Header("Volume")] [SerializeField, Range(0, 1)]
    private float _levelMusicVolume = 0.6f;

    [SerializeField, Range(0, 1)] private float _ambientMusicVolume = 0.3f;
    [SerializeField, Range(0, 1)] private float _soundFxVolume = 0.8f;


    public static AudioManager Instance;


    private void Awake() =>
      Singleton();

    private void Start()
    {
      SetVolume();
      SetMusicSourceSettings();
      PlayLevelMusic();
    }


    public void PlaySFX(AudioClip clip, float delayTime = 0f, bool randomPitch = true)
    {
      StartCoroutine(PlaySFXOnDelay(clip, delayTime, randomPitch));
    }

    public void StopLevelMusic()
    {
      _levelMusicSource.Stop();
    }

    private IEnumerator PlaySFXOnDelay(AudioClip clip, float delayTime, bool randomPitch)
    {
      yield return new WaitForSeconds(delayTime);
      
      if (randomPitch)
        _soundEffectsSource.pitch = Random.Range(0.8f, 1.2f);
      
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