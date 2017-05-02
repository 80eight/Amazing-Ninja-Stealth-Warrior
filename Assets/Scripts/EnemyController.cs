using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public Text EnemiesText;
    private Vector3 currentDes;
    private Vector3 pos1;
    private Vector3 pos2;

    private static float moveSpeed;
    private static float range;
    private static float throwForce;
    private static float throwSpeed = 0.75f; //Use 0.3f to make the enemy throw twice
    private static float deathTime = 2;
    private float deathTimer;
    private float health;
    private float throwTimer;
    private float shurikenDamage;
    public int Enemies;
    private bool dead;

    public GameObject Shuriken;
    private GameObject S2;

    private GameObject player;
    Animator anim;
    PlayerController PlayerC;
    ShurikenController sc;
    SpriteRenderer sprite;
    public Sprite DeathSprite;

	// Use this for initialization
	void Start () {
        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x + 3, transform.position.y, 0);

        EnemiesText = GameObject.Find("EnemiesAliveText").GetComponent<Text>();

        Enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        player = GameObject.Find("Player");
        PlayerC = player.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if(gameObject.layer == 9)
        {
            health = 100;
            throwForce = 6.25f;
            moveSpeed = 3;
        }
        else if(gameObject.layer==10)
        {
            health = 125;
            throwForce = 6.75f;
            moveSpeed = 3.5f;
        }
        else
        {
            health = 175;
            moveSpeed = 4;
            throwForce = 7;
        }
        currentDes = pos1;
        range = 5;
        dead = false;
	}

    // Update is called once per frame
    void Update() {
        #region Checks and Updates
        if(health <= 0)
        {
            deathTimer += Time.deltaTime;
        }
        if(deathTimer > deathTime)
        {
            Destroy(gameObject);
        }
        if (health <= 0 && !dead)
        {
            Die();
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            anim.enabled = false;
            sprite.sprite = DeathSprite;
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
            if (!a[i].GetComponent<EnemyController>().dead)
            {
                Enemies += 1;
            }
        }

        if(transform.position.x <= pos1.x && currentDes == pos1)
        {
            currentDes = pos2;
        }
        else if(transform.position.x >= pos2.x && currentDes == pos2)
        {
            currentDes = pos1;
        }
        #endregion

        #region Movement
        if (currentDes == pos1 && !dead)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if(throwTimer > throwSpeed && player.transform.position.x - transform.position.x < 0)
            {
                if(player.transform.position.y - transform.position.y < -1 || player.transform.position.y - transform.position.y > 1)
                {

                }
                
                else
                {
                    Shoot();
                }
            }
        }
        else if(currentDes == pos2 &&!dead)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if(throwTimer > throwSpeed && player.transform.position.x - transform.position.x > 0)
            {
                if (player.transform.position.y - transform.position.y < -1 || player.transform.position.y - transform.position.y > 1)
                {

                }

                else
                {
                    Shoot();
                }
            }
        }

        throwTimer += Time.deltaTime;
        if(!dead)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentDes.x,transform.position.y,currentDes.z), moveSpeed * Time.deltaTime);
            anim.SetBool("Moving", true);
        }
        #endregion
    }

    void LateUpdate()
    {
        EnemiesText.text = "Enemies Alive: " + Enemies;
        Enemies = 0;
    }

    void Shoot()
    {
        if(currentDes == pos1)
        {
            S2 = Instantiate(Shuriken, new Vector3(transform.position.x - 0.6f, transform.position.y, transform.position.z), transform.rotation);
            S2.GetComponent<Rigidbody2D>().velocity = Vector2.left * throwForce;
            sc = S2.GetComponent<ShurikenController>();
            if(gameObject.layer == 11)
            {
                sc.damage = 40;
            }

        }
        else
        {
            S2 = Instantiate(Shuriken, new Vector3(transform.position.x + 0.6f, transform.position.y, transform.position.z), transform.rotation);
            S2.GetComponent<Rigidbody2D>().velocity = Vector2.right * throwForce;
            sc = S2.GetComponent<ShurikenController>();
        }
        throwTimer = 0;
    }

    void Die()
    {
        dead = true;
    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if(sc != null)
        {
            if (col.gameObject == sc.gameObject)
            {
                Destroy(col.gameObject);
            }
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }

}
