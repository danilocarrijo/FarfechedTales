using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStatus 
{
    void Hit(BaseClass infos, Item weaponItem);
    void AbilityHit(Ability infos);
    BaseClass GetInfos();
}
