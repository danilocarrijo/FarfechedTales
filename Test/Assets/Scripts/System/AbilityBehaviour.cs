using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Ability ability;
    [HideInInspector]
    public float destroyTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(destroyTime == 0)
            GameObject.Destroy(this.gameObject, ability.destroyTime);
        else
            GameObject.Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<IEnemyStatus>().AbilityHit(ability);
        }
    }
}
