using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;    // 拖入你建立的 AudioMixer
    public Slider musicSlider;       // 拖入 Music Slider
    public Slider sfxSlider;         // 拖入 SFX Slider

    const string MUSIC_KEY = "MusicPref"; // PlayerPrefs key
    const string SFX_KEY = "SFXPref";
    const string MUSIC_PARAM = "MusicVolume"; // AudioMixer 內的 exposed name
    const string SFX_PARAM = "SFXVolume";

    void Start()
    {
        // 讀取儲存值，預設 1f (最大)
        float musicVal = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVal = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        if (musicSlider != null)
        {
            musicSlider.value = musicVal;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVal;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // 立即套用
        SetMusicVolume(musicVal);
        SetSFXVolume(sfxVal);
    }

    // Slider 介於 0~1，轉成 dB：0 -> -80 (靜音), 1 -> 0 dB
    public void SetMusicVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, sliderValue);
        if (audioMixer != null)
        {
            if (sliderValue <= 0.0001f) audioMixer.SetFloat(MUSIC_PARAM, -80f);
            else audioMixer.SetFloat(MUSIC_PARAM, Mathf.Log10(sliderValue) * 20f);
        }
    }

    public void SetSFXVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat(SFX_KEY, sliderValue);
        if (audioMixer != null)
        {
            if (sliderValue <= 0.0001f) audioMixer.SetFloat(SFX_PARAM, -80f);
            else audioMixer.SetFloat(SFX_PARAM, Mathf.Log10(sliderValue) * 20f);
        }
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
