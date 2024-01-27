using UnityEngine;
using UnityEngine.InputSystem;

public class P_Spawner : MonoBehaviour
{
    private Transform[] spawnPoints;
    private int spawnIndex;
    
    [Header("Player Colors")]
    [SerializeField] private Color[] playerColors;
    
    private void Start()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawner");
        spawnPoints = new Transform[spawns.Length];
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }
    }
    
    public void OnSpawn(PlayerInput playerInput)
    {
        playerInput.transform.SetPositionAndRotation(spawnPoints[spawnIndex].position + new Vector3(0,1,0), spawnPoints[spawnIndex].rotation);
        playerInput.GetComponent<P_Health>().playerColor = playerColors[spawnIndex];
        spawnIndex++;
    }
}
