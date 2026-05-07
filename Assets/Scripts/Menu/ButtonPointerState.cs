using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPointerState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private StartUIManager _startUiManager;
    [SerializeField] private int difficulty;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _startUiManager.OnHover(difficulty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _startUiManager.OnExit();
    }
}
