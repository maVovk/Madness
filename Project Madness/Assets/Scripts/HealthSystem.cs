using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // текущий множитель входящего урона

    /* ПАРАМЕТРЫ УСИЛЕНИЯ И ОСЛАБЛЕНИЯ */
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
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
            StartCoroutine(Boost(boostTime * 60, debuffTime * 60));
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
            damage.clip = Death;
            damage.Play();
            /******************************************************
                СЮДА МОЖНО ПОНАТЫКАТЬ АНИМАЦИЙ СМЕРТИ ДЛЯ ИГРОКА
            ******************************************************/
        }

        if (gameObject.tag == "Enemy")
        {
            //Instantiate(healCapsPref, gameObject.trasform.position);

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
