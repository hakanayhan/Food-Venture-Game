using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Random Position")]
public class RandomPosition : ScriptableObject
{
    public float minXRandomTransform;
    public float maxXRandomTransform;

    public float minYRandomTransform;
    public float maxYRandomTransform;

    public float minZRandomTransform;
    public float maxZRandomTransform;

    public Vector3 GetRandomPos()
    {
        float x = Random.Range(minXRandomTransform, maxXRandomTransform);
        float y = Random.Range(minYRandomTransform, maxYRandomTransform);
        float z = Random.Range(minZRandomTransform, maxZRandomTransform);
        return new Vector3(x, y, z);
    }
}
