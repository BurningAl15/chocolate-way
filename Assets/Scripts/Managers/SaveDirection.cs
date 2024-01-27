using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDirection : MonoSingleton<SaveDirection>
{
    [SerializeField] List<Vector3> directions = new List<Vector3>();

    public void AddDirection(Vector3 direction)
    {
        directions.Add(direction);
    }

    public Vector3 GetDirection()
    {
        Vector3 temp = directions[directions.Count - 1];
        directions.Remove(temp);
        return temp;
    }

    public Vector3 GetInitialDirection()
    {
        return directions[directions.Count - 1];
    }

    public int Size()
    {
        return directions.Count;
    }
}
