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
       
        foreach (var foodTarget in SystemAPI.Query<RefRW<FoodTargetData>>())
        {
            foreach (var evt in SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation().TriggerEvents)
            {
                Debug.Log(evt);
            }
        }
    }
}