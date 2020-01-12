using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Text HPtext;
    [SerializeField] private EnemyCombat combat;
    private EnemyStats stats;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStats>();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = combat.hp / stats.maxHP;
        HPtext.text = combat.hp.ToString();
        if (combat.hp < 0)
            HPtext.text = "0";

        Vector2 iceCube = transform.localScale;
        canvas.transform.localScale = iceCube;
    }
}
