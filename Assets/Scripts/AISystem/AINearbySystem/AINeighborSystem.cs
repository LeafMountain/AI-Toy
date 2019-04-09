using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class AINeighborSystem : JobComponentSystem
{
    [BurstCompile]
    struct AINeighborSystemJob : IJobForEach<Translation, AINeighbor>
    {
        public void Execute([ReadOnly] ref Translation translation, [ReadOnly] ref AINeighbor neightbors)
        {

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new AINeighborSystemJob();
        return job.Schedule(this, inputDependencies);
    }
}