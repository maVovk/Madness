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
    public float blockedSpeed = 4f; // скорость при блокировке

    public Rigidbody2D rb; // ригидбоди, который отвечает за перемещение игрока

    Vector2 movement;
    Vector3 dash; // вектор направления движения
    bool isDashCooldown = false;
    float dashCooldown = 5f; // время отката рывка

    public float dashMultiplier = 100f; // множитель расстояния рывка
    public float angle; // угол поворота героя
    public AudioSource Step; // для воспроизведения звуков шагов

    public GameObject playerBody; // спрайт на герое
    public int gunAct = 2; // текущее оружие
    public GameObject[] gunObjects; // массив оружий
    //private HashSet<GameObject> gunObj = new HashSet<GameObject>();
    public AudioSource step;
    public AudioClip[] steps;

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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

        // изменения спрайта в зависимости от направления ходьбы
        if(x == 1f)
		{
            if (y == 1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (y == -1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -135);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
		}
        else if (x == -1f)
        {
            if (y == 1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (y == -1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
		else
		{
            if (y == 1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (y == -1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                
            }
        }

        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        dash = new Vector3(x, y).normalized;
    }

    void FixedUpdate()
    {
		// движение игрока
        
        if (!Input.GetKey(KeyCode.Space))
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * blockedSpeed * Time.fixedDeltaTime);
        //rb.velocity = movement * speed;

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("Walking", true);
            if (!step.isPlaying)
            {
                step.clip = steps[Random.Range(0, 7)];
                step.Play();
            }
        }
        else
        {
            anim.SetBool("Walking", false);
            step.Stop();
        }

        // рывок игрока
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashCooldown)
        {
            isDashCooldown = true;
            rb.MovePosition(transform.position + dash * dashMultiplier); // рывок делается по направлению движения игрока
            StartCoroutine(DashCooldown());
        }

        // смена оружия
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gunAct = 0;
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha1)) && (gunAct != 1))
        {
            gunAct = 1;
            Instantiate(gunObjects[1]).transform.SetParent(gameObject.transform);
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha2)) && (gunAct != 2))
        {
            gunAct = 2;
            Instantiate(gunObjects[2]).transform.SetParent(gameObject.transform);
        }
    }

    IEnumerator DashCooldown()
	{
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
	}
}