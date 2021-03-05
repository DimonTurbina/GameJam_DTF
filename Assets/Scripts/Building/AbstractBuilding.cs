using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuilding : MonoBehaviour
{
    public abstract void Use(ZombieManager manager);
}
