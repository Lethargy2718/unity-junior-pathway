using UnityEngine;

public abstract class Powerup<T> : ScriptableObject, IPowerup where T : Component
{
    public virtual string PowerupName => "Powerup";
    public virtual float Duration => 5f;

    protected T component;

    protected void AddComponent(GameObject target)
    {
        RemoveComponent(target);
        component = target.AddComponent<T>();
    }

    protected void RemoveComponent(GameObject target)
    {
        if (target != null && target.TryGetComponent<T>(out var component))
        {
            Destroy(component);
        }
    }

    protected void RemoveComponent()
    {
        if (component != null)
        {
            Destroy(component);
        }
    } 

    public void Apply(GameObject target)
    {
        AddComponent(target);
        OnApply(target);
    }

    public void Remove(GameObject target)
    {
        RemoveComponent(target);
        OnRemove(target);
    }

    protected virtual void OnApply(GameObject target) { }

    protected virtual void OnRemove(GameObject target) { }
}