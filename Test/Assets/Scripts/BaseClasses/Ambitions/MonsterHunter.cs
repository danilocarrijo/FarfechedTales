using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterHunter : Ambition
{
    public int normalMonsterKill = 0;
    public int rareMonsterKill = 0;
    public int legendaryMonsterKill = 0;

    public override void Set(GameObject target)
    {
        if (target.tag.Equals("Enemy"))
        {
            var status = target.gameObject.GetComponent<IEnemyStatus>();
            if(status!= null)
            {
                var rarity = status.GetInfos().enemyRarity;
                switch (rarity)
                {
                    case ItemRarity.COMMON:
                        normalMonsterKill++;
                        break;
                    case ItemRarity.UNCOMMON:
                        normalMonsterKill++;
                        break;
                    case ItemRarity.RARE:
                        rareMonsterKill++;
                        break;
                    case ItemRarity.LEGANDARY:
                        legendaryMonsterKill++;
                        break;
                }
                if(normalMonsterKill >= 10)
                {
                    var description = tasks[1];
                    tasks.Remove(1);
                    completedTasks.Add(1, description);
                }

                if (normalMonsterKill >= 100)
                {
                    var description = tasks[2];
                    tasks.Remove(2);
                    completedTasks.Add(2, description);
                }

                if (normalMonsterKill >= 1000)
                {
                    var description = tasks[3];
                    tasks.Remove(3);
                    completedTasks.Add(3, description);
                }

                if (normalMonsterKill >= 10000)
                {
                    var description = tasks[4];
                    tasks.Remove(4);
                    completedTasks.Add(4, description);
                }
                //------------------------rare----------------------
                if (rareMonsterKill >= 10)
                {
                    var description = tasks[5];
                    tasks.Remove(5);
                    completedTasks.Add(5, description);
                }

                if (rareMonsterKill >= 100)
                {
                    var description = tasks[6];
                    tasks.Remove(6);
                    completedTasks.Add(6, description);
                }

                if (rareMonsterKill >= 1000)
                {
                    var description = tasks[7];
                    tasks.Remove(7);
                    completedTasks.Add(7, description);
                }

                if (rareMonsterKill >= 10000)
                {
                    var description = tasks[8];
                    tasks.Remove(8);
                    completedTasks.Add(8, description);
                }
                //------------------------legendary----------------------
                if (legendaryMonsterKill >= 10)
                {
                    var description = tasks[9];
                    tasks.Remove(9);
                    completedTasks.Add(9, description);
                }

                if (legendaryMonsterKill >= 100)
                {
                    var description = tasks[10];
                    tasks.Remove(10);
                    completedTasks.Add(10, description);
                }

                if (legendaryMonsterKill >= 1000)
                {
                    var description = tasks[11];
                    tasks.Remove(11);
                    completedTasks.Add(11, description);
                }

                if (legendaryMonsterKill >= 10000)
                {
                    var description = tasks[12];
                    tasks.Remove(12);
                    completedTasks.Add(12, description);
                }
            }
        }
    }
    public MonsterHunter()
    {
        name = "Monster Hunter";
        type = AmbitionType.MONSTER_KILL;
        tasks.Add(1,"Kill 10 monsters");
        tasks.Add(2,"Kill 100 monsters");
        tasks.Add(3,"Kill 1000 monsters");
        tasks.Add(4,"Kill 10000 monsters");
        tasks.Add(5,"Kill 10 rare monsters");
        tasks.Add(6,"Kill 100 rare monsters");
        tasks.Add(7,"Kill 1000 rare monsters");
        tasks.Add(8,"Kill 10000 rare monsters");
        tasks.Add(9,"Kill 10 legendary monsters");
        tasks.Add(10,"Kill 100 legendary monsters");
        tasks.Add(11,"Kill 1000 legendary monsters");
        tasks.Add(12,"Kill 10000 legendary monsters");
    }
}
