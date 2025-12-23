using System;
using JJW._02_Script.Sound;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerSO mixerSo;
    [SerializeField] private Slider slider;

    private void Reset()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (mixerSo.GetFloat(out var value))
        {
            value /= 20f;
            value = Mathf.Pow(10, value);
            slider.value = value;
        }
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        mixerSo.SetNormalized(volume);
    }
}
