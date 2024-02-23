using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] PickUp PickUpPrefab;
    List<PickUp> PickUps = new List<PickUp>();
    [SerializeField] int numberToSpawn;
    [SerializeField] float proximity;
    [SerializeField] LayerMask spherCastLayers;

    public bool SpawnComplete => PickUps.Count >= numberToSpawn;

    /*private async void OnEnable()
    {
        await SpawnPickUpsTask();
    }*/

    private void Update()
    {
        /*if (PickUps.Count < numberToSpawn)
        {
            SpawnPickup();
        }*/
    }


    public void SpawnPickUps()
    {
        for (int pickUpIndex = 0;  pickUpIndex < numberToSpawn; pickUpIndex++)
        {
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        Vector3 position = nextPosition;
        //if (InProximityToOthers(position)) return;

        if (Physics.SphereCastAll(position, proximity, Vector3.up, proximity, spherCastLayers).Length == 0)
        {
            PickUp pickUp = Instantiate(PickUpPrefab, position, Quaternion.identity);
            PickUps.Add(pickUp);
            pickUp.Spawner(this);
            pickUp.transform.parent = transform;
        }
    }

    private Vector3 nextPosition
    {
        get
        {
            
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float height = 0;
            Vector3 position = new Vector3(x, height, y);
            return position;
        }
    }

    private bool InProximityToOthers(Vector3 position)
    {
        foreach(PickUp pickUp in PickUps)
        {
            if (Vector3.Distance(pickUp.transform.position, position) <= proximity) return true;
        }
        return false;
    }

    public void RemovePickUp(PickUp pickUp)
    {
        PickUps.Remove(pickUp);
    }

    public async Task SpawnPickUpsTask()
    {
        for (int pickUpIndex = 0; pickUpIndex < numberToSpawn; pickUpIndex++)
        {
            while (PickUps.Count <= pickUpIndex)
            {
                SpawnPickup();
                await Task.Yield();
            }
            await Task.Yield();

            if (pickUpIndex % 100 == 0)
            {
                Debug.Log("Spawned " + PickUps.Count.ToString());
            }

            if (pickUpIndex == numberToSpawn -1)
            {
                Debug.Log("SpawningComplete");
            }
        }
    }
}
