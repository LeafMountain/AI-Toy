﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class SpawnerSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [BurstCompile]
    struct SpawnerSystemJob : IJobForEachWithEntity<Spawner, LocalToWorld>
    {
        // Add fields here that your job needs to do its work.
        // For example,
        //    public float deltaTime;
        public EntityCommandBuffer CommandBuffer;


        public void Execute(Entity entity, int index, [ReadOnly] ref Spawner spawner, [ReadOnly] ref LocalToWorld location)
        {
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job. Note that the 'rotation' parameter is
            // marked as [ReadOnly], which means it cannot be modified,
            // but allows this job to run in parallel with other jobs
            // that want to read Rotation component data.
            // For example,
            //     translation.Value += mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
            for (int i = 0; i < spawner.Count; i++)
            {
                Entity instance = CommandBuffer.Instantiate(spawner.Prefab);
                float3 position = math.transform(location.Value, new float3(0));
                CommandBuffer.SetComponent(instance, new Translation { Value = position });
            }

            CommandBuffer.DestroyEntity(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new SpawnerSystemJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDependencies);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
        return job;

        // Assign values to the fields on your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     job.deltaTime = UnityEngine.Time.deltaTime;



        // Now that the job is set up, schedule it to be run. 
        // return job.Schedule(this, inputDependencies);
    }
}