using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Variables : Singleton<Variables>
{
    [BoxGroup("Player Infos")]
    [SerializeField] private int playerHealth = 3;
    public int PlayerHealth => playerHealth;

    [BoxGroup("Player Infos")]
    [SerializeField]
    private float playerSpeed = 5f;
    public float PlayerSpeed => playerSpeed;

    [BoxGroup("Player Infos")]
    [SerializeField]
    private Color[] playerColors;
    public Color[] PlayerColors => playerColors;

    [BoxGroup("Fight Infos")]
    [SerializeField] [Tooltip("Time in milliseconds")]
    private int stunTime = 500;
    public int StunTime => stunTime;

    [BoxGroup("Punch Infos")]
    [SerializeField]
    private int punchCooldown = 1000;
    public int PunchCooldown => punchCooldown;

    [BoxGroup("Punch Infos")]
    [SerializeField][Tooltip("Time in milliseconds")]
    private int punchDuration = 15;
    public int PunchDuration => punchDuration;

    [BoxGroup("Punch Infos")]
    [SerializeField]
    private float punchForce = 15f;
    public float PunchForce => punchForce;

    [BoxGroup("Punch Infos")]
    [SerializeField]
    private int punchDamage = 1;
    public int PunchDamage => punchDamage;

    [BoxGroup("Grab Infos")]
    [SerializeField]
    private float throwForce = 12.5f;
    public float ThrowForce => throwForce;

    [BoxGroup("Grab Infos")]
    [SerializeField]
    private int throwDamage = 1;
    public int DefaultThrowableDamage => throwDamage;

    [BoxGroup("Gameplay Infos")]
    [SerializeField]
    private float streetSpeed = 5f;
    public float StreetSpeed => streetSpeed;
    
    [BoxGroup("Gameplay Infos")]
    [SerializeField]
    private ItemScriptable[] boxItems;
    public ItemScriptable[] BoxItems => boxItems;
    
    [BoxGroup("Gameplay Infos")]
    [SerializeField]
    private GameObject[] spawnableItems;
    public GameObject[] SpawnableItems => spawnableItems;
    
    [BoxGroup("Respawn Infos")]
    [SerializeField]
    private float respawnTime = 5f;
    public float RespawnTime => respawnTime;
    
    [BoxGroup("Respawn Infos")]
    [SerializeField]
    private float respawnRange = 5f;
    public float RespawnRange => respawnRange;

    [BoxGroup("Respawn Infos")]
    [SerializeField]
    private Vector3 respawnOffset = new Vector3(0, 0);
    public Vector3 RespawnOffset => respawnOffset;
    
    [BoxGroup("Score")]
    [ShowInInspector, ReadOnly]
    private int[] scores = new int[4];
    public int[] Scores => scores;
    
    [BoxGroup("Score")]
    [ShowInInspector, ReadOnly]
    private int[] deaths = new int[4];
    public int[] Deaths => deaths;
}
