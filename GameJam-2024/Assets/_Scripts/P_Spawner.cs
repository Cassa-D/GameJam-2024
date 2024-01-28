using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Spawner : MonoBehaviour
{
    private List<Transform> spawnPoints = new List<Transform>();
    private int spawnIndex;
    
    private void Start()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var t in spawns)
        {
            spawnPoints.Add(t.transform);
        }
    }
    
    public async void OnSpawn(PlayerInput playerInput)
    {
        while (spawnPoints.Count < 4)
        {
            await Task.Delay(100);
        }
        
        if (playerInput.playerIndex < spawnIndex) return;
        
        playerInput.transform.SetPositionAndRotation(spawnPoints[playerInput.playerIndex].position + new Vector3(0,1,0), spawnPoints[playerInput.playerIndex].rotation);
        playerInput.GetComponent<P_Health>().playerColor = Variables.Instance.PlayerColors[playerInput.playerIndex];
        playerInput.GetComponent<P_Health>().playerIndex = playerInput.playerIndex;
        spawnIndex++;
    }
}
