using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _resume;
    [SerializeField] private Button _options;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _back;
    [SerializeField] private Button _retry;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private List<GameObject> _hearts;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _streak;

    private InputSystem_Actions _input;
    private TimerManager _timerManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Inputs.Enable();
        _input.Inputs.Exit.performed += Exit_performed;

        _timerManager = GetComponent<TimerManager>();
        _gameManager = GetComponent<GameManager>();
        _gameManager.OnHeartDestroyed += OnHeartDestroyed;
        _gameManager.OnGameOver += OnGameOver;

        _resume.onClick.AddListener(OnResume);
        _options.onClick.AddListener(OnOptions);
        _exit.onClick.AddListener(OnExit);
        _back.onClick.AddListener(OnBack);
        _retry.onClick.AddListener(OnRetry);
    }

    private void Start()
    {
        if(_gameManager.Difficulty == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                _hearts[i].SetActive(true);
            }
        }
    }

    private void Exit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_optionsMenu.activeSelf == true)
        {
            _optionsMenu.SetActive(false);
        }
        else if (_pauseMenu.activeSelf == true)
        {
            _pauseMenu.SetActive(false);
            _timerManager.SetPlayState(true);
        }
        else
        {
            _pauseMenu.SetActive(true);
            _timerManager.SetPlayState(false);
        }
    }

    private void OnHeartDestroyed(object sender, EventArgs e)
    {
        _hearts.Last().SetActive(false);
        _hearts.Remove(_hearts.Last());
    }

    private void OnGameOver(object sender, GameManager.OnGameEndedArgs e)
    {
        _input.Inputs.Disable();
        _gameOverMenu.SetActive(true);
        _score.text = e.Score.ToString();
        _streak.text = e.Streak.ToString();
    }

    private void OnResume()
    {
        _pauseMenu.SetActive(false);
        _timerManager.SetPlayState(true);
    }

    private void OnOptions()
    {
        _optionsMenu.SetActive(true);
    }

    private void OnExit()
    {
        SceneManager.LoadScene(0);
    }

    private void OnBack()
    {
        SceneManager.LoadScene(0);
    }

    private void OnRetry()
    {
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        _input.Inputs.Disable();
    }
}
