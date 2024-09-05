using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimeLasts => slider.value;

    [SerializeField] private float maxTime = 20f;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderBar;
    [SerializeField] private Gradient gradient;
    public readonly UnityEvent TimeOut = new();

    private void Awake()
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }

    private void Update()
    {
        slider.value -= Time.deltaTime;
        if (slider.value <= 0)
        {
            TimeOut.Invoke();
            Debug.LogWarning("Timer is out");
        }

        float normalizedTime = slider.value / maxTime;
        sliderBar.color = gradient.Evaluate(normalizedTime);
    }
}
