using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image healthBar;
    private EnemyStats enemystats;
    private EnemyAnimationControl eminem;
    public Text HPtext;
    private Canvas canvas;
    [SerializeField] private EnemyCombat enccom;
    // Start is called before the first frame update
    void Start()
    {
        eminem = GetComponentInParent<EnemyAnimationControl>();
        enemystats = GetComponentInParent<EnemyStats>();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.fillAmount = enccom.hp / enemystats.maxHP;
        HPtext.text = enccom.hp.ToString();
        
        
            Vector2 iceCube = transform.parent.lossyScale;
            
            canvas.transform.localScale = iceCube;
        
        
        
    }
}
