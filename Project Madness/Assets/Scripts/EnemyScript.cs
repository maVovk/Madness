using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    public float damage = 20f; // дамаг
    public float speedAttack = 2.0f;

    //public GameObject enemy;
    GameObject player; // герой

    public int dist;
    Transform target;
    Transform spawnPos;
    bool goHome = false;
    bool attack = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack = true;
        }
    }

    private void Start()
    {
        spawnPos = this.transform;
        target = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (goHome)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.deltaTime);
        }
        CheckDistance();
    }  
    
    void FixedUpdate()
    {
        if (player != null && attack && Time.time % speedAttack == 0f)
        {
            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position) * 1000f);
            player.GetComponent<HealthSystem>().TakeDamage(damage);
            /*******************************
            
                АНИМАЦИИ УДАРА ВРАГОВ

            ******************************/
        }
    }

    private void CheckDistance()
    {
        if (target != null && Vector3.Distance(target.position, transform.position) <= dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goHome = true;
    }
}

