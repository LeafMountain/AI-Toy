using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class AIAvoidSystem : JobComponentSystem
{
    [BurstCompile]
    struct AIAvoidSystemJob : IJobForEach<Translation, MoveTarget, AINeighbor>
    {
        public float neighborDistance;
        // public NativeArray<Translation> entityTranslations;

        public void Execute([ReadOnly] ref Translation translation, ref MoveTarget moveTarget, [ReadOnly] ref AINeighbor neighbors)
        {
            // for (int i = 0; i < neighbors.Values.Length; i++)
            // {
            //     if (math.distance(translation.Value, neighbors.Values[i]) < neighborDistance)
            //     {
            //         moveTarget.Value += (translation.Value - neighbors.Values[i]) * 2;
            //     }
            // }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new AIAvoidSystemJob();
        job.neighborDistance = 5;
        // job.entityTranslations = EntityManager.getcomponent
        return job.Schedule(this, inputDependencies);
    }
}