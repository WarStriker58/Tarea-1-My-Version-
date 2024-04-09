using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxHealth = 5f;
    private float currentHealth;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;

    public Text healthText;
    //public Text scoreText; // Comentado: UI de puntaje

    //private int score = 0; // Comentado: variable de puntaje

    private Vector2 movementInput;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        //UpdateScoreUI(); // Comentado: actualizar la UI de puntaje al inicio
    }

    void Update()
    {
        Vector2 moveInput = movementInput;
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 shootDirection = GetShootDirection();
            ShootBullet(shootDirection);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();
            movementInput = inputDirection;
        }
        else if (context.canceled)
        {
            movementInput = Vector2.zero;
        }
    }

    Vector3 GetShootDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;
        return (mousePosition - transform.position).normalized;
    }

    void ShootBullet(Vector3 shootDirection)
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("El prefab del proyectil debe tener un componente Rigidbody2D para moverse correctamente.");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = "Lives: " + currentHealth.ToString();
    }

    void Die()
    {
        SceneManager.LoadScene("DefeatScene"); // Cargar la escena de derrota
    }

    // Método para aumentar el puntaje cuando se recolecta una moneda
    /*public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI(); // Actualizar la UI de puntaje cuando se recolecta una moneda
    }*/

    // Método para actualizar la UI de puntaje
    /*void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }*/

    // Método para detectar colisiones con monedas
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Obtener el script Coin asociado a la moneda y aumentar el puntaje
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                IncreaseScore(coin.scoreValue);
                Destroy(other.gameObject);
            }
        }
    }*/
}