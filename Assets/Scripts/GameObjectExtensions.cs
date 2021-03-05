using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetOrAddComponent<S,T>(this S self) where T : Component 
                                                        where S : Component
    {
        var component = self.GetComponent<T>();
        if (component != null)
        {
            return self.gameObject.AddComponent<T>();
        }
        return component;
    }
}
