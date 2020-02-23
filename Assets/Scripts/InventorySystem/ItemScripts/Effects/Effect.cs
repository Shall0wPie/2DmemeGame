using UnityEngine;

public interface IEffect
{
    void ApplyEffect(Transform target, Item item);
}
public abstract class Effect : ScriptableObject, IEffect
{
    public abstract void ApplyEffect(Transform target, Item item);
}