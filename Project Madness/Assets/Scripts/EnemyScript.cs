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
    public  Animator anim;

    public int dist;
    Transform target;
    Transform spawnPos;
    bool goHome = false;
    bool attack = false;
    public bool freez = false;
    public float freezTime = 0f;
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
    }

    void Update()
    {
        if (goHome)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.deltaTime);
        }
        CheckDistance();
        time += Time.fixedDeltaTime;
        if (freez)
            freezTime -= Time.fixedDeltaTime;
        /*if (hp <= 0)
            Destroy(enemy);*/
    }  
    
    void FixedUpdate()
    {
        if (player != null && attack && System.Math.Round(time, 1) >= speedAttack && !Input.GetKey(KeyCode.Space))
        {
            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position) * 1000f);
            player.GetComponent<HealthSystem>().TakeDamage(damage);
            time = 0f;
            /*******************************
            
                АНИМАЦИИ УДАРА ВРАГОВ

            ******************************/
        }
        if (freez)
            attack = false;
        if (freezTime <= 0)
            freez = false;
    }

    private void CheckDistance()
    {
        if (target != null && Vector3.Distance(target.position, transform.position) <= dist && !freez)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("Walking", true);
        }
		else
		{
            anim.SetBool("Walking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        goHome = true;
        attack = false;
    }
}

