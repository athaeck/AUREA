using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private static StateController _instance;

    private bool walkable = true;

    private bool collided = false;

    public static StateController Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("StateController is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;

    }
    public void SetWalkable(bool b)
    {
        walkable = b;
    }
    public bool GetWalkability()
    {
        return walkable;
    }
    public bool GetCollided()
    {
        return collided;
    }
    public void SetCollided(bool b)
    {
        collided = b;
    }

}
