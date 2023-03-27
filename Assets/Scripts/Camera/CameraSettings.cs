using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Camera Settings")]
public class CameraSettings : ScriptableObject
{
    public float moveSpeed;
    public float minZ;
    public float maxZ;
    public float dragSpeed;
}
