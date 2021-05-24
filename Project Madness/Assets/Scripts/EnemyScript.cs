using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    public float damage = 20f; // дамаг
    public float defence = 0.5f; // защита
    Rigidbody2D rb; // ригидбоди, который отвечает за нахождение врагов в радиусе видимости
    public GameObject enemy;
    public Transform player; // герой
    public float hp = 100f; // HP
    public float strength = 0.5f; // def
    Vector2 enemyVec; // вектор направления движения
    Vector2 dash; // вектор направления рывка
    float angle; // угол поворота героя
    public int dist;
    Transform target;
    Transform spawnPos;
    bool goHome = false;
    private void Start()
    {
        spawnPos = this.transform;
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (goHome)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPos.position, speed * Time.deltaTime);
        }
        CheckDistance();
        if (hp <= 0)
            Destroy(enemy);
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

