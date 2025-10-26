using UnityEngine;
using Unity.Mathematics;

public class waveManager : MonoBehaviour
{
    [Header("Wave Source")]
    [SerializeField] Renderer waveRenderer;
    [SerializeField] Material waveMaterial;

    [Header("Gizmo Settings")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private int gridSize = 20;
    [SerializeField] private float gridSpacing = 1.0f;
    [SerializeField] private float gizmoRadius = 0.1f;
    [SerializeField] private Color gizmoColor = Color.cyan;

    // --- Globals ---
    private float gravity;

    // --- Wave 1 ---
    private Vector4 dir1;
    private float depth1;
    private float phase1;
    private float scaleTime1;
    private float amp1;

    // --- Wave 2 ---
    private Vector4 dir2;
    private float depth2;
    private float phase2;
    private float scaleTime2;
    private float amp2;

    // --- Wave 3 ---
    private Vector4 dir3;
    private float depth3;
    private float phase3;
    private float scaleTime3;
    private float amp3;

    // --- Wave 4 ---
    private Vector4 dir4;
    private float depth4;
    private float phase4;
    private float scaleTime4;
    private float amp4;

    void Awake()
    {
        if (waveMaterial == null && waveRenderer != null)
        {
            // Use sharedMaterial to ensure we read the asset, not an instance
            waveMaterial = waveRenderer.sharedMaterial;
        }

        if (waveMaterial == null)
        {
            Debug.LogError("WaveManager: No material assigned or found on renderer!", this);
        }
    }

    void Update()
    {
        // Keep loading properties to see inspector changes in real-time
        if (waveMaterial != null && Application.isPlaying)
        {
            LoadWaveProperties();
        }
    }

    void LoadWaveProperties()
    {
        gravity = waveMaterial.GetFloat("_Gravity");

        dir1 = waveMaterial.GetVector("_Direction_1");
        depth1 = waveMaterial.GetFloat("_Depth_1");
        phase1 = waveMaterial.GetFloat("_Phase_1");
        scaleTime1 = waveMaterial.GetFloat("_ScaleTime_1");
        amp1 = waveMaterial.GetFloat("_Amplitude_1");

        dir2 = waveMaterial.GetVector("_Direction_2");
        depth2 = waveMaterial.GetFloat("_Depth_2");
        phase2 = waveMaterial.GetFloat("_Phase_2");
        scaleTime2 = waveMaterial.GetFloat("_ScaleTime_2");
        amp2 = waveMaterial.GetFloat("_Amplitude_2");

        dir3 = waveMaterial.GetVector("_Direction_3");
        depth3 = waveMaterial.GetFloat("_Depth_3");
        phase3 = waveMaterial.GetFloat("_Phase_3");
        scaleTime3 = waveMaterial.GetFloat("_ScaleTime_3");
        amp3 = waveMaterial.GetFloat("_Amplitude_3");

        dir4 = waveMaterial.GetVector("_Direction");
        depth4 = waveMaterial.GetFloat("_Depth");
        phase4 = waveMaterial.GetFloat("_Phase");
        scaleTime4 = waveMaterial.GetFloat("_ScaleTime");
        amp4 = waveMaterial.GetFloat("_Amplitude");
    }

    public float getWaveHeight(float world_x, float world_z)
    {
        return GetWaveDisplacement(world_x, world_z).y;
    }

    public Vector3 GetWaveDisplacement(float world_x, float world_z)
    {
        Vector3 disp1 = GerstnerWave(world_x, world_z, dir1, amp1, depth1, phase1, scaleTime1);
        Vector3 disp2 = GerstnerWave(world_x, world_z, dir2, amp2, depth2, phase2, scaleTime2);
        Vector3 disp3 = GerstnerWave(world_x, world_z, dir3, amp3, depth3, phase3, scaleTime3);
        Vector3 disp4 = GerstnerWave(world_x, world_z, dir4, amp4, depth4, phase4, scaleTime4);

        return disp1 + disp2 + disp3 + disp4;
    }

    private Vector3 GerstnerWave(float world_x, float world_z, Vector4 dir, float amp, float depth, float ph, float scaleT)
    {
        // --- THIS IS THE FIX ---
        // The shader graph uses the 3D Length of the direction vector for 'k'
        // (image_3e48c7.png, image_3e4888.png)
        float k = new Vector3(dir.x, dir.y, dir.z).magnitude;

        if (k == 0.0f)
        {
            return Vector3.zero;
        }

        // Omega calculation is correct
        float omega = (float)math.sqrt(gravity * k * math.tanh(k * depth));
        float zeit = Time.time * scaleT;

        // The dot product uses (dir.x, dir.y) and (world.x, world.z)
        // (image_3e48e5.png)
        float f = (dir.x * world_x) + (dir.y * world_z) + (omega * zeit) - ph;

        float cos_f = (float)math.cos(f);
        float sin_f = (float)math.sin(f);

        // Y (Height) displacement is correct
        float y_disp = amp * cos_f;

        // X displacement uses dir.x / k (image_3e4888.png)
        float x_disp = -(dir.x / k) * amp * sin_f;

        // Z displacement uses dir.y / k (image_3e4888.png)
        float z_disp = -(dir.y / k) * amp * sin_f;

        // -----------------------

        return new Vector3(x_disp, y_disp, z_disp);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos || !Application.isPlaying || waveMaterial == null || waveRenderer == null)
        {
            return;
        }

        Gizmos.color = gizmoColor;
        Vector3 origin = waveRenderer.transform.position;

        for (int x = -gridSize / 2; x < gridSize / 2; x++)
        {
            for (int z = -gridSize / 2; z < gridSize / 2; z++)
            {
                float xPos = origin.x + x * gridSpacing;
                float zPos = origin.z + z * gridSpacing;

                Vector3 basePos = new Vector3(xPos, origin.y, zPos);
                Vector3 displacement = GetWaveDisplacement(xPos, zPos);

                Gizmos.DrawSphere(basePos + displacement, gizmoRadius);
            }
        }
    }
}