using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    
    public string unsheathWeaponStringTreeName;
    public string weaponStringTreeName;
    public string swaordAttackAnimPropName;
    public string unarmedAttackAnimPropName;
    public string unsheathTriggerName;
    public string attackTriggerName;
    public float[] swordAnims;
    public float[] unarmedAnims;
    [SerializeField]
    public List<UnstashEvent> unstashEvent;
    [HideInInspector]
    public Animator animator;
    private WeaponTypeEnum currWeapom = WeaponTypeEnum.UNARMED;
    private Moviment moviment;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moviment = GetComponent<Moviment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moviment.walking)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                moviment.navMeshAgent.speed = 7f;
            else
                moviment.navMeshAgent.speed = 3f;

            this.GetComponentInChildren<Animator>().SetBool("IsIdling", false);
            this.GetComponentInChildren<Animator>().SetBool("isWalking", moviment.walking);
            animator.SetBool("isRunning", true);
            animator.SetFloat("velocity", moviment.navMeshAgent.speed);
        }
        else
        {
            this.GetComponentInChildren<Animator>().SetBool("IsIdling", true);
            this.GetComponentInChildren<Animator>().SetBool("isWalking", moviment.walking);
            moviment.navMeshAgent.speed = 0f;
            animator.SetFloat("velocity", 0f);
            animator.SetBool("isRunning", false);
        }
    }

    public IEnumerator StashUnstash(WeaponTypeEnum weaponType)
    {
        var waitTime = 1f;
        if (currWeapom == WeaponTypeEnum.UNARMED)
            StartCoroutine(UnstashAnim(weaponType, waitTime));
        else
            StartCoroutine(StashAnim(weaponType, waitTime));
        yield return null;
    }

    public IEnumerator Attack()
    {
        switch (currWeapom)
        {
            case WeaponTypeEnum.UNARMED:
                animator.SetFloat(swaordAttackAnimPropName, unarmedAnims[Random.Range(0, unarmedAnims.Length )]);
                break;
            case WeaponTypeEnum.SWORD:
                animator.SetFloat(unarmedAttackAnimPropName, swordAnims[Random.Range(0, swordAnims.Length )]);
                break;
            default:
                break;
        }
        animator.SetTrigger(attackTriggerName);
        yield return null;
    }
    internal void FootR()
    {
    }
    internal void FootL()
    {
    }
    internal void WeaponSwitch()
    {
        moviment.WeaponSwitch();
    }

    

    private IEnumerator UnstashAnim(WeaponTypeEnum weaponType,float waitTime)
    {
        var anim = unstashEvent.Where(x => x.Weapon == weaponType).FirstOrDefault();
        animator.SetFloat(unsheathWeaponStringTreeName, anim.anim);
        animator.SetTrigger(unsheathTriggerName);
        StartCoroutine(moviment.LockForTime(waitTime));

        yield return new WaitForSeconds(waitTime);

        animator.SetFloat(weaponStringTreeName, (int)weaponType);
        currWeapom = WeaponTypeEnum.SWORD;
    }

    private IEnumerator StashAnim(WeaponTypeEnum weaponType, float waitTime)
    {
        var anim = unstashEvent.Where(x => x.Weapon == currWeapom).FirstOrDefault();
        animator.SetFloat(unsheathWeaponStringTreeName, anim.anim);
        animator.SetTrigger(unsheathTriggerName);
        StartCoroutine(moviment.LockForTime(waitTime));

        yield return new WaitForSeconds(waitTime);

        animator.SetFloat(weaponStringTreeName, 0);
        currWeapom = WeaponTypeEnum.UNARMED;
    }
}

