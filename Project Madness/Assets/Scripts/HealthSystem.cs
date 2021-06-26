using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // текущий множитель входящего урона

    public int medicins = 5;
    public GameObject medPref;
    public GameObject cam;

    /* ПАРАМЕТРЫ УСИЛЕНИЯ И ОСЛАБЛЕНИЯ */
    bool possibleToBoost = true;
    float cooldownTime = 5; // время отката усидения(в секундах)
    float extraHp = 20; // дополнительное количество здоровья при бусте
    float boostTime = 1.75f; // время усиления(в минутах)
    float debuffTime = 3f; // время отходняка(в минутах)

    float normalInputMultiplier = 1f; // стандартный множитель входящего урона
    float boostedInputMultiplier = 0.6f; // множитель входяшего урона при усилении
    float debuffedInputMultiplier = 1.2f; // множитель входяшего урона при ослаблении
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
        health -= dmg * inputDamageMultiplier; // расчет входящего урона

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
                СЮДА МОЖНО ПОНАТЫКАТЬ АНИМАЦИЙ СМЕРТИ ДЛЯ ИГРОКА
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
                            А СЮДА ДЛЯ ПРОТИВНИКА
                        ПРИЧЁМ РАЗНЫХ ДЛЯ РАЗНИХ ТИПОВ
                  ДОСТАТОЧНО ПРОСТО ПОМЕНЯТЬ ПРОВЕРЯЕМЫЙ ТЕГ
            ******************************************************/
        }

        Destroy(gameObject);
    }
}
