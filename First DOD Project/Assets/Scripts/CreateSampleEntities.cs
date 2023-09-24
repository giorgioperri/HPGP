using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct CreateSampleEntities : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        Entity entity = state.EntityManager.CreateEntity();
        state.EntityManager.SetName(entity, "Sample Entity");

        //state.EntityManager.AddComponent<MovementData>(entity);
        //state.EntityManager.AddComponent<PositionData>(entity);

        state.EntityManager.AddComponentData(entity, new MovementData
        {
            speed = 2f, amplitude = 2f
        });
        
        state.EntityManager.AddComponentData(entity, new PositionData
        {
            position = new float3(0f, 0f, 0f)
        });
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        
    }
}

public struct MovementData : IComponentData
{
    public float speed;
    public float amplitude;
}

public struct PositionData : IComponentData
{
    public float3 position;
}