using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titles : MonoBehaviour
{
    // Start is called before the first frame update
     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        SceneManager.LoadScene(1);
    }

   
}

