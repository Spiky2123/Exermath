using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PostProccessing _postProcessing;

    private int _difficulty;
    private int _remainingHearts=3;
    private int _score;
    private int _streak;
    private int _totalStreak;
    private EcuationManager _ecuationManager;
    private TimerManager _timerManager;
    private SolutionManager _solutionManager;
    
    public int Difficulty => _difficulty;
    public EventHandler OnHeartDestroyed;
    public EventHandler<OnGameEndedArgs> OnGameOver;
    public class OnGameEndedArgs : EventArgs
    {
        public int Score;
        public int Streak;
    }

    private void Awake()
    {
        _difficulty = StateNameController.Difficulty;
        _ecuationManager = GetComponent<EcuationManager>();
        _timerManager = GetComponent<TimerManager>();
        _solutionManager = GetComponent<SolutionManager>();

        _timerManager.OnTimerEnd += (object sender, EventArgs e) => Penalty(true);
        _solutionManager.OnSolution += OnSolution;

        NewEcuation();
    }

    private void OnSolution(object sender, int e)
    {
        if (e == _ecuationManager.Solution)
        {
            _score++;
            _streak++;

            NewEcuation();
        }
        else
        {
            Penalty(false);
        }
    }

    private void Penalty(bool reason)
    {
        if(_streak > _totalStreak)
        {
            _totalStreak = _streak;
            _streak = 0;
        }

        switch (_difficulty)
        {
            case 0:
                if(_remainingHearts > 1)
                {
                    _remainingHearts--;
                    OnHeartDestroyed?.Invoke(this, EventArgs.Empty);
                    NewEcuation();
                }
                else
                {
                    GameOver();
                }
                break;
            case 1:
                if(reason)
                {
                    GameOver();
                }
                else
                {
                    _timerManager.TimePenalty();
                }
                break;
            case 2:
                GameOver();
                break;
        }

        _postProcessing.OnDamage();
    }

    private void NewEcuation()
    {
        _timerManager.ResetTime();
        _ecuationManager.NewEcuation();
    }

    private void GameOver()
    {
        if (_score > StateNameController.MaxScore[_difficulty])
        { 
            StateNameController.MaxScore[_difficulty] = _score; 
        }
        if (_totalStreak > StateNameController.MaxStreak[_difficulty])
        {
            StateNameController.MaxStreak[_difficulty] = _totalStreak;
        }

        OnGameOver?.Invoke(this, new OnGameEndedArgs { Score = _score, Streak = _totalStreak });
    }
}
