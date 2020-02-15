using UnityEngine;

public interface IItem
{
    string Name { get; }
    Sprite UIIcon { get; }
    int stackSize { get; }

}
