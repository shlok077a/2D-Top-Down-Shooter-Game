using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;

    private int halfHealth;
    private Animator anim;

    public int damage;
    public GameObject splash, effect;

    Animator cameraShake;

    private Slider healthBar;
    private SceneTransitons sceneTransitions;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        cameraShake = Camera.main.GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindObjectOfType<SceneTransitons>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(splash, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
;        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<player_movment>().TakeDamage(damage);
        }
    }

    public void shake()
    {
        cameraShake.SetTrigger("shake");
    }

}