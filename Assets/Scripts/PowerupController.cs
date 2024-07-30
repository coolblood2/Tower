using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public float lifetime = 10f;
    private float timer;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    // Handle player interaction (you'll add this in your player controller script)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activate powerup effect
            // ... (Your implementation here)

            Destroy(gameObject);
        }
    }
}
