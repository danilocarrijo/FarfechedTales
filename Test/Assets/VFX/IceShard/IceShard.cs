using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{

    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public float delay;
    [SerializeField]
    public float velocity;
    [SerializeField]
    public float minDistance;
    [SerializeField]
    public float maxDistance;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cast());
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += this.transform.forward * Time.deltaTime * velocity;
        Vector3 tempVect = transform.forward;
        tempVect = tempVect.normalized * velocity * Time.deltaTime;
        rigidbody.MovePosition(transform.position + tempVect);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if(!other.tag.Equals("Player") && !other.tag.Equals("Enemy"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
    private IEnumerator Cast()
    {
        while (true)
        {
            var obj = GameObject.Instantiate(prefab);
            //obj.transform.SetParent(this.transform);
            obj.transform.localScale = Vector3.one;
            float xPosition = Random.Range(this.transform.position.x - maxDistance, this.transform.position.x + maxDistance);
            float zPosition = Random.Range(this.transform.position.z - maxDistance, this.transform.position.z + maxDistance);

            var randomPosition = new Vector3(xPosition, this.transform.position.y + 0.5f, zPosition);

            obj.transform.position = randomPosition;
            obj.transform.localRotation = Quaternion.identity;
            obj.AddComponent<AbilityBehaviour>();
            obj.GetComponent<AbilityBehaviour>().ability = this.GetComponent<AbilityBehaviour>().ability;
            obj.GetComponent<AbilityBehaviour>().destroyTime = 5;


            yield return new WaitForSeconds(delay);
        }
    }
}
