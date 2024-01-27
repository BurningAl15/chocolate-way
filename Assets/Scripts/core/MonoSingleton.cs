using UnityEngine;

[DefaultExecutionOrder(-1)]
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Current { get; private set; }

    protected virtual void Awake()
    {
        if (Current != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Current = this as T;
    }
}