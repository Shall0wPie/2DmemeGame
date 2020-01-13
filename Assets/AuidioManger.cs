using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidioManger : MonoBehaviour
{
    public AudioClip main;
    public AudioClip moskvinTheme;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (SaveManager.instance.checkPoint == 3)
        {
            if (source.clip != moskvinTheme)
            {
                source.clip = moskvinTheme;
                source.Play();
            }
        }
    }
}
