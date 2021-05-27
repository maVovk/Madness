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
    //public float defence = 0.5f; // защита

    //Rigidbody2D rb; // ригидбоди, который отвечает за нахождение врагов в радиусе видимости

    public GameObject enemy;
    public GameObject player; // герой

    //public float hp = 100f; // HP
    //public float strength = 0.5f; // def

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
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (goHome)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.deltaTime);
        }
        CheckDistance();
        /*if (hp <= 0)
            Destroy(enemy);*/
    }  
    
    void FixedUpdate()
    {
        if (attack && Time.time % speedAttack == 0f)
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
        if (Vector3.Distance(target.position, transform.position) <= dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goHome = true;
    }
}

