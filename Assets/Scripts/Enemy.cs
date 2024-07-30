using UnityEngine;
using System.Collections.Generic;





public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public int soulDrop = 1;
    public float speed = 5.0f;
    public float attackDamage = 10.0f;
    public float attackCooldown = 1.0f;
    private Transform towerTransform;
    private Tower towerScript;
    private bool isDead = false;
    private float attackCooldownTimer = 0.0f;
    //private bool killedByPlayer = false;  // Flag to indicate if the enemy was killed by the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))  // Ensure bullets have the "Bullet" tag
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);  // Apply damage from the bullet
                Destroy(other.gameObject);  // Destroy the bullet upon collision
            }
        }
    }

    void Start()
    {
        GameObject tower = GameObject.Find("Tower");
        if (tower != null)
        {
            towerTransform = tower.transform;
            towerScript = tower.GetComponent<Tower>();
        }
    }

    void Update()
    {
        if (isDead) return;

        transform.position = Vector3.MoveTowards(transform.position, towerTransform.position, speed * Time.deltaTime);

        attackCooldownTimer -= Time.deltaTime;
        if (Vector3.Distance(transform.position, towerTransform.position) <= 0.5f && attackCooldownTimer <= 0)
        {
            AttackTower();
            attackCooldownTimer = attackCooldown;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            //killedByPlayer = true;
            Die(); // Call Die() immediately when health is 0 or below
            Destroy(gameObject);
        }
    }


    private void AttackTower()
    {
        if (towerScript != null)
        {
            towerScript.TakeDamage(attackDamage);
            Die();
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Check for SoulManager.instance before using it
        //if (killedByPlayer && SoulManager.instance != null)
        //{
        //    SoulManager.instance.AddSouls(soulDrop);
        //}
        //else if (killedByPlayer) // Log error only if the enemy was killed by the player
        //{
        //    Debug.LogError("SoulManager instance not found.");
        //}
    }
}
