using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    public float damage = 34f; // дамаг
    public float speedAttack = 0.5f;
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
    float time = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack = true;
        }
        else
            attack = false;
    }

    private void Start()
    {
        spawnPos = this.transform;
        target = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (goHome)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.deltaTime);
        }
        CheckDistance();
        time += Time.deltaTime;
        /*if (hp <= 0)
            Destroy(enemy);*/
    }  
    
    void FixedUpdate()
    {
        if (player != null && attack && System.Math.Round(time, 1) % speedAttack == 0f)
        {
            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position) * 1000f);
            player.GetComponent<HealthSystem>().TakeDamage(damage);
            time = 0f;
            /*******************************
            
                АНИМАЦИИ УДАРА ВРАГОВ

            ******************************/
        }
    }

    private void CheckDistance()
    {
        if (target != null && Vector3.Distance(target.position, transform.position) <= dist && !attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goHome = true;
        attack = false;
    }
}

