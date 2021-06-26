using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class betahp : MonoBehaviour
{
    public Text txt;
    public GameObject player;
    public int num;
    
    void Start()
    {
        //txt.text = "HP: 100";
    }

    void Update()
    {
        if (num == 0)
        {
            float HP = player.GetComponent<HealthSystem>().getHP();
            txt.text = "HP: " + HP;
        }
        else if (num == 1)
        {
            float kokain = player.GetComponent<HealthSystem>().medicins;
            txt.text = "Морфий: " + kokain;
        }
    }
}
