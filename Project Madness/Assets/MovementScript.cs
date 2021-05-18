using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f; // скорость нашего персонажа
    public Rigidbody2D rb; // ригидбоди, который отвечает за перемещение игрока
    Vector2 movement; // вектор направления движения
    Vector2 dash; // вектор направления рывка

    void Update()
    {
        // управление на WASD и стрелки
        // определяем вектор, на который будем перемещаться
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // рывок делается по направлению мыши
        // определяем вектор, на который будем делать рывок
        dash = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dash.x -= rb.position.x;
        dash.y -= rb.position.y;
    }

    void FixedUpdate()
    {
        // движение игрока
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // рывок игрока
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.MovePosition(new Vector2(rb.position.x + dash.x * 0.1f, rb.position.y + dash.y * 0.1f));
        }
    }
}