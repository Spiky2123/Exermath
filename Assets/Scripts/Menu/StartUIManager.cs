using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    [SerializeField] private int _gameId;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _easyButton;
    [SerializeField] private Button _mediumButton;
    [SerializeField] private Button _hardButton;
    [SerializeField] private GameObject _difficultyMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _easyDescription;
    [SerializeField] private GameObject _mediumDescription;
    [SerializeField] private GameObject _hardDescription;
    [SerializeField] private TMP_Text _maxScore;
    [SerializeField] private TMP_Text _maxStreak;

    private InputSystem_Actions _input;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Inputs.Enable();
        _input.Inputs.Exit.performed += Exit_performed;

        _startButton.onClick.AddListener(StartButtonAction);
        _optionsButton.onClick.AddListener(OptionsButtonAction);
        _easyButton.onClick.AddListener(() => GameStart(0));
        _mediumButton.onClick.AddListener(() => GameStart(1));
        _hardButton.onClick.AddListener(() => GameStart(2));

        OnExit();
    }

    private void Exit_performed(InputAction.CallbackContext context)
    {
        _difficultyMenu.SetActive(false);
        _optionsMenu.SetActive(false);
    }

    private void StartButtonAction()
    {
        _difficultyMenu.SetActive(true);
    }

    private void GameStart(int difficulty)
    {
        StateNameController.Difficulty = difficulty;
        SceneManager.LoadScene(_gameId);
    }

    private void OptionsButtonAction()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        _input.Inputs.Disable();
    }

    public void OnHover(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                _easyDescription.SetActive(true);
                break;
            case 1:
                _mediumDescription.SetActive(true);
                break;
            case 2:
                _hardDescription.SetActive(true);
                break;
        }

        _maxScore.text = StateNameController.MaxScore[difficulty].ToString();
        _maxStreak.text = StateNameController.MaxStreak[difficulty].ToString();
    }

    public void OnExit()
    {
        _easyDescription.SetActive(false);
        _mediumDescription.SetActive(false);
        _hardDescription.SetActive(false);
        _maxScore.text = "-";
        _maxStreak.text = "-";
    }
}
