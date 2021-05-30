using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Camera mainCamera;
    
    public float speed = 6f; // �������� ������ ���������

    public float normalSpeed = 6f; // ������� ��������
    public float boostedSpeed = 7f; // �������� ��� �����
    public float slowedSpeed = 3.5f; // �������� ��� �������

    public Rigidbody2D rb; // ���������, ������� �������� �� ����������� ������

    Vector2 movement;
    Vector3 dash; // ������ ����������� ��������
    bool isDashCooldown = false;
    float dashCooldown = 5f; // ����� ������ �����

    public float dashMultiplier = 100f; // ��������� ���������� �����
    public float angle; // ���� �������� �����
    public AudioSource Step; // ��� ��������������� ������ �����

    public GameObject playerBody; // ������ �� �����
    public int gunAct = 0; // ������� ������
    public GameObject[] gunObjects; // ������ ������
    //private HashSet<GameObject> gunObj = new HashSet<GameObject>();
    public AudioSource step;
    public AudioClip[] steps;

    void Start()
    {
        //gunObjects[0] = (GameObject.FindGameObjectWithTag("gun"));
    }

    void Update()
    {
        // ���������� �� WASD � �������
        // ���������� ������, �� ������� ����� ������������
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
		// �������� ������
        /*rb.MovePosition(rb.position + movement * (speed + Mathf.Cos(Time.time * 5) / 2f) * Time.fixedDeltaTime);
        if (movement.x != 0 || movement.y != 0)
        {
	        if (!Step.isPlaying) // ���� ���� ���� ��� ����, ����� �� ��������
	        {
		        Step.clip = Steps[Random.Range(0, 7)]; // ����� ��������� ���� ����
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

        // ����� ������
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashCooldown)
        {
            isDashCooldown = true;
            rb.MovePosition(transform.position + dash * dashMultiplier); // ����� �������� �� ����������� �������� ������
            StartCoroutine(DashCooldown());
        }

        //playerBody.transform.position = rb.position;
		//����� ��� ����� ������� ��������, �� � �� ���� ��� ��� ��������

        // ����� ������
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            gunAct = 0;
		}

        // ������������ ������ ������ � �������
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