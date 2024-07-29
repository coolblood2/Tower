using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float attackRadius = 3.0f;
    public float attackCooldown = 1.0f;
    public float maxHealth = 100f;
    private float currentHealth;
    private float attackCooldownTimer = 0.0f;
    private Transform target;
    private CircleCollider2D circleCollider;
    private List<Collider2D> detectedEnemies = new List<Collider2D>();

    public TextMeshProUGUI healthText;
    public GameManager gameManager;  // Ensure this is assigned in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();

        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = attackRadius;
        circleCollider.isTrigger = true;
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (target == null || Vector2.Distance(transform.position, target.position) > attackRadius)
        {
            FindTarget();
        }

        if (target != null && attackCooldownTimer <= 0)
        {
            Shoot();
            attackCooldownTimer = attackCooldown;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            detectedEnemies.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            detectedEnemies.Remove(other);
        }
    }

    void FindTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRadius)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        if (target == null || bulletPrefab == null || firePoint == null)
        {
            return;
        }

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameManager.GameOver();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0");
        }
    }
}
