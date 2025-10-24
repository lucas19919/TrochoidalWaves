using UnityEngine;
using Unity.Mathematics;

public class waveManager : MonoBehaviour
{
    [SerializeField] Renderer waveRenderer;
    [SerializeField] Material waveMaterial;

    private float waveSpeed;
    private float waveHeight;

    void Awake()
    {
        waveMaterial = waveRenderer.material;

        waveSpeed = waveMaterial.GetFloat("_WaveSpeed");
        waveHeight = waveMaterial.GetFloat("_WaveHeight");
    }
    public float getWaveHeight(float world_x, float world_z)
    {


        return 0;
    }
}
