using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Singleton class for MonoBehaviour derived classes.
/// Ensures that there is only one instance of the class and provides a global point of access to it.
/// </summary>
/// <typeparam name="T">The type of the singleton class.</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// The instance of the singleton class.
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Called when the script instance is being loaded. Ensures that only one instance of the singleton exists.
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Debug.LogWarning($"Another instance of {typeof(T)} already exists. Destroying the new instance.");
            Destroy(gameObject);
        }
    }
}