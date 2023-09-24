using System.Collections;
using System.Collections.Generic;using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct FoodFleeSystem : ISystem
{
    
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        
        float3 currentCharacterPosition = new float3(0,0,0);
        
        foreach (var characterMovement in SystemAPI.Query<RefRO<CharacterMovementData>>())
        {
            currentCharacterPosition = characterMovement.ValueRO.currentPosition;
        }
        
        foreach (var (foodMovement, localTransform) in SystemAPI.Query<RefRO<FoodMovementData>, RefRW<LocalTransform>>())
        {
            float3 position = localTransform.ValueRW.Position;
            
            //flee from the character
            float3 direction = position - currentCharacterPosition;
            float3 fleeDirection = math.normalize(direction);
            
            position = new float3 { x = position.x + fleeDirection.x * foodMovement.ValueRO.speed, y = position.y, z = position.z + fleeDirection.z * foodMovement.ValueRO.speed };

            localTransform.ValueRW.Position = position;
        }
    }
}
