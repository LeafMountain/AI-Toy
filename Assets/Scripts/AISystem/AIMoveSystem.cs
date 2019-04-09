using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class AIMoveSystem : JobComponentSystem
{
    [BurstCompile]
    struct AIMoveSystemJob : IJobForEach<Translation, MoveSpeed, MoveForce, MoveTarget, Velocity>
    {
        public void Execute(ref Translation translation, ref MoveSpeed speed, ref MoveForce force, ref MoveTarget target, ref Velocity velocity)
        {
            if (target.Value.x == 0 & target.Value.y == 0 & target.Value.z == 0)
                return;

            // The desired direction
            float3 desired;
            // Amount of force to apply
            float3 steer;

            float3 futurePosition = translation.Value + velocity.Value;

            // UnityEngine.Debug.Log(target.Value);
            // desired = (target.Value) - futurePosition;
            desired = (translation.Value + target.Value) - translation.Value;
            desired = math.normalize(desired);
            desired *= speed.Value;

            // Find the force to apply 
            steer = desired - velocity.Value;
            steer = math.normalize(steer) * force.Value;
            steer.y = 0;

            velocity.Value = steer;
            // UnityEngine.Debug.DrawRay(translation.Value, steer, UnityEngine.Color.red);

            // velocity.Value = desired;

            // Reset the value for the next frame
            // target.Value = translation.Value;

            // UnityEngine.Debug.Log("Test");

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new AIMoveSystemJob();

        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}