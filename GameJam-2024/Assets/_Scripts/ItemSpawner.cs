using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        
        SpawnItems();
    }

    private async void SpawnItems()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject prefab =
            Variables.Instance.SpawnableItems[Random.Range(0, Variables.Instance.SpawnableItems.Length)];
        GameObject item = Instantiate(prefab, spawnPoint.position + new Vector3(0, prefab.transform.position.y, 0), Quaternion.identity);
        if (item.TryGetComponent(out Throwable throwable))
        {
            throwable.Start();
        }
        
        await Task.Delay(Random.Range(2000, 7000));
        SpawnItems();
    }
}
