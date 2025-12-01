using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Instance statis SoundManager, memungkinkan akses global.
    public static SoundManager Instance; 
    
    // Referensi ke AudioSource yang kita tambahkan di Langkah 1.2
    private AudioSource audioSource;

    // Dipanggil saat objek pertama kali dimuat
    void Awake()
    {
        // --- Pola Singleton untuk memastikan hanya ada satu Sound Manager ---
        if (Instance == null)
        {
            Instance = this;
            // Penting: Pastikan objek ini TIDAK HANCUR saat pindah Scene.
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Jika sudah ada instance lain, hancurkan diri sendiri.
            Destroy(gameObject);
        }
        // -----------------------------------------------------------------

        // Dapatkan AudioSource dari objek ini.
        audioSource = GetComponent<AudioSource>();
    }

    // Fungsi publik yang dipanggil oleh tombol untuk memutar suara
    public void PlaySound(AudioClip clip)
    {
        // Memastikan klip audio dan AudioSource tersedia sebelum memutar
        if (clip != null && audioSource != null)
        {
            // PlayOneShot digunakan agar suara yang baru diputar tidak menghentikan
            // suara lain yang mungkin sedang diputar (misalnya, musik latar).
            audioSource.PlayOneShot(clip);
        }
    }
}