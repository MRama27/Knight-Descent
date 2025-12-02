using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHandler : MonoBehaviour
{
    // Karena dipasang pada panel Selamat, kita tidak perlu slot victoryScreen
    
    private void Start()
    {
        // Pastikan panel nonaktif saat Start
        gameObject.SetActive(false); 
        
        // 🚨 PERUBAHAN: Dengarkan event statis global
        // HANYA dipanggil saat Boss yang memiliki Tag "Boss" mati
        GameEvents.BossDefeated.AddListener(OnBossDeath);
    }

    // Ubah OnBossDeath untuk menerima argumen GameObject (Boss yang dikalahkan)
    private void OnBossDeath(GameObject defeatedBoss)
    {
        Debug.Log("Selamat! Boss (" + defeatedBoss.name + ") telah dikalahkan.");
        ShowVictoryScreen();
    }

    private void ShowVictoryScreen()
    {
        // Aktifkan objek tempat skrip ini terpasang (panel Selamat)
        gameObject.SetActive(true); 
        Time.timeScale = 0; 
    }

    public void GoHome()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Main Menu"); 
    }
}