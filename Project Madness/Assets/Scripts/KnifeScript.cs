using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    //public Camera mainCamera;
    //public int num;
    public float speed = 10f; // скорость ударов оружия
    public float superSpeed = 1f; // скорость оружия при сильном ударе
    public float extraSpeed = 0.1f; // скорость оружия при особенном ударе
    public float damage = 20f; // дамаг
    public float superDamage = 50f; // дамаг при сильном ударе
    //public Rigidbody2D rb; // ригидбоди, который отвечает за нахождение врагов в радиусе видимости
    public GameObject player; // герой
    public float strength = 20f; // прочность
    private HashSet<GameObject> afBodies = new HashSet<GameObject>();
    Animator anim;
    float time = 0f;

	private void Start()
	{
        anim = gameObject.GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject != null) && (other.gameObject.tag == "Enemy"))
            afBodies.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject != null) && (other.gameObject.tag == "Enemy"))
            afBodies.Remove(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && time >= System.Math.Round(1 / speed, 1) && !Input.GetKey(KeyCode.Space))// удар на ЛКМ
        {
            StartCoroutine(PlayAnimation());
            foreach (GameObject go in afBodies)
            {
                if (!go.GetComponent<HealthSystem>().TakeDamage(damage))
                {
                    go.GetComponent<Rigidbody2D>().AddForce((go.transform.position - transform.position) * 1000f);
                }
                else
                {
                    afBodies.Remove(go);
                }
            }
            
            time = 0;
        }
        else if (Input.GetMouseButtonDown(1) && time >= System.Math.Round(1 / superSpeed, 1) && !Input.GetKey(KeyCode.Space))// удар на ПКМ
        {
            foreach (GameObject go in afBodies)
            {
                if (!go.GetComponent<HealthSystem>().TakeDamage(superDamage))
                {
                    go.GetComponent<Rigidbody2D>().AddForce((go.transform.position - transform.position) * 3000f);
                }
                else
                {
                    afBodies.Remove(go);
                }
            }

            time = 0;

            /************************************

               АНИМАЦИИ СИЛЬНЫХ УДАРОВ ИГРОКА

            *************************************/
        }
        else if (Input.GetKey(KeyCode.F) && time >= System.Math.Round(1 / extraSpeed, 1) && !Input.GetKey(KeyCode.Space))// удар на F
        {
            foreach (GameObject go in afBodies)
            {
                for (int i = 0; i < Random.Range(0, 15); i++)
                {
                    if (!go.GetComponent<HealthSystem>().TakeDamage(Random.Range(0, 15)))
                    {
                        StartCoroutine(extraTime());
                    }
                    else
                    {
                        afBodies.Remove(go);
                        break;
                    }
                }
            }

            time = 0;

            /************************************

               АНИМАЦИИ ОСОБЕННЫХ УДАРОВ ИГРОКА

            *************************************/
        }
    }

    IEnumerator PlayAnimation()
	{
        anim.SetBool("punching", true);
        int current = Random.Range(1, 3);
        anim.SetInteger("animationNum", current);

        yield return new WaitForSeconds(0.2f);

        anim.SetBool("punching", false);
    }

    IEnumerator extraTime()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
