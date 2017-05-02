using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private float moveSpeed;
    private static float jumpSpeed;
    private static float throwForce;
    private static float ThrowSpeed = 0.25f;
    private static float maxHealth;
    private static float regenSpeed = 2;
    private static float CeilingHangTime = 3.5f;
    private static float DeathTime = 2;
    private float DeathTimer;
    private float CeilingTimer;
    public float health;
    private float ThrowTimer;
    private bool OnCeiling;

    public GameObject PauseMenu;
    public GameObject Shuriken;
    private GameObject S2;
    private GameObject floorg;
    public AudioClip[] PlayerAudio = new AudioClip[3];

    private EnemyController EC;

    private SpriteRenderer SpriteR;
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D c;
    private AudioSource AudioSource;
    private LayerMask floor;
    public Slider healthSilder;
    public Sprite DeathSprite;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        floor = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        SpriteR = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();

        maxHealth = 100;
        health = maxHealth;
        throwForce = 7;
        jumpSpeed = 6;
        moveSpeed = 5;
        healthSilder.maxValue = maxHealth;
        healthSilder.minValue = 0;
        healthSilder.value = maxHealth;
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        #region Health
        healthSilder.value = health;
        if(health < maxHealth)
        {
            health += Time.deltaTime * regenSpeed;
        }
        else
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            DeathTimer += Time.deltaTime;
            anim.enabled = false;
            SpriteR.sprite = DeathSprite;
            enabled = false;
            AudioSource.clip = PlayerAudio[2];
        }
        if(DeathTimer > DeathTime)
        {
            Destroy(gameObject);
        }
        #endregion
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            EC = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        }
        if (EC != null)
        {
            if (EC.Enemies <= 0)
            {
                AudioSource.clip = PlayerAudio[1];
                AudioSource.Play();
            }
        }
        ThrowTimer += Time.deltaTime;
        #region Fire
        if (Input.GetMouseButtonDown(0) && ThrowTimer > ThrowSpeed)  
        {
            AudioSource.clip = PlayerAudio[0];
            AudioSource.Play();
            if (transform.eulerAngles.y == 0)
            {

                if (OnCeiling)
                {
                    S2 = Instantiate(Shuriken, new Vector3(transform.position.x - 0.7f, transform.position.y, 0), transform.rotation);
                    S2.GetComponent<Rigidbody2D>().velocity = Vector2.left * throwForce;
                }
                else
                {
                    S2 = Instantiate(Shuriken, new Vector3(transform.position.x + 0.7f, transform.position.y, 0), transform.rotation);
                    S2.GetComponent<Rigidbody2D>().velocity = Vector2.right * throwForce;
                }
            }
            else
            {
                if (!OnCeiling)
                {
                    S2 = Instantiate(Shuriken, new Vector3(transform.position.x - 0.7f, transform.position.y, 0), transform.rotation);
                    S2.GetComponent<Rigidbody2D>().velocity = Vector2.left * throwForce;
                }
                else
                {
                    S2 = Instantiate(Shuriken, new Vector3(transform.position.x + 0.7f, transform.position.y, 0), transform.rotation);
                    S2.GetComponent<Rigidbody2D>().velocity = Vector2.right * throwForce;
                }
            }
            ThrowTimer = 0;
        }
        #endregion
        #region Pause
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(PauseMenu.active == false)
            {
                Cursor.visible = true; 
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
            else
            {
                Cursor.visible = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Movement
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        if (Physics2D.IsTouchingLayers(c, floor))
        {
            rb.velocity = new Vector2(0, y *  jumpSpeed);
            if (floorg.transform.position.y - transform.position.y > 0.8)
            {
                OnCeiling = true;
                if(CeilingTimer < CeilingHangTime)
                {
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 180);
                    if (x > 0)
                    {
                        x *= -1;
                        transform.eulerAngles = new Vector3(0, 180, 180);
                    }
                    else if (x < 0)
                    {
                        x *= -1;
                        transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                }
                else
                {
                    rb.velocity = new Vector3(0, -1);
                }
                CeilingTimer += Time.deltaTime;
            }
            else
            {
                OnCeiling = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                CeilingTimer = 0;
            }
        }
        else
        {
            OnCeiling = false;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
            x *= -1;
        }
        else if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }
        transform.Translate(x * Time.deltaTime * moveSpeed, 0, 0);

        if (x != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        #endregion
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Shuriken"))
        {
            ShurikenController sc = col.gameObject.GetComponent<ShurikenController>();
            Destroy(col.gameObject);
            Destroy(sc);
        }
        else if(col.gameObject.layer == 8 )
        {
            floorg = col.gameObject;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
