using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Camera mainCamera;
    
    public float speed = 6f; // скорость нашего персонажа

    public float normalSpeed = 6f; // обычная скорость
    public float boostedSpeed = 7f; // скорость при бусте
    public float slowedSpeed = 3.5f; // скорость при дебаффе

    public Rigidbody2D rb; // ригидбоди, который отвечает за перемещение игрока

    Vector2 movement;
    Vector3 dash; // вектор направления движения
    bool isDashCooldown = false;
    float dashCooldown = 5f; // время отката рывка

    public float dashMultiplier = 100f; // множитель расстояния рывка
    public float angle; // угол поворота героя
    public AudioSource Step; // для воспроизведения звуков шагов

    public GameObject playerBody; // спрайт на герое
    public int gunAct = 0; // текущее оружие
    public GameObject[] gunObjects; // массив оружий
    //private HashSet<GameObject> gunObj = new HashSet<GameObject>();
    public AudioSource step;
    public AudioClip[] steps;

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
        float x = 0f, y = 0f;
		if (Input.GetAxis("Horizontal") > 0)
		{
            x = 1f;
		} else if(Input.GetAxis("Horizontal") < 0)
		{
            x = -1f;
		}

		if (Input.GetAxis("Vertical") > 0)
		{
            y = 1f;
		} else if(Input.GetAxis("Vertical") < 0)
		{
            y = -1f;
		}

        dash = new Vector3(x, y).normalized;
    }

    void FixedUpdate()
    {
		// движение игрока
        /*rb.MovePosition(rb.position + movement * (speed + Mathf.Cos(Time.time * 5) / 2f) * Time.fixedDeltaTime);
        if (movement.x != 0 || movement.y != 0)
        {
	        if (!Step.isPlaying) // если звук шага уже есть, новый не играется
	        {
		        Step.clip = Steps[Random.Range(0, 7)]; // берем рандомный звук шага
		        Step.Play();
	        }
        }
        else Step.Stop();*/
        
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        //rb.velocity = movement * speed;

        if(movement.x != 0 || movement.y != 0)
        {
            if (!step.isPlaying)
            {
                step.clip = steps[Random.Range(0, 7)];
                step.Play();
            }
        }
        else step.Stop();

        // рывок игрока
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashCooldown)
        {
            isDashCooldown = true;
            rb.MovePosition(transform.position + dash * dashMultiplier); // рывок делается по направлению движения игрока
            StartCoroutine(DashCooldown());
        }

        //playerBody.transform.position = rb.position;
		//здесь ещё нужно сменить анимацию, но я не знаю как это делается

        // смена оружия
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            gunAct = 0;
		}

        // передвижение оружия вместе с игроком
        MoveWeapon(gunAct);
    }

    void MoveWeapon(int index)
	{
        //Debug.Log(index);
        gunObjects[index].transform.position = new Vector3(rb.transform.position.x + 0.25f, rb.transform.position.y + 0.2f, rb.transform.position.z);
    }

    IEnumerator DashCooldown()
	{
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
	}
}