using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public partial struct FoodTargetSystem : ISystem
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
        
        
        foreach (var foodTarget in SystemAPI.Query<RefRW<FoodTargetData>>())
        {
            foreach (var evt in SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation().TriggerEvents)
            {
                entityToDestroy = evt.EntityA;
            }
        }
        
        if (entityToDestroy != Entity.Null && !state.EntityManager.HasComponent<CharacterMovementData>(entityToDestroy))
        {
            var child = state.EntityManager.GetBuffer<Child>(entityToDestroy);
            
            foreach (var entity in child)
            {
                state.EntityManager.DestroyEntity(entity.Value);
            }
            
            state.EntityManager.DestroyEntity(entityToDestroy);
            
        }
    }
}