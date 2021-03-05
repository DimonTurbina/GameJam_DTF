using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbstractBuilding
{
    public Transform TeleportationPoint;
    public override void Use(ZombieManager manager)
    {
        if(TeleportationPoint != null)
        {
            manager.TeleportateTo(TeleportationPoint);
        }
    }
}
