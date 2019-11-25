using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Transform healthBarParent; // Refers to Empty Canvas element
    public GameObject healthBarUIPrefab; // Healthbar Prefab to spawn
    public Transform healthBarPoint; // Refers to transform for following health bar 

    private int health = 0;
    private Slider healthSlider;
    private Renderer rend;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn a new Healthbar under 'HealthBarParent'
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        // Get Slider component from new Healthbar
        healthSlider = clone.GetComponent<Slider>();
        // Set health to maxHealth
        health = maxHealth;
        // Get the renderer attached to Enemy
        rend = GetComponent<Renderer>();
    }

    // When the GameObject gets Destroyed
    void OnDestroy()
    {
        // If healthSlider exists
        if (healthSlider)
        {
            // Take the HealthSlider with it
            Destroy(healthSlider.gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // If the renderer (Enemy) is visible 
        if (rend.isVisible)
        {
            // Activate the HealthBar
            healthSlider.gameObject.SetActive(true);
            // Update position of healthbar with enemy
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPoint.position);
            healthSlider.transform.position = screenPosition;
        }
        // If it is NOT visible
        else
        {
            // Deactivate the HealthBar
            healthSlider.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce health with damage
        health -= damage;
        // Update Health Slider
        healthSlider.value = (float)health / (float)maxHealth;
        // If health reaches zero
        if (health < 0)
        {
            // Destroy the GameObject
            Destroy(gameObject);
            gameOver.SetActive(true);
        }
    }
}