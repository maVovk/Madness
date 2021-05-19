using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f; // �������� ������ ���������
    public Rigidbody2D rb; // ���������, ������� �������� �� ����������� ������
    Vector2 movement; // ������ ����������� ��������
    Vector2 dash; // ������ ����������� �����

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
            rb.MovePosition(new Vector2(rb.position.x + dash.x * 0.1f, rb.position.y + dash.y * 0.1f));
        }
    }
}