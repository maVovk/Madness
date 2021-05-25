using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // текущий множитель входящего урона

    /* ПАРАМЕТРЫ УСИЛЕНИЯ И ОСЛАБЛЕНИЯ */
    float extraHp = 20; // дополнительное количество здоровья при бусте
    float boostTime = 5.0f; // время усиления
    float debuffTime = 10.0f; // время отходняка

    float normalInputMultiplier = 1f; // стандартный множитель входящего урона
    float boostedInputMultiplier = 0.6f; // множитель входяшего урона при усилении
    float debuffedInputMultiplier = 1.2f; // множитель входяшего урона при ослаблении


    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
            StartCoroutine(Boost(boostTime, debuffTime));
		}
	}

	public bool TakeDamage(float dmg)
	{
        health -= dmg * inputDamageMultiplier; // расчет входящего урона

        if (health <= 0)
        {
            Die(); // скрипт смерти
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
                СЮДА МОЖНО ПОНАТЫКАТЬ АНИМАЦИЙ СМЕРТИ ДЛЯ ИГРОКА
            ******************************************************/
        }

        if (gameObject.tag == "Enemy")
        {
            /******************************************************
                            А СЮДА ДЛЯ ПРОТИВНИКА
                        ПРИЧЁМ РАЗНЫХ ДЛЯ РАЗНИХ ТИПОВ
                  ДОСТАТОЧНО ПРОСТО ПОМЕНЯТЬ ПРОВЕРЯЕМЫЙ ТЕГ
            ******************************************************/
        }

        // удаление объекта со сцены
        Destroy(gameObject);
	}
}
