using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f; // скорость оружия
    public float damage = 20f; // дамаг
    public float defence = 0.5f; // защита
    public Rigidbody2D rb; // ригидбоди, который отвечает за нахождение врагов в радиусе видимости
    public GameObject enemy;
    public GameObject player; // герой
    public float hp = 100f; // HP
    public float strenght = 0.5f; // def
    Vector2 enemyVec; // вектор направления движения
    Vector2 dash; // вектор направления рывка
    float angle; // угол поворота героя
    
    void Update()
    {
        if (hp <= 0)
            Destroy(enemy);
    }

    void FixedUpdate()
	{
        
	}
}

