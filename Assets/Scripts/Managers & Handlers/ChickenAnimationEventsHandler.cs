using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationEventsHandler : AnimationEventsHandler
{
    public void LaunchEgg()
    {
        ChickenEnemy chickenEnemy = GetComponentInParent<ChickenEnemy>();
        chickenEnemy.EnableEggnade();
    }
}
