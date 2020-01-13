using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidioManger : MonoBehaviour
{
    public static AuidioManger instance;

    private void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public AudioClip main;
    public AudioClip moskvinTheme;
    public AudioClip failSound;

    private Transform player;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;


        if (SaveManager.instance.checkPoint == 3)
        {
            if (source.clip != moskvinTheme)
            {
                source.clip = moskvinTheme;
                source.Play();
            }
        }
        else if (player.GetComponent<PlayerStats>().isAlive)
        {
            if (source.clip != main)
            {
                source.clip = main;
                source.Play();
            }
        }
        else
        {
            if (source.clip != failSound)
            {
                source.clip = failSound;
                source.Play();
            }
        }
    }
}
