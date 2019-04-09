using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class AIWanderSystem : JobComponentSystem
{
    [BurstCompile]
    struct AIWanderSystemJob : IJobForEach<MoveTarget, Velocity>
    {
        public Random rand;

        public void Execute(ref MoveTarget target, [ReadOnly] ref Velocity velocity)
        {
            float3 currentVelocity = velocity.Value;
            if (currentVelocity.IsZero())
                currentVelocity = new float3(1, 0, 1).Rotate(rand.NextFloat(0, 360));

            float angle = 5;

            rand.NextFloat();   // this is needed. If you remove this the AI will start to run in circles

            float randomFloat = ((rand.NextFloat() * 2) - 1) * angle;
            target.Value = currentVelocity.Rotate(randomFloat);
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new AIWanderSystemJob
        {
            rand = new Random((uint)UnityEngine.Random.Range(10, 1000))
        };

        return job.Schedule(this, inputDependencies);
    }
}