using UnityEngine;

public class OceanController : MonoBehaviour
{
    [Header("Global Settings")]
    [SerializeField] public static float gravity = 9.81f;
    [SerializeField] public static float depth = 10f;

    [Header("Wave Settings")]
    [SerializeField] public GerstnerWave[] waves;
}
