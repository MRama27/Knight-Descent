using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// using System.Numerics; // Hapus atau komentari

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 3f;
    public float pathUpdateSeconds = 0.3f; // Diperbaiki penamaan

    [Header("Physics")]
    public float speed = 1000f; // Nilai disesuaikan berdasarkan Inspector (1000)
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behaviour")]
    public bool followEnable = true;
    public bool jumpEnable = true;
    public bool directionLookEnable = true;

    private Path path;
    private int currentWaypoint = 0; // Tipe data int
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    // Tambahkan referensi ke script Goblin (jika diperlukan untuk mengambil data, tapi kita fokus ke pergerakan)
    // Goblin goblin; 

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        // goblin = GetComponent<Goblin>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds); 
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnable)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnable && TargetInDistance() && seeker.IsDone()) // Perbaikan sintaks dan nama variabel
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);   
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // Cek darat
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Gunakan Force Mode untuk pergerakan A* yang lebih baik
        Vector2 force = direction * speed * Time.deltaTime; 
        
        if (jumpEnable && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        // Terapkan gaya dorong
        rb.AddForce(force); 

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (directionLookEnable)
        {
            // LOGIKA FLIPPING YANG DIBALIK (jika musuh berjalan mundur)
            // Kanan = Skala X Positif
            if (rb.linearVelocity.x > 0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            // Kiri = Skala X Negatif
            else if (rb.linearVelocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        
        // Catatan: Jika Anda ingin animasi berjalan, Anda harus mengirim data kecepatan 
        // (rb.linearVelocity.magnitude) ke Animator di sini.
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0; 
        }
    }
}