using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationEventsHandler : AnimationEventsHandler
{
    public void LaunchEgg()
    {
        ChickenEnemy chickenEnemy = enemy as ChickenEnemy;
        chickenEnemy.EnableEggnade();
    }
}
