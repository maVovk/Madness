using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //public Camera mainCamera;
    public int num;
    //public float speed = 10f; // �������� ������
    public float damage = 20f; // �����
    public Rigidbody2D rb; // ���������, ������� �������� �� ���������� ������ � ������� ���������
    public GameObject player; // �����
    public float strength = 20f; // ���������
    private HashSet<GameObject> afBodies = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if ((other.gameObject != null) && (other.gameObject.tag != "Player"))
            afBodies.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject != null) && (other.gameObject.tag != "Player"))
            afBodies.Remove(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<MovementScript>().gunAct == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (GameObject go in afBodies)
                {
                    if (!go.GetComponent<HealthSystem>().TakeDamage(damage))
                    {
                        go.GetComponent<Rigidbody2D>().AddForce(transform.position - go.transform.position);
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
            }
        }
    }
}
