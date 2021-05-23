using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // ��������� ��������� �����

    public bool TakeDamage(float dmg)
	{
        health -= dmg * inputDamageMultiplier; // ������ ��������� �����

        if (health <= 0)
        {
            Die(); // ������ ������
            return true;
        }

        return false;
	}

    void Die()
	{
        if (gameObject.tag == "Player")
        {
            /******************************************************
                ���� ����� ���������� �������� ������ ��� ������
            ******************************************************/
        }

        if (gameObject.tag == "Enemy")
        {
            /******************************************************
                            � ���� ��� ����������
                        ���ר� ������ ��� ������ �����
                  ���������� ������ �������� ����������� ���
            ******************************************************/
        }

        // �������� ������� �� �����
        Destroy(gameObject);
	}
}
