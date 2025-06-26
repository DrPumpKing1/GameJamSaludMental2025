using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBar : MonoBehaviour
{
    [SerializeField] private TutorialStar _star;
    
    [Header("UI Elements")]
    [SerializeField] private Image _bar;

    private void Update() => UpdateFillAmount();

    private void UpdateFillAmount()
    {
        if(_star == null) return;
        _bar.fillAmount = Mathf.Clamp01(_star.CurrentTimeOnStar / _star.TimeOnStar);
    }
}
