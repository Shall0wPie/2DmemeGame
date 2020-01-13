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
        if (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
            lvlload.LoadLevel(1);
    }

    IEnumerator NextLevelCor()
    {
        
        yield return new WaitForSeconds(15f);
        lvlload.LoadLevel(1);
    }
}
