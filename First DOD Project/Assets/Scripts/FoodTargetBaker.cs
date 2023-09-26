using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class FoodTargetBaker : MonoBehaviour
{
}

public struct FoodTargetData : IComponentData { }

public class FoodTargetTriggerBaker : Baker<FoodTargetBaker>
{
    public override void Bake(FoodTargetBaker authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new FoodTargetData());
    }
}