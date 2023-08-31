using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    private Animator m_animator;
    private Rigidbody2D m_body2d;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public static int maxHealth = 100;
    
    int currentHeath;


    public static int attackDamage = 20;
    private bool grounded = false;
    public int jumpHeight = 15;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        currentHeath = maxHealth;
        BotController.OnBotDeath += increaseExp;
    }

    public void increaseExp()
    {
       
    }


    // Update is called once per frame
    void Update()
    {

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
            m_animator.SetInteger("speed", 1);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-5.0f, 5.0f, 1.0f);
            m_animator.SetInteger("speed", 1);
        }
        else
        {
            m_animator.SetInteger("speed", 0);
        }

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // enter mouse left to hero attack 
        if (Input.GetMouseButtonDown(0))
        {
            heroAttack();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, jumpHeight);
                m_animator.SetTrigger("isJumping");
            }

        }
    }

    private void heroAttack()
    {
        // set animation attack
        m_animator.SetTrigger("attack");

        // detack enermy in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BotController>().TakeDamage(attackDamage);
        }

    }

    

    public void TakeDamage(int damage)
    {
        currentHeath -= damage;
        HealthBar.SettingHealth(currentHeath);
        Debug.Log("HP hero:" + currentHeath);

        if (currentHeath <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
       
        GameObject.Destroy(this.gameObject);
        Debug.Log("die");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (other.gameObject.tag == "Heart")
        {
            HealthBar.Heart();
            GameObject gameObject = GameObject.Find("Heart");
        }
    }


}
