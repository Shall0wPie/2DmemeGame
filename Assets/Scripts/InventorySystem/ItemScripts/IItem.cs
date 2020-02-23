using UnityEngine;

public interface IItem
{
    string name { get; }
    Sprite uiIcon { get; }
    int stackSize { get; }

}
