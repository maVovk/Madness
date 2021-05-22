using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;
    public float inputDamageMultiplier = 1; // ìíîæèòåëü âõîäÿùåãî óğîíà

    public void TakeDamage(int dmg)
	{
        health -= dmg * inputDamageMultiplier; // ğàñ÷åò âõîäÿùåãî óğîíà

        if (health <= 0) Die(); // ñêğèïò ñìåğòè
	}

    void Die()
	{
        if (gameObject.tag == "Player")
        {
            /******************************************************
                ÑŞÄÀ ÌÎÆÍÎ ÏÎÍÀÒÛÊÀÒÜ ÀÍÈÌÀÖÈÉ ÑÌÅĞÒÈ ÄËß ÈÃĞÎÊÀ
            ******************************************************/
        }

        if (gameObject.tag == "Enemy")
        {
            /******************************************************
                            À ÑŞÄÀ ÄËß ÏĞÎÒÈÂÍÈÊÀ
                        ÏĞÈ×¨Ì ĞÀÇÍÛÕ ÄËß ĞÀÇÍÈÕ ÒÈÏÎÂ
                  ÄÎÑÒÀÒÎ×ÍÎ ÏĞÎÑÒÎ ÏÎÌÅÍßÒÜ ÏĞÎÂÅĞßÅÌÛÉ ÒÅÃ
            ******************************************************/
        }

        // óäàëåíèå îáúåêòà ñî ñöåíû
        Destroy(gameObject);
	}
}
