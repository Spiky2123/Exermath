using System;
using UnityEngine;
using UnityEngine.UI;

public class SolutionManager : MonoBehaviour
{
    [SerializeField] private Button _sum;
    [SerializeField] private Button _dif;
    [SerializeField] private Button _multiply;
    [SerializeField] private Button _div;

    public EventHandler<int> OnSolution;

    private void Awake()
    {
        _sum.onClick.AddListener(() => OnSolution?.Invoke(this, 1));
        _dif.onClick.AddListener(() => OnSolution?.Invoke(this, 2));
        _multiply.onClick.AddListener(() => OnSolution?.Invoke(this, 3));
        _div.onClick.AddListener(() => OnSolution?.Invoke(this, 4));
    }
}
