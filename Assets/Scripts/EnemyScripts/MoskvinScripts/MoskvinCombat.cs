using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoskvinCombat : EnemyCombat
{
    public Transform[] point;
    private float distance;
    [SerializeField] [Range(0f, 10f)] private float DelayTp;
    [SerializeField] [Range(0f, 10f)] private float TpRange;
    [SerializeField] [Range(0f, 100f)] public float tamponRange = 1f;
    [SerializeField] protected float tamponCooldown = 1f;

    [SerializeField] private GameObject door;

    private bool isInRange = false;
    private int prevPoint = -1;
    private float tamponTimeStamp = 0;
    private int phase = 1;
    private bool firstPhasePassed = false;
    private bool secondPhasePassed = false;


    protected override void Update()
    {
        distance = Vector2.Distance(target.position, transform.position);
        if (distance < tamponRange && SaveManager.instance.checkPoint != 3)
        {
            GetComponentInParent<DialogueControl>().TriggerDialogue("MoskvinEngage");
            SaveManager.instance.SavePosition(3);
        }
        if (SaveManager.instance.checkPoint == 3)
        {
            door.SetActive(true);
        }

        if (phase == 1)
        {
            FirstPhase();
        }

        if (phase == 2)
        {
            GetComponentInParent<DialogueControl>().TriggerDialogue("Nahrena");
            phase = 3;
        }

        if (phase == 3)
        {
            SecondPhase();
        }

        if (phase == 4)
        {
            GetComponentInParent<DialogueControl>().TriggerDialogue("Butilka");
            phase = 0;
        }
    }

    void FirstPhase()
    {
        if (DialogManager.instance.isInDialogue == false)
        {
            
            if (distance < TpRange && !isInRange)
            {
                int pos = Random.Range(0, 7);

                while (pos == prevPoint)
                {
                    pos = Random.Range(0, 7);
                }
                prevPoint = pos;
                StartCoroutine(ITeleport(pos));
            }

            if (tamponTimeStamp <= Time.time && distance < tamponRange)
            {
                animControl.PlayShoot();
                StartCoroutine(ShootTampon());
                tamponTimeStamp = Time.time + tamponCooldown;
            }
            // Sec phase transition
            if (hp / stats.maxHP < 0.3f)
            {
                phase = 2;
                StopAllCoroutines();
                transform.parent.position = point[1].position;
            }
        }
    }

    void SecondPhase()
    {
        if (tamponTimeStamp <= Time.time && DialogManager.instance.isInDialogue == false)
        {
            animControl.PlayShoot();
            StartCoroutine(ShootTamponPhase());
            tamponTimeStamp = Time.time + tamponCooldown;

        }
    }

    private IEnumerator ITeleport(int pos)
    {
        isInRange = true;
        yield return new WaitForSeconds(DelayTp);

        transform.parent.position = point[pos].position;
        isInRange = false;
    }

    public IEnumerator ShootTampon()
    {
        Transform projectile = Prefabs.instance.projectileTampon;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;

        Quaternion q = new Quaternion();


        while (true)
        {
            //force for enemy punch
            yield return null;
            distance = Vector2.Distance(target.position, transform.position);
            if (animControl.renderer.sprite.name.Equals("throw"))
            {
                Vector2 dir = target.position - transform.position;
                q.SetFromToRotation(Vector2.up, dir);
                projectile = Instantiate(projectile, transform.position, q);
                projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
                break;
            }
            if (animControl.anim.GetCurrentAnimatorStateInfo(0).IsName("afk"))
                break;
        }
    }

    public IEnumerator ShootTamponPhase()
    {
        Transform projectile = Prefabs.instance.projectileTampon;
        projectile.localScale = transform.lossyScale;
        projectile.GetComponent<Projectile>().caster = transform;

        Quaternion q = new Quaternion();

        for (float i = 3.14f; i > -3.14f; i -= 0.5f)
        {
            if (projectile == null)
                break;
            Vector2 dir = new Vector2(Mathf.Sin(i), Mathf.Cos(i));
            q.SetFromToRotation(Vector2.up, dir);
            projectile = Instantiate(projectile, transform.position, q);
            projectile.GetComponent<Projectile>().SetVelocityDirection(dir);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator Kill()
    {
        phase = 4;
        // Plays anim
        stats.isAlive = false;

        yield return new WaitForSeconds(2f);
        animControl.PlayDeath();

        LootTable lootTable = GetComponent<LootTable>();
        if (lootTable != null)
        {
            lootTable.SpawnLoot();
        }
        GetComponentInParent<EnemyControl>().enabled = false;

        // The rest of function will continue as deathDuration passes
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Respawns Enemy in its Spawn Point
        if (stats.allowRespawn)
            Respawn();
        // Destroys parent object (the entire Enemy object)
        else
            Destroy(transform.parent.gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, TpRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tamponRange);
    }
}
