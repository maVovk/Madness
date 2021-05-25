using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // ������� ��������� ��������� �����

    /* ��������� �������� � ���������� */
    float extraHp = 20; // �������������� ���������� �������� ��� �����
    float boostTime = 5.0f; // ����� ��������
    float debuffTime = 10.0f; // ����� ���������

    float normalInputMultiplier = 1f; // ����������� ��������� ��������� �����
    float boostedInputMultiplier = 0.6f; // ��������� ��������� ����� ��� ��������
    float debuffedInputMultiplier = 1.2f; // ��������� ��������� ����� ��� ����������


    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
            StartCoroutine(Boost(boostTime, debuffTime));
		}
	}

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

    IEnumerator Boost(float boost, float debuff)
	{
        MovementScript ms = gameObject.GetComponent<MovementScript>();

        inputDamageMultiplier = boostedInputMultiplier;
        health += extraHp;
        ms.speed = ms.boostedSpeed;

        yield return new WaitForSeconds(boost);

        inputDamageMultiplier = debuffedInputMultiplier;
        ms.speed = ms.slowedSpeed;

        yield return new WaitForSeconds(debuff);

        inputDamageMultiplier = normalInputMultiplier;
        ms.speed = ms.normalSpeed;
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
