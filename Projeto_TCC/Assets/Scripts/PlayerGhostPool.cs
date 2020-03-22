using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostPool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static PlayerGhostPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for(int i = 0; i < 10; i++)
        {
            var instanceAdd = Instantiate(afterImagePrefab);
            instanceAdd.transform.SetParent(transform);

            AddToPool(instanceAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);

        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if(availableObjects.Count == 0)
        {
            GrowPool();
        }

        var instace = availableObjects.Dequeue();
        instace.SetActive(true);
        return instace;
    }
}
