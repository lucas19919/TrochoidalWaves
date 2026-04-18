using UnityEngine;

public class OceanMaster : MonoBehaviour
{
    [SerializeField] Material waveMaterial;

    public static Vector3 direction;
    public static float amplitude;
    public static float waveSpeed;
    public static float phase;    
    public static float gravity;
    public static float depth;

    void FixedUpdate()
    {
        direction = waveMaterial.GetVector("_Direction");
        amplitude = waveMaterial.GetFloat("_Amplitude");
        waveSpeed = waveMaterial.GetFloat("_WaveSpeed");
        phase = waveMaterial.GetFloat("_Phase");
        gravity = waveMaterial.GetFloat("_Gravity");
        depth = waveMaterial.GetFloat("_Depth");
    }
}
