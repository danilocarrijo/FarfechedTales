using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Moviment : MonoBehaviour
{
    [Header("Stats")]
    public float attackDistance;
    public float attackRate;

    private float nextAttack;

    [HideInInspector]
    public Transform clickedObject;
    [HideInInspector]
    public Transform itemObject;
    [HideInInspector]
    public bool itemClicked;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    private Animator animator; 

    private Transform targetEnenmy;
    private PlayerBehavor playerBehavor;

    [HideInInspector]
    public bool walking;

    private bool oneClick;
    private bool doubleClick;

    [SerializeField]
    public Item weaponItem;

    [SerializeField]
    public GameObject swordSocket;


    private GameObject currentWeapon;
    private bool isMouseButtonDown = false;

    private CharacterAnimationController characterAnimationController;

    private DragHandler canvas;

    private bool canMove = true;
    private bool canAttack = true;

    //----------------------------Teste----------------------------
    public Ability testeAbility;
    private float timerDoubleClick;
    private float delay = 0.25f;


    void CheckDoublClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!oneClick)
            {
                oneClick = true;
                timerDoubleClick = Time.time;

            }
            else
            {
                oneClick = false;
                doubleClick = true;
            }
        }

        if (oneClick)
        {
            if (Time.time - timerDoubleClick > delay)
            {
                oneClick = false;
                doubleClick = false;
            }
        }
    }
    //----------------------------Teste----------------------------


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();

        playerBehavor = transform.parent.GetComponent<PlayerBehavor>();

        characterAnimationController = transform.GetComponentInChildren<CharacterAnimationController>();

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<DragHandler>();

        

    }

    public IEnumerator LockForTime(float seconds)
    {
        StartCoroutine(Lock());

        yield return new WaitForSeconds(seconds);

        StartCoroutine(UnLock());
    }
    public IEnumerator Lock()
    {
        canMove = false;
        canAttack = false;

        yield return true;
    }
    public IEnumerator UnLock()
    {
        canMove = true;
        canAttack = true;

        yield return true;
    }

    public void UnequilWeapon()
    {
        this.weaponItem = null;
        StartCoroutine(characterAnimationController.StashUnstash(WeaponTypeEnum.UNARMED));
    }

    public void WeaponSwitch()
    {
        GameObject.Destroy(currentWeapon);
        if(weaponItem != null)
        {
            currentWeapon = GameObject.Instantiate(weaponItem.itemPrefab);
            currentWeapon.SetActive(true);
            currentWeapon.transform.SetParent(swordSocket.transform);
            currentWeapon.transform.localScale = Vector3.one;
            currentWeapon.transform.position = swordSocket.transform.position;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }

    }

    public void EquipWeapon(Item weaponItem)
    {
        this.weaponItem = weaponItem;
        StartCoroutine(characterAnimationController.StashUnstash(weaponItem.weapon));
        /*GameObject.Destroy(currentWeapon);

        this.weaponItem = weaponItem;

        currentWeapon = GameObject.Instantiate(weaponItem.itemPrefab);
        currentWeapon.SetActive(false);
        currentWeapon.transform.SetParent(swordSocket.transform);
        currentWeapon.transform.localScale = Vector3.one;
        currentWeapon.transform.position = swordSocket.transform.position;
        currentWeapon.transform.localRotation = Quaternion.identity;
        if (weaponItem.weapon == Weapon.ARMED)
        {
            switch (weaponItem.WeaponType)
            {
                case WeaponSubType.SWORD:
                    break;
            }
        }*/
    }


    internal void Hit()
    {
        if (targetEnenmy != null)
        {
            targetEnenmy.gameObject.GetComponent<IEnemyStatus>().Hit(playerBehavor.infos,weaponItem);
        }
    }

    public void StashUnstash()
    {
        StartCoroutine(characterAnimationController.StashUnstash(WeaponTypeEnum.SWORD));
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        CheckDoublClick();

        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
        }

        if (Input.GetMouseButtonUp (0))
        {
            isMouseButtonDown = false;
        }



        if (isMouseButtonDown && !EventSystem.current.IsPointerOverGameObject() && canvas.currDragSlot == null)
        {
            navMeshAgent.ResetPath();
            itemObject = null;
            targetEnenmy = null;
            clickedObject = null;
            if (Physics.Raycast(ray,out raycastHit, 1000))
            {
                if(raycastHit.collider.tag == "Enemy" && canAttack)
                {
                    targetEnenmy = raycastHit.transform;
                }
                else if (raycastHit.collider.tag == "Item")
                {
                    itemObject = raycastHit.transform.parent;
                }
                else if (raycastHit.collider.tag == "Search")
                {
                    clickedObject = raycastHit.transform;
                }
                else if(canMove)
                {
                    walking = true;
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = raycastHit.point;
                }
            }
        }

        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }

        if (targetEnenmy != null)
        {
            StartCoroutine(MoveAndAttack());
        }
        else if (itemObject != null)
        {
            StartCoroutine(PickupItem());
        }
        else if (clickedObject != null)
        {
            StartCoroutine(ReadInfo());
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 0.5f)
            {
                walking = false;
            }
            else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance)
            {
                walking = true; 
            }
        }

        

    }

    public IEnumerator PickupItem()
    {
        navMeshAgent.destination = itemObject.position;
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > 0.5f)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 0.5f)
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(itemObject);
            walking = false;

            var comp = itemObject.gameObject.GetComponentInChildren<Animator>();

            if (comp != null)
            {
                comp.SetTrigger("Play");
            }

            try
            {
                playerBehavor.AddItem(itemObject.GetComponent<ItemBehaviour>().item);
                GameObject.Destroy(itemObject.gameObject);
                itemObject = null;
            }
            catch (System.Exception)
            {            }

            navMeshAgent.ResetPath();
        }

        yield return true;
    }

    public IEnumerator ReadInfo()
    {
        navMeshAgent.destination = clickedObject.position;
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance > 0.5f)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 0.5f)
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(clickedObject);
            walking = false;

            var comp = clickedObject.gameObject.GetComponentInChildren<Animator>();

            if(comp != null)
            {
                comp.SetTrigger("Play");
            }

            navMeshAgent.ResetPath();
        }
        yield return true;
    }
    public IEnumerator MoveAndAttack()
    {

        if(targetEnenmy == null)
        {
            yield return true;
        }

        navMeshAgent.destination = targetEnenmy.position;

        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance >= attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackDistance)
        {
            //animator.SetBool("isAttacking", false);
            //transform.LookAt(targetEnenmy);
            Vector3 dirToAttack = targetEnenmy.transform.position - transform.position;

            Vector3 rot = Quaternion.LookRotation(dirToAttack).eulerAngles;
            rot.x = rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
            animator.SetBool("Moving", false);
            walking = false;

            if (Time.time> nextAttack)
            {
                nextAttack = Time.time + attackRate;
                StartCoroutine(characterAnimationController.Attack());
            }
            navMeshAgent.isStopped = true;
        }

        yield return true;
    }




}
