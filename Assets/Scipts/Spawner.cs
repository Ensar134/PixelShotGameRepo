using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject spawnObject;

    #region Singleton

    public static Spawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        SpawnPos = transform.position;
    }

    void SpawnNewObject()
    {
        GameObject prefab = Resources.Load("Player") as GameObject;
        Instantiate(prefab, SpawnPos, Quaternion.identity);
    }

    public void NewSpawnRequest()
    {
        Invoke(nameof(SpawnNewObject), 0.3f);        
    }
}