using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Pastikan ini terhubung di Inspector
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // Nama parameter yang sudah terkonfirmasi benar
    private const string MUSIC_PARAM = "MusicVolume"; 
    private const string SFX_PARAM = "SFXVolume";

    private void Start()
    {
        LoadVolume();
    }
    
    // Dipanggil oleh event On Value Changed dari Music Slider
    public void SetMusicVolume(float volume)
    {
        // Konversi nilai linier (0.0001f - 1f) ke skala logaritmik Desibel (dB)
        float dbVolume = Mathf.Log10(volume) * 20; 
        
        myMixer.SetFloat(MUSIC_PARAM, dbVolume);
        
        PlayerPrefs.SetFloat(MUSIC_PARAM, volume);
    }

    // Dipanggil oleh event On Value Changed dari SFX Slider
    public void SetSFXVolume(float volume)
    {
        float dbVolume = Mathf.Log10(volume) * 20;
        myMixer.SetFloat(SFX_PARAM, dbVolume);
        
        PlayerPrefs.SetFloat(SFX_PARAM, volume);
    }
    
    private void LoadVolume()
    {
        // Muat volume yang tersimpan
        float savedMusicVol = PlayerPrefs.GetFloat(MUSIC_PARAM, 1.0f);
        float savedSFXVol = PlayerPrefs.GetFloat(SFX_PARAM, 1.0f);
        
        // Terapkan nilai dB ke mixer
        SetMusicVolume(savedMusicVol); 
        SetSFXVolume(savedSFXVol);

        // Atur posisi slider UI (penting agar UI sesuai dengan volume yang dimuat)
        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVol;
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVol;
        }
    }
}