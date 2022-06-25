using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  [Header("Controlled components")] [SerializeField]
  private CoinManager _coinManager;

  [Header("Controlled UI elements")] [SerializeField]
  private GameObject _titleScreen;

  [SerializeField] private GameObject _tutorialScreen;
  [SerializeField] private GameObject _loseMenu;
  [SerializeField] private GameObject _winMenu;
  [SerializeField] private GameObject _inGameGUI;
  [SerializeField] private TMP_Text _currentCoinText;


  [Header("Results coins screen")] [SerializeField]
  private GameObject _coinResultsLabel;

  [SerializeField] private TMP_Text _coinResultsText;
  [SerializeField] private Image _backgroundCurtain;
  [SerializeField] private float _coinBonusAddLerpRate = 0.01f;

  private int _currentCoinValue;
  private float _screenAlphaOnTutorial = 0.6f;


  private void Start()
  {
    ShowTitleScreen();
    Subscribe();
    UpdateCoinUIText();
  }

  private void OnDestroy()
  {
    Unsubscribe();
  }


  private void ShowTitleScreen()
  {
    _titleScreen.SetActive(true);
  }

  #region Start logic

  private void OnTutorial()
  {
    _backgroundCurtain.gameObject.SetActive(true);
    SetBackgroundCurtainAlphaTo(_screenAlphaOnTutorial);
    
    _titleScreen.SetActive(false);
    _tutorialScreen.SetActive(true);
    _winMenu.SetActive(false);
    _loseMenu.SetActive(false);
    _coinResultsLabel.SetActive(false);
    _inGameGUI.SetActive(false);
  }

  private void OnStartedGame()
  {
    StartCoroutine(DoActionOnScreenFadeOut(StartGame, _screenAlphaOnTutorial));
  }

  private void StartGame()
  {
    _titleScreen.SetActive(false);
    _tutorialScreen.SetActive(false);
    _winMenu.SetActive(false);
    _loseMenu.SetActive(false);
    _coinResultsLabel.SetActive(false);
    _inGameGUI.SetActive(true);
  }

  #endregion

  #region Win logic

  private void OnWinEvent()
  {
    StartCoroutine(DoActionOnScreenFadeIn(ShowWinMenu, 0.8f));
  }

  private void ShowWinMenu()
  {
    _titleScreen.SetActive(false);
    _tutorialScreen.SetActive(false);
    _winMenu.SetActive(true);
    _loseMenu.SetActive(false);
    _coinResultsLabel.SetActive(true);
    _inGameGUI.SetActive(false);
  }

  #endregion

  #region Loose logic

  private void OnLooseEvent()
  {
    StartCoroutine(DoActionOnScreenFadeIn(ShowLoseMenu, 0.8f));
  }

  private void ShowLoseMenu()
  {
    _titleScreen.SetActive(false);
    _tutorialScreen.SetActive(false);
    _winMenu.SetActive(false);
    _loseMenu.SetActive(true);
    _coinResultsLabel.SetActive(true);
    _inGameGUI.SetActive(false);
  }

  #endregion


  #region EventSubscribtion

  private void Subscribe()
  {
    EventManager.Current.OnTutorial += OnTutorial;
    EventManager.Current.OnStartedGame += OnStartedGame;
    EventManager.Current.OnGameWin += OnWinEvent;
    EventManager.Current.OnGameLose += OnLooseEvent;
    _coinManager.OnCoinCollect += CoinManagerOnCoinCollect;
  }

  private void Unsubscribe()
  {
    EventManager.Current.OnTutorial -= OnTutorial;
    EventManager.Current.OnStartedGame -= OnStartedGame;
    EventManager.Current.OnGameWin -= OnWinEvent;
    EventManager.Current.OnGameLose -= OnLooseEvent;
    _coinManager.OnCoinCollect -= CoinManagerOnCoinCollect;
  }

  #endregion


  #region Coin logic

  private IEnumerator SmoothIncreaseCoinValueRoutine()
  {
    while (_currentCoinValue != _coinManager.CoinsCollectedCount)
    {
      _currentCoinValue++;
      UpdateCoinUIText();
      yield return new WaitForSeconds(_coinBonusAddLerpRate);
    }
  }

  private void UpdateCoinUIText()
  {
    _coinResultsText.text = _currentCoinValue.ToString();
    _currentCoinText.text = _currentCoinValue.ToString();
  }

  private void CoinManagerOnCoinCollect()
  {
    StartCoroutine(SmoothIncreaseCoinValueRoutine());
  }

  #endregion

  #region UI Fade Logic

  private IEnumerator DoActionOnScreenFadeIn(Action action, float toNewValue)
  {
    _backgroundCurtain.gameObject.SetActive(true);
    SetBackgroundCurtainAlphaTo(0);
    while (_backgroundCurtain.color.a <= toNewValue)
    {
      var newColor = _backgroundCurtain.color;
      newColor.a += 0.03f;
      _backgroundCurtain.color = newColor;
      yield return new WaitForSeconds(0.02f);
    }

    action.Invoke();
  }

  private IEnumerator DoActionOnScreenFadeOut(Action action, float fromValue)
  {
    _backgroundCurtain.gameObject.SetActive(true);

    SetBackgroundCurtainAlphaTo(fromValue);

    while (_backgroundCurtain.color.a >= 0.1f)
    {
      var newColor = _backgroundCurtain.color;
      newColor.a -= 0.03f;
      _backgroundCurtain.color = newColor;
      yield return new WaitForSeconds(0.02f);
    }

    _backgroundCurtain.gameObject.SetActive(false);

    action.Invoke();
  }


  private void SetBackgroundCurtainAlphaTo(float newValue)
  {
    var newColor = _backgroundCurtain.color;
    newColor.a = newValue;
    _backgroundCurtain.color = newColor;
  }

  #endregion
}