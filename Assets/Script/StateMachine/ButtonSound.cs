using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Variabel untuk klip audio. Variabel ini tetap berada di tombol.
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // Di Start, kita tidak perlu lagi mencari AudioSource!
    void Start()
    {
        // Tidak ada kode apa-apa di sini, karena SoundManager yang akan menangani AudioSource.
    }

    // Dipanggil saat pointer mouse memasuki area tombol (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Meminta SoundManager untuk memutar suara hover
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(hoverSound);
        }
    }

    // Dipanggil saat tombol di-klik
    public void OnPointerClick(PointerEventData eventData)
    {
        // Meminta SoundManager untuk memutar suara click
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(clickSound);
        }
    }
}
