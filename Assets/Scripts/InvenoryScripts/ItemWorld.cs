using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public Item Item;


    private static Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public Item GetItem()
    {
        return Item; 
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static void DropItem(Item item)
    {
        var objectFromPrefab = Instantiate(item.GetWorldPrefab().transform);

        Vector3 forceVector = new Vector3(4, 4, 4);
        objectFromPrefab.GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);

        Vector3 spawnPosition = 2 * player.transform.forward + player.position;
        objectFromPrefab.position = spawnPosition;
    }
}
