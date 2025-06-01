using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float receiveDamage = 1f;
    private float currentHealth;
    [SerializeField] private string sceneName;

    [SerializeField] private Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(receiveDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(sceneName);
    }
}
    

