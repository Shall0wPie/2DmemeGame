using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskvinCombat : EnemyCombat
{
    public Transform[] point;
    public Transform moskva;
    private DialogManager dial;
    private float distance;
    [Range(0f, 10f)] public float DelayTp;
    [Range(0f, 10f)] public float TpRange;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        dial = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DialogManager>();
        moskva = GameObject.FindGameObjectWithTag("Moskvin").GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((dial.isInDialogue == false))
        {
            int pos = Random.Range(0, 6);
            StartCoroutine(ITeleport(pos));
        }
    }

    public virtual IEnumerator ITeleport(int pos)
    {

        
        distance = Vector2.Distance(target.position, transform.position);
        yield return new WaitForSeconds(DelayTp);
        if(distance < TpRange)
        moskva.position = point[pos].position;


    }
}
