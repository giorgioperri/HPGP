using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
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
        Entity entityToDestroy = Entity.Null;

        NativeArray<float3> characterPosition = new NativeArray<float3>(1, Allocator.TempJob);

        int foodLeft = 0;

        var bla = new SetCharacterPositionJob
        {
            characterPosition = characterPosition
        }.Schedule(state.Dependency);

        bla.Complete();
        
        
        
        
        /*foreach (var characterMovement in SystemAPI.Query<RefRO<CharacterMovementData>>())
        {
            currentCharacterPosition = characterMovement.ValueRO.currentPosition;
        }*/

        foreach (var (foodMovement, localTransform, foodStatus) in SystemAPI.Query<RefRO<FoodMovementData>, RefRW<LocalTransform>, RefRO<FoodStatusData>>())
        {
            foodLeft++;
            float3 position = localTransform.ValueRW.Position;
            
            //flee from the character
            float3 direction = position - characterPosition[0];
            float3 fleeDirection = math.normalize(direction);
            
            position = new float3 { x = position.x + fleeDirection.x * foodMovement.ValueRO.speed, y = position.y, z = position.z + fleeDirection.z * foodMovement.ValueRO.speed };

            localTransform.ValueRW.Position = position;
        }

        GameManager.Instance.foodLeft = foodLeft;

        foreach (var (foodMovement, localTransform, entity) in SystemAPI.Query<RefRO<FoodMovementData>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            if (!state.EntityManager.HasComponent<FoodStatusData>(entity))
            {
                float3 position = localTransform.ValueRW.Position;
                float scale = localTransform.ValueRW.Scale;
                
                position = new float3 { x = position.x, y = position.y + 1 * foodMovement.ValueRO.speed, z = position.z};
                
                scale = scale >= 0 ? scale - 0.01f : 0;

                if (scale == 0)
                {
                    entityToDestroy = entity;
                }
                
                localTransform.ValueRW.Position = position;
                localTransform.ValueRW.Scale = scale;
            }
        }
        
        if (entityToDestroy != Entity.Null && !state.EntityManager.HasComponent<CharacterMovementData>(entityToDestroy))
        {
            var child = state.EntityManager.GetBuffer<Child>(entityToDestroy);
            
            foreach (var ent in child)
            {
                state.EntityManager.DestroyEntity(ent.Value);
            }
            
            state.EntityManager.DestroyEntity(entityToDestroy);
        }
        
    }
}

[BurstCompile]
public partial struct SetCharacterPositionJob : IJobEntity
{   
    //variables to pass here
    [NativeDisableParallelForRestriction]
    public NativeArray<float3> characterPosition;

    void Execute(in CharacterMovementData characterMovementData)
    {
        //logic code here
        characterPosition[0] = characterMovementData.currentPosition;
    }
}