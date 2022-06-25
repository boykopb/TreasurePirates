using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameMenu;  
    [SerializeField] private GameObject _startMenu;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreAfterGameText;
    [SerializeField] private string _prevTextScore = "Score: ";
    [SerializeField] private string _prevAfterGameTextScore = "Your score: ";
    
    [SerializeField] private float _timeToActivateLoseMenu = 1.5f;
    [SerializeField] private float _timeToActivateWinMenu = 1.5f;

    [SerializeField] private CoinManager _coinManager;
    void Start()
    {
        EventManager.Current.OnGameLose += OnGameLose;
        EventManager.Current.OnGameWin += OnGameWin;
        EventManager.Current.OnStartedGame += OnStartedGame;
        
        
        _coinManager.OnCoinCollect += CoinManagerOnOnCoinCollect;
    }
    
    private void CoinManagerOnOnCoinCollect(int currentCountCoin)
    {
        SetCurrentScore(currentCountCoin);
    }
    
    private void SetCurrentScore(int currentScore)
    {
        _scoreText.text = _prevTextScore + currentScore;
        _scoreAfterGameText.text = _prevAfterGameTextScore + currentScore;
    }

    private void ShowLoseMenu()
    {
        _loseMenu.SetActive(true);
    }
    
    private void ShowWinMenu()
    {
        _winMenu.SetActive(true);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        EventManager.Current.StartGame();
    }
    
    private void OnGameWin()
    {
        Invoke("ShowWinMenu", _timeToActivateLoseMenu);
    }
    
    private void OnGameLose()
    {
        Invoke("ShowLoseMenu", _timeToActivateWinMenu);
    }
    
    private void OnStartedGame()
    {
        _startMenu.SetActive(false);
        _gameMenu.SetActive(true);
    }
}
