using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ScavangerHunt_PickUpSpawner : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] ScavangerHunt_PickUp PickUpPrefab;
    [field: SerializeField] public List<ScavangerHunt_PickUp> PickUps { get; private set; } 
        = new List<ScavangerHunt_PickUp>();
    [SerializeField] int numberToSpawn;
    [SerializeField] float proximity;
    [SerializeField] LayerMask spherCastLayers;
    [SerializeField] LayerMask groundLayers;

    public bool SpawnComplete => PickUps.Count >= numberToSpawn /*&& positionsChecked >= numberToSpawn*/;

    int positionsChecked;

    private void SpawnPickup()
    {
        Vector3 position = nextPosition;
        //if (InProximityToOthers(position)) return;

        if (Physics.SphereCastAll(position, proximity, Vector3.up, proximity, spherCastLayers).Length == 0)
        {
            ScavangerHunt_PickUp pickUp = Instantiate(PickUpPrefab, position, Quaternion.identity);
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
            float height = 500;
            Vector3 position = new Vector3(x, height, y);
            if (Physics.Raycast(position, new Vector3(0, -1, 0), out RaycastHit hit, 1000, groundLayers))
            {
                return hit.point;
            }

            return new Vector3();
        }
    }

    private bool InProximityToOthers(Vector3 position)
    {
        foreach(ScavangerHunt_PickUp pickUp in PickUps)
        {
            if (Vector3.Distance(pickUp.transform.position, position) <= proximity) return true;
        }
        return false;
    }

    public void RemovePickUp(ScavangerHunt_PickUp pickUp)
    {
        PickUps.Remove(pickUp);
    }

    public async Task SpawnPickUpsTask()
    {
        if (SpawnComplete) return;

        for (int pickUpIndex = PickUps.Count; pickUpIndex < numberToSpawn; pickUpIndex++)
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

    public async Task CheckPositionsTask()
    {
        ScavangerHunt_PickUp[] pickUps = GetComponentsInChildren<ScavangerHunt_PickUp>();

        foreach (ScavangerHunt_PickUp pickUp in pickUps)
        {
            if (Physics.Raycast(pickUp.transform.position - new Vector3(0, -10, 0), new Vector3(0, -1, 0), out RaycastHit hit, 1000, groundLayers))
            {
                pickUp.transform.position = hit.point;
                positionsChecked++;
                await Task.Yield();
            }
        }
    }

}
