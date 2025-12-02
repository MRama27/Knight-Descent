using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // 👈 PENTING: Tambahkan untuk Coroutine

public class GameOverPlayer : MonoBehaviour
{
    public GameObject gameOverScreen; // Drag your Game Over UI here in the Inspector
    public float delayTime = 2.0f; // 👈 Waktu jeda (dalam detik)

    private Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerDamageable = player.GetComponent<Damageable>();
            
            if (playerDamageable != null)
            {
                // Mendaftarkan metode OnPlayerDeath sebagai listener
                playerDamageable.damageableDeath.AddListener(OnPlayerDeath);
            }
            else
            {
                Debug.LogError("Komponen Damageable tidak ditemukan pada objek Player.");
            }
        }
        else
        {
            Debug.LogError("Objek dengan Tag 'Player' tidak ditemukan.");
        }
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Game Over triggered. Starting delay...");
        // Memulai Coroutine untuk menunda tampilan panel Game Over
        StartCoroutine(ShowGameOverScreenAfterDelay(delayTime)); 
    }

    // Metode Coroutine baru untuk menunda tampilan UI
    private IEnumerator ShowGameOverScreenAfterDelay(float delay)
    {
        // 1. Tunggu selama waktu jeda yang ditentukan (misalnya 2 detik)
        yield return new WaitForSeconds(delay);
        
        // 2. Tampilkan panel Game Over
        ShowGameOverScreen();
    }
    
    // Metode ini sekarang hanya fokus pada pengaktifan UI dan jeda waktu
    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // Activate the Game Over screen
        Time.timeScale = 0; // Pause the game
        Debug.Log("Game Over screen displayed and game paused.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}