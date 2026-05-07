using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private float _maxTime = 30;

    private float _time;
    private bool _isPlaying;
    private float _penalty = 6.5f;

    public EventHandler OnTimerEnd;
    public float MaxTime => _maxTime;

    private void Awake()
    {
        SetPlayState(true);
        ResetTime();
    }

    private void Update()
    {
        if(_isPlaying)
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
            }
            else if (_time < 0)
            {
                ResetTime();
                OnTimerEnd?.Invoke(this, EventArgs.Empty);
            }
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);
        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTime()
    { 
        _time = _maxTime; 
    }

    public void TimePenalty()
    {
        _time -= _penalty;
        if (_penalty < _maxTime)
            _penalty += 1;
    }

    public void SetPlayState(bool n)
    { 
        _isPlaying = n; 
    }
}
