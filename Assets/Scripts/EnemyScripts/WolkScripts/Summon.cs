using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : StateMachineBehaviour
{
    [SerializeField] [Range(0, 10f)] private float waveDuration;
    [SerializeField] [Range(0, 10f)] private float pentaDuration;

    private Animator anim;
    private WolkCombat combat;
    private Transform[] points;


    private void OnEnable()
    {
        combat = GameObject.Find("Wolk").GetComponentInChildren<WolkCombat>();
        anim = GameObject.Find("Wolk").GetComponentInChildren<Animator>();
        points = combat.dawgPoints;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim.GetInteger("Stage") == 0)
            combat.StartCoroutine(FirstPattern());
        if (anim.GetInteger("Stage") == 2)
            combat.StartCoroutine(SecondPattern());
        if (anim.GetInteger("Stage") == 4)
            combat.StartCoroutine(PentaPattern());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    private void SpawnDawg(Transform from, Transform to, Vector2 offset)
    {
        Transform projectile = Prefabs.instance.projectileWolf;
        projectile.localScale = combat.transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = from;

        Quaternion q = new Quaternion();
        //distance = Vector2.Distance(target.position, transform.position);

        Vector2 dir = to.position - from.position;
        q.SetFromToRotation(Vector2.left, dir);

        projectile = Instantiate(projectile, (Vector2) from.position + offset, q);
        projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
    }

    private IEnumerator MakeWave(int from, int to, float howLong)
    {
        Vector2 offset;
        float timeStemp = Time.time + howLong;
        while (Time.time < timeStemp)
        {
            offset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            SpawnDawg(points[from], points[to], offset);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FirstPattern()
    {
        combat.StartCoroutine(MakeWave(7, 8, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(1, 5, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(0, 2, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(3, 4, waveDuration));
        yield return new WaitForSeconds(waveDuration + 2);
        combat.GetComponentInParent<DialogueControl>().TriggerDialogue("AfterFirst");
        anim.SetBool("IsCast", false);
        anim.SetInteger("Stage", 1);
    }

    private IEnumerator SecondPattern()
    {
        yield return new WaitForSeconds(2);
        combat.StartCoroutine(MakeWave(1, 6, waveDuration));
        combat.StartCoroutine(MakeWave(2, 5, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(0, 1, waveDuration));
        combat.StartCoroutine(MakeWave(0, 2, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(4, 3, waveDuration));
        combat.StartCoroutine(MakeWave(7, 8, waveDuration));
        yield return new WaitForSeconds(waveDuration);
        combat.StartCoroutine(MakeWave(0, 1, waveDuration));
        combat.StartCoroutine(MakeWave(1, 4, waveDuration));
        yield return new WaitForSeconds(waveDuration + 2);
        combat.GetComponentInParent<DialogueControl>().TriggerDialogue("AfterSecond");
        anim.SetBool("IsCast", false);
        anim.SetInteger("Stage", 3);
    }

    private IEnumerator PentaPattern()
    {
        yield return new WaitForSeconds(2);
        while (combat.hp > 0)
        {
            combat.StartCoroutine(MakeWave(4, 3, waveDuration));
            combat.StartCoroutine(MakeWave(3, 2, waveDuration));
            combat.StartCoroutine(MakeWave(2, 0, waveDuration));
            combat.StartCoroutine(MakeWave(0, 1, waveDuration));
            combat.StartCoroutine(MakeWave(1, 4, waveDuration));
            yield return new WaitForSeconds(pentaDuration + 1);
        }

        yield return null;
    }
}