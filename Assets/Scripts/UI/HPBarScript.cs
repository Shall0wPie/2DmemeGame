using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    public Image healthBar;
    public Text HPtext;
    private PlayerCombat combat;
    private PlayerStats stats;

    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerCombat>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {

        healthBar.fillAmount = combat.hp / stats.maxHP;
        HPtext.text = combat.hp.ToString();
        if (combat.hp < 0)
            HPtext.text = "0";
        if ((combat.hp * 100 / stats.maxHP) > 60)
        {
            HPtext.color = Color.green;
        }
        if ((combat.hp * 100 / stats.maxHP) > 20 && (combat.hp * 100 / stats.maxHP) <= 60)
        {
            HPtext.color = Color.yellow;
        }
        if ((combat.hp * 100 / stats.maxHP) <= 20)
        {
            HPtext.color = Color.red;
        }
    }
}
