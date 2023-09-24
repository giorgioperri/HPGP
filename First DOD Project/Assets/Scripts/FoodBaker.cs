using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class FoodBaker : MonoBehaviour
{
    public float speed;
    public float radius;
}

public struct FoodMovementData : IComponentData
{
    public float speed;
    public float radius;
}

public class FoodMovementBaker : Baker<FoodBaker>
{
    public override void Bake(FoodBaker authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new FoodMovementData
        {
            speed = authoring.speed,
            radius = authoring.radius
        });
    }
}