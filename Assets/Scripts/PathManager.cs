using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject pathPrefab; 
    public Transform player; 
    private Vector3 lastPathEndPos; 

    public float spawnDistance = 50f; 
    public float pathLength = 10f; 
    private void Start()
    {
        lastPathEndPos = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(player.position, lastPathEndPos) < spawnDistance)
        {
            SpawnPath();
        }
    }
    private void SpawnPath()
    {
        GameObject newPath = Instantiate(pathPrefab, lastPathEndPos, Quaternion.identity);
        lastPathEndPos += Vector3.forward * pathLength;
    }
}
