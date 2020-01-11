using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    public Image healthBar;
    private PlayerCombat playcom;
    private PlayerStats stats;
    private GameObject player1;
    public Text HPtext;
    // Start is called before the first frame update
    void Start()
    {
        
        playcom = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
        healthBar.fillAmount = playcom.hp/stats.maxHP;
            HPtext.text = playcom.hp.ToString();
        if((playcom.hp*100/stats.maxHP)>60)
        {
            HPtext.color = Color.green;
        }
        if ((playcom.hp * 100 / stats.maxHP) > 20 && (playcom.hp * 100 / stats.maxHP) <= 60)
        {
            HPtext.color = Color.yellow;
        }
        if ((playcom.hp * 100 / stats.maxHP) <= 20)
        {
            HPtext.color = Color.red;
        }
            
    }
}
