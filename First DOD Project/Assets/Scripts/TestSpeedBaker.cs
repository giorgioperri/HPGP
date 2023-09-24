using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TestSpeedBaker : MonoBehaviour
{
  public float sampleSpeed;
}

public struct SpeedData : IComponentData
{
  public float value;
}

public class SimpleBaker : Baker<TestSpeedBaker>
{
  public override void Bake(TestSpeedBaker authoring)
  {
    var entity = GetEntity(TransformUsageFlags.Dynamic);
    AddComponent(entity, new SpeedData
    {
      value = authoring.sampleSpeed
    });
  }
}
