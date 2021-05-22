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
    public float dashMultiplier = 0.2f; // множитель расстояния рывка
    public float angle; // угол поворота героя

    public GameObject playerBody; // спрайт на герое
    public int gunAct = 1;
    public GameObject[] gunObjects;
    private HashSet<GameObject> gunObj = new HashSet<GameObject>();

    void Start()
    {
        //gunObjects[0] = (GameObject.FindGameObjectWithTag("gun"));
    }

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
        rb.MovePosition(rb.position + movement * (speed + Mathf.Cos(Time.time * 5) / 2f) * Time.fixedDeltaTime);

        // рывок игрока
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.MovePosition(new Vector2(rb.position.x + dash.x * dashMultiplier, rb.position.y + dash.y * dashMultiplier));
        }

        //if (movement != new Vector2(0, 0))
        //    playerBody.transform.forward = Vector3.Normalize(new Vector3(movement.x, 0f, movement.y));
        playerBody.transform.position = rb.position;
        //здесь ещё нужно сменить анимацию, но я не знаю как это делается

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunAct = 1;
        }

        if (gunAct == 1)
        {
            gunObjects[0].transform.position = new Vector3(rb.transform.position.x + 0.25f, rb.transform.position.y + 0.2f, rb.transform.position.z);
        }
    }
}