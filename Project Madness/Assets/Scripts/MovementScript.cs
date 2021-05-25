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
    
    Vector2 movement; // ������ ����������� ��������
    Vector2 dash; // ������ ����������� �����
    
    public float dashMultiplier = 0.2f; // ��������� ���������� �����
    public float angle; // ���� �������� �����

    public GameObject playerBody; // ������ �� �����
    public int gunAct = 0; // ������� ������
    public GameObject[] gunObjects; // ������ ������
    //private HashSet<GameObject> gunObj = new HashSet<GameObject>();

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

        // ����� �������� �� ����������� ����
        // ���������� ������, �� ������� ����� ������ �����
        dash = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dash.x -= rb.position.x;
        dash.y -= rb.position.y;

    }

    void FixedUpdate()
    {
        // �������� ������
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // ����� ������
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.MovePosition(new Vector2(rb.position.x + dash.x * dashMultiplier, rb.position.y + dash.y * dashMultiplier));
        }

        //if (movement != new Vector2(0, 0))
        //    playerBody.transform.forward = Vector3.Normalize(new Vector3(movement.x, 0f, movement.y));
        playerBody.transform.position = rb.position;
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
}