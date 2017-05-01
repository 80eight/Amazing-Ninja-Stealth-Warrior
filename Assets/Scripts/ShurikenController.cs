using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour {

    Rigidbody2D rb;
    EnemyController ec;
    PlayerController pc;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(transform.position.y < - 5)
        {
            Destroy(gameObject);
        }

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(Physics2D.IsTouchingLayers(GetComponent<Collider2D>(),LayerMask.GetMask("Floor")))
        {
            rb.velocity = rb.velocity * 0.1f;
            GetComponent<Collider2D>().isTrigger = true;
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            ec = col.gameObject.GetComponent<EnemyController>();
            ec.TakeDamage(30);
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("Player"))
        {
            pc.TakeDamage(30);
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("Shuriken"))
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
