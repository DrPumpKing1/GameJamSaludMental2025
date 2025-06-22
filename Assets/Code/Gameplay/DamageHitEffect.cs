using System;
using System.Collections;
using UnityEngine;

public class DamageHitEffect : MonoBehaviour
{
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _flashDuration;
    [SerializeField] private float _maxFlashAmount;
    [SerializeField] private AnimationCurve _flashCurve;
    [SerializeField] private SpriteRenderer _spriteRenderers;
    private Material _material;
    
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        _material = _spriteRenderers.material;
    }

    private void Start()
    {
        SetFlashColors();
        ResetFlash();
    }

    public void CallFlash()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        
        _flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentFlashAmount = (1 - _flashCurve.Evaluate(Mathf.Clamp01(elapsedTime / _flashDuration))) * _maxFlashAmount;
            SetFlashAmount(currentFlashAmount);
            
            yield return null;
        }
        
        ResetFlash();
    }

    private void ResetFlash()
    {
        SetFlashAmount(0f);
    }

    private void SetFlashColors()
    {
        _material.SetColor("_FlashColor", _flashColor);
    }

    private void SetFlashAmount(float flashAmount)
    {
        _material.SetFloat("_FlashAmount", flashAmount);
    }
}
