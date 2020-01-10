using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 2)]
public class Dialogue : ScriptableObject
{
    public string dialogueName;
    public string actorName;
    [TextArea(3, 10)]
    public string[] sentence;
}
