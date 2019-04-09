using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class GameManager : MonoBehaviour
{
    public Transform modelPrefab;

    TransformAccessArray transforms;
    MovementJob moveJob;
    JobHandle moveHandle;

    private void OnDisable() 
    {
        moveHandle.Complete();
        transforms.Dispose();
    }

    void Start()
    {
        transforms = new TransformAccessArray(0, -1);
    }

    void Update()
    {
        moveHandle.Complete();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddDecoys(100);
        }

        moveJob = new MovementJob()
        {
            moveSpeed = 1.0f,
            bound1 = 20f,
            bound2 = -20f,
            deltaTime = Time.deltaTime
        };
        moveHandle = moveJob.Schedule(transforms);
        JobHandle.ScheduleBatchedJobs();
    }

    private void AddDecoys(int amount)
    {
        moveHandle.Complete();
        transforms.capacity = transforms.length + amount;
        for(int i = 0; i < amount; i++)
        {
            float xVal = Random.Range(-20, 20);
            float zVal = Random.Range(-20, 20);
            Vector3 pos = new Vector3(xVal, 0f, zVal);
            Quaternion rot = Quaternion.Euler(0f, 180f, 0f);
            var obj = Instantiate(modelPrefab, pos, rot);
            transforms.Add(obj.transform);
        }
    }
}
