using System.Collections;
using System.Collections.Generic;using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SampleSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (movement, position) in SystemAPI.Query<RefRO<MovementData>, RefRW<PositionData>>())
        {
            float yPos = (float)math.sin(SystemAPI.Time.ElapsedTime * movement.ValueRO.speed);
            
            position.ValueRW.position = new float3(position.ValueRO.position.x, yPos, position.ValueRO.position.z);
        }
        
        foreach (var (position, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SpeedData>>())
        {
            float yPos = (float)math.sin(SystemAPI.Time.ElapsedTime * speed.ValueRO.value);
            
            position.ValueRW.Position = new float3(position.ValueRW.Position.x, yPos, position.ValueRW.Position.z);
        }
    }
}
