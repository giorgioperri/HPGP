using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CharacterBaker : MonoBehaviour
{
    public float speed;
    public float maxVelocity;
}

public struct CharacterMovementData : IComponentData
{
    public float speed;
    public float maxVelocity;
    public float3 currentPosition;
}

public class CharacterMovementBaker : Baker<CharacterBaker>
{
    public override void Bake(CharacterBaker authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new CharacterMovementData
        {
            speed = authoring.speed,
            maxVelocity = authoring.maxVelocity
        });
    }
}