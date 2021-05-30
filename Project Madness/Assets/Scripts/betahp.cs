using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class betahp : MonoBehaviour
{
    public Text txt;
    public GameObject player;
    public float HP;
    void Start()
    {
        txt.text = "HP: 100";
    }

    void Update()
    {
        HP = player.GetComponent<HealthSystem>().getHP();
        txt.text = "HP: " + HP;
    }
}
