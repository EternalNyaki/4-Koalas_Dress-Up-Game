using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //Public read-only instance property
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //Set the instance
                _instance = FindAnyObjectByType<T>();
                _instance.Initialize();
            }
            return _instance;
        }
    }

    //Singleton instance of child class
    protected static T _instance;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            //Set the instance
            _instance = (T)this;
            Initialize();
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"Cannot have multiple {this.GetType().Name} objects in one scene.");
            Destroy(this);
        }
    }

    ///<summary>
    /// Initialization method (use instead of Start() or Awake())
    /// Called when the singleton instance is created
    ///</summary>
    protected virtual void Initialize()
    {

    }
}
