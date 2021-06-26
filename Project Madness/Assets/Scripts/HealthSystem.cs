using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // ������� ��������� ��������� �����

    public int medicins = 5;
    public GameObject medPref;
    public GameObject cam;

    /* ��������� �������� � ���������� */
    bool possibleToBoost = true;
    float cooldownTime = 5; // ����� ������ ��������(� ��������)
    float extraHp = 20; // �������������� ���������� �������� ��� �����
    float boostTime = 1.75f; // ����� ��������(� �������)
    float debuffTime = 3f; // ����� ���������(� �������)

    float normalInputMultiplier = 1f; // ����������� ��������� ��������� �����
    float boostedInputMultiplier = 0.6f; // ��������� ��������� ����� ��� ��������
    float debuffedInputMultiplier = 1.2f; // ��������� ��������� ����� ��� ����������
    public AudioClip[] Punches;
    public AudioClip Death;
    public AudioSource damage;


    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4) && medicins > 0 && possibleToBoost)
		{
            StartCoroutine(Boost(boostTime * 60, debuffTime * 60));
            medicins--;
        }
	}

    public float getHP()
    {
        return health;
    }

	public bool TakeDamage(float dmg)
	{
        health -= dmg * inputDamageMultiplier; // ������ ��������� �����

        if (health <= 0)
        {
            Die();
            return true;
        }
        else
        {
                damage.clip = Punches[Random.Range(0, 5)];
                damage.Play();
        }

        return false;
	}

    IEnumerator Boost(float boost, float debuff)
	{
        possibleToBoost = false;
        PostProcessVolume[] vols = cam.GetComponents<PostProcessVolume>();
        MovementScript ms = gameObject.GetComponent<MovementScript>();

        vols[1].enabled = true;
        inputDamageMultiplier = boostedInputMultiplier;
        health += extraHp;
        ms.speed = ms.boostedSpeed;

        yield return new WaitForSeconds(boost);

        vols[1].enabled = false;
        inputDamageMultiplier = debuffedInputMultiplier;
        ms.speed = ms.slowedSpeed;

        yield return new WaitForSeconds(debuff);

        inputDamageMultiplier = normalInputMultiplier;
        ms.speed = ms.normalSpeed;
        possibleToBoost = true;
    }

    void Die()
	{
        if (gameObject.tag == "Player")
        {
            damage.clip = Death;
            damage.Play();
            /******************************************************
                ���� ����� ���������� �������� ������ ��� ������
            ******************************************************/
        }

        if (gameObject.tag == "Enemy")
        {
            if (Random.Range(0, 2) == 0)
                for (int i = 0; i < Random.Range(0, 2); i++)
                    Instantiate(medPref, gameObject.trasform.position);

            damage.clip = Death;
            damage.Play();
            /******************************************************
                            � ���� ��� ����������
                        ���ר� ������ ��� ������ �����
                  ���������� ������ �������� ����������� ���
            ******************************************************/
        }

        Destroy(gameObject);
    }
}
