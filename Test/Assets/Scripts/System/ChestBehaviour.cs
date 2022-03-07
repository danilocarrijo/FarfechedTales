using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    GameCenter gameCenter;

    [SerializeField]
    public List<Item> itemsToSpwan = new List<Item>();

    Vector3 randomPosition;
    public float xRange = 3f;
    // xRange the range in the x axis that the object can be placed
    public float zRange = 3f;

    public float yPos = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameCenter = GameCenter.Instance;

        foreach (var item in itemsToSpwan)
        {
            float xPosition = Random.Range(this.transform.position.x - xRange, this.transform.position.x + xRange);
            float zPosition = Random.Range(this.transform.position.z - zRange, this.transform.position.z + zRange);

            randomPosition = new Vector3(xPosition, this.transform.position.y + yPos, zPosition);

            gameCenter.SpawnObjectWorld(randomPosition, item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
