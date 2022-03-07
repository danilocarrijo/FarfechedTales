using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour, IEnemyStatus
{
    [SerializeField]
    public BaseClass infos;

    [SerializeField]
    public GameObject lifeBar;


    private Slider lifeSlider;

    public BaseClass GetInfos()
    {
        return infos;
    }

    private void Awake()
    {
        lifeSlider = lifeBar.GetComponent<Slider>();
        infos.cur_Heath = infos.max_Heath;
    }

    private void Update() 
    {
        lifeSlider.value = infos.cur_Heath / infos.max_Heath;
    }

    public void AbilityHit(Ability infos)
    {
        var resistence = this.infos.resistencePercentages.Where(x => x.dameType == infos.damegeTypes.damegeTypes).FirstOrDefault();
        var damage = Random.Range(infos.damegeTypes.min, infos.damegeTypes.max);
        if (resistence != null)
        {
            damage -= (resistence.percentage / 100) * damage;
        }
        this.infos.cur_Heath -= damage;
    }

    public void Hit(BaseClass infos, Item weaponItem)
    {
        var chanceToEvade = this.infos.evade + this.infos.agility + Random.Range(1, 20);
        var chancetoHit = infos.baseAttack + Random.Range(1, 20);
        if (chancetoHit > chanceToEvade)
        {
            var weaponDamage = 0.0f;
            if(weaponItem != null)
            {
                foreach (var item in weaponItem.weaponModifiers)
                {
                    var resistence = this.infos.resistencePercentages.Where(x => x.dameType == item.dameType).FirstOrDefault();
                    var damage = Random.Range(item.minType, item.maxType);
                    if (resistence != null)
                    {
                        damage -= (resistence.percentage / 100) * damage;
                    }
                    weaponDamage += damage;
                }
            }
            this.infos.cur_Heath -= weaponDamage - this.infos.armorClass < 0 ? 0 : weaponDamage - this.infos.armorClass; 
        }
    }
}

