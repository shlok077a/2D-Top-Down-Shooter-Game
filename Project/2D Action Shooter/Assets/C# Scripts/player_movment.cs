using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movment : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;

    private Vector2 moveAmount;
    private Animator anim;

    public int health;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    public Animator hurtAnim;
    private SceneTransitons sceneTransitions;

    public Joystick moveJoystick;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitons>();
    }


    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveJoystick.InputDir != Vector3.zero)
        {
            moveInput = moveJoystick.InputDir;
        }

        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int DamageAmount)
    {
        hurtAnim.SetTrigger("hurt");
        health -= DamageAmount;
        UpdateHealthUI(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }

    }

    public void ChangeWeapon(weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHealthUI(int currentHealth)
    {

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < currentHealth)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }

        }

    }

    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }


}
