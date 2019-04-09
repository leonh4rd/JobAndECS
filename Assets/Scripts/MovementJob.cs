using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public struct MovementJob : IJobParallelForTransform
{
    public float moveSpeed;
    public float bound1;
    public float bound2;
    public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        Vector3 pos = transform.position;
        pos += moveSpeed * deltaTime * (transform.rotation * new Vector3(0f, 0f, 1f));
        if(pos.z  < bound1)
        {
            pos.z = bound2;
        }
        transform.position = pos;
    }
}
