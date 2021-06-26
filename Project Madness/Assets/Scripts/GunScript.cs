using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //public Camera mainCamera;
    public int num;
    public float speed = 5f; // �������� ������
    public float superSpeed = 1f; // �������� ������ ��� ������� �����
    public float extraSpeed = 5f; // �������� ������ ��� ��������� �����
    public float damage = 20f; // �����
    public float superDamage = 50f; // ����� ��� ������� �����
    public Rigidbody2D rb; // ���������, ������� �������� �� ���������� ������ � ������� ���������
    public GameObject player; // �����
    public float strength = 20f; // ���������
    private HashSet<GameObject> afBodies = new HashSet<GameObject>();
    float time = 0f;

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
        if (player.GetComponent<MovementScript>().gunAct == 0)
        {
            if (Input.GetMouseButtonDown(0) && time >= System.Math.Round(1/speed, 1) && !Input.GetKey(KeyCode.Space))
            {
                //Debug.Log(afBodies.Count);
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

                    /*float ehp = go.GetComponent<EnemyScript>().hp;
                    ehp -= damage * go.GetComponent<EnemyScript>().defence;
                    go.GetComponent<EnemyScript>().hp = ehp;
                    prochnost -= ehp;*/
                }

                time = 0;

                /***********************************

                   �������� ������� ������ ������

                ************************************/
            }
            else if (Input.GetMouseButtonDown(1) && time >= System.Math.Round(1 / superSpeed, 1) && !Input.GetKey(KeyCode.Space))
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

               �������� ������� ������ ������

            *************************************/
            }
            else if (Input.GetKey(KeyCode.F) && time >= System.Math.Round(1 / extraSpeed, 1) && !Input.GetKey(KeyCode.Space))
            {
                foreach (GameObject go in afBodies)
                {
                    if (!go.GetComponent<HealthSystem>().TakeDamage(0))
                    {
                        go.GetComponent<EnemyScript>().freez = true;
                        go.GetComponent<EnemyScript>().freezTime = 4f;
                    }
                    else
                    {
                        afBodies.Remove(go);
                    }
                }

                time = 0;

                /************************************

                   �������� ��������� ������ ������

                *************************************/
            }
        }
    }
}
