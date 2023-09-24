using System.Collections;
using System.Collections.Generic;using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct CharacterSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (characterMovement, localTransform) in SystemAPI.Query<RefRW<CharacterMovementData>, RefRW<LocalTransform>>())
        {
            float3 position = localTransform.ValueRW.Position;
            
            if (Input.GetKey(KeyCode.W))
            {
                position = new float3 { x = position.x, y = position.y, z = position.z + characterMovement.ValueRW.speed };  
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                position = new float3 { x = position.x, y = position.y, z = position.z - characterMovement.ValueRW.speed };  
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                position = new float3 { x = position.x - characterMovement.ValueRW.speed, y = position.y, z = position.z };  
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                position = new float3 { x = position.x + characterMovement.ValueRW.speed, y = position.y, z = position.z };  
            }
            
            characterMovement.ValueRW.currentPosition = position;
            localTransform.ValueRW.Position = position;
        }
    }
}
