using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrashData
{
    public GameObject prefab;       // Drag prefab dari folder Project, BUKAN dari scene
    public Vector3 position;
    public Quaternion rotation;
}

public class TrashSpawner : MonoBehaviour
{
    public List<TrashData> trashList; // Data prefab dan posisi awal sampah
    private List<GameObject> spawnedTrash = new List<GameObject>();

    void Start()
    {
        SpawnAllTrash();
    }

    public void SpawnAllTrash()
    {
        foreach (var data in trashList)
        {
            if (data.prefab != null)
            {
                GameObject trash = Instantiate(data.prefab, data.position, data.rotation);
                spawnedTrash.Add(trash);
            }
            else
            {
                Debug.LogWarning("Trash prefab is null. Make sure you assign prefab from Project folder.");
            }
        }
    }

    public void ClearTrash()
    {
        foreach (var obj in spawnedTrash)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedTrash.Clear();
    }

    public void RespawnTrash()
    {
        ClearTrash();
        SpawnAllTrash();
    }
}
