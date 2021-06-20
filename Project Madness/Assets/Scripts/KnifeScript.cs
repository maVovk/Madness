using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    //public Camera mainCamera;
    //public int num;
    public float speed = 10f; // �������� ������ ������
    public float damage = 20f; // �����
    //public Rigidbody2D rb; // ���������, ������� �������� �� ���������� ������ � ������� ���������
    public GameObject player; // �����
    public float strength = 20f; // ���������
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
        if (Input.GetMouseButtonDown(0) && time >= System.Math.Round(1 / speed, 1) && !Input.GetKey(KeyCode.Space))
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
    }

    IEnumerator PlayAnimation()
	{
        anim.SetBool("punching", true);
        int current = Random.Range(1, 3);
        anim.SetInteger("animationNum", current);

        yield return new WaitForSeconds(0.2f);

        anim.SetBool("punching", false);
    }
}
