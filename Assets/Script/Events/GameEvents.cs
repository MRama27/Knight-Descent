using UnityEngine;
using UnityEngine.Events;

// Kelas statis untuk event global
public static class GameEvents
{
    // Event ini dipanggil setiap kali ada Boss yang mati.
    // Kita mengirimkan objek Boss yang mati sebagai argumen (GameObject).
    public static UnityEvent<GameObject> BossDefeated = new UnityEvent<GameObject>();
}