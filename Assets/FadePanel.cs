using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    private EnemyStats moskvinStats;
    private Image image;
    private bool isRunning = false;

    private void Start()
    {
        moskvinStats = FindObjectOfType<MoskvinCombat>().GetComponentInParent<EnemyStats>();
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (!moskvinStats.isAlive && !isRunning)
        {
            StartCoroutine(FadeOut());
            isRunning = true;
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(5f);
        Color color = new Color();
        float volume = AuidioManger.instance.GetComponent<AudioSource>().volume;
        for (float i = 0; i <= 1; i += 0.01f)
        {
            AuidioManger.instance.GetComponent<AudioSource>().volume -= 0.01f*volume;
            yield return new WaitForSeconds(0.0333f);
            color = image.color;
            color.a = i;
            image.color = color;
        }
    }
}
