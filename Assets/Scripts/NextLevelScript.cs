using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : MonoBehaviour
{
    private LevelLoader lvlload;
    // Start is called before the first frame update
    void Start()
    {
        lvlload = GetComponent<LevelLoader>();
        StartCoroutine(NextLevelCor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NextLevelCor()
    {       
        
        yield return new WaitForSeconds(20f);
        lvlload.LoadLevel(1);
    }
}
