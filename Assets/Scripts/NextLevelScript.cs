using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : MonoBehaviour
{
    public LevelLoader lvlload;
    // Start is called before the first frame update
    void Start()
    {
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
