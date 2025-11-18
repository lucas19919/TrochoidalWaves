using UnityEngine;

public class Floaters : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] Rigidbody rb;

    [Header("Floaters")]
    [SerializeField] Transform[] floaters;

    [Header("Buoyancy Settings")]
    [SerializeField] float waterLevel = 0f;
    [SerializeField] float buoyancyMagnitude = 15f;
    [SerializeField] float dampingFactor = 0.8f;

    [Header("Ocean Controller")]


    private int floaterCount;

    private void Awake()
    {
        if (floaters.Length == 0)
        {
            Debug.LogError("No floaters assigned!", this);
            enabled = false;
            return;
        }
        floaterCount = floaters.Length;
    }

    private void FixedUpdate()
    {
        foreach (Transform floater in floaters)
        {
            Vector3 floaterPos = floater.position;

            float waveDisplacement = GerstnerDisplacement.getDisplacement(floaterPos.x, floaterPos.z).y;
            float currentWaterHeight = waterLevel + waveDisplacement;

            float submersionDepth = currentWaterHeight - floaterPos.y;

            if (submersionDepth > 0)
            {
                Vector3 buoyancyForce = Vector3.up * submersionDepth * (buoyancyMagnitude / floaterCount);
                rb.AddForceAtPosition(buoyancyForce, floaterPos, ForceMode.Acceleration);

                float verticalVelocity = rb.GetPointVelocity(floaterPos).y;
                Vector3 dampingForce = Vector3.up * -verticalVelocity * (dampingFactor / floaterCount);
                rb.AddForceAtPosition(dampingForce, floaterPos, ForceMode.Acceleration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (floaters == null) return;

        Gizmos.color = Color.red;
        foreach (Transform floater in floaters)
        {
            if (floater != null)
            {
                Gizmos.DrawSphere(floater.position, 0.1f);
            }
        }

        Gizmos.color = Color.blue;
        foreach (Transform floater in floaters)
        {
            if (floater != null)
            {
                Gizmos.DrawSphere(GerstnerDisplacement.getDisplacement(floater.position.x, floater.position.z), 0.1f);
            }
        }
    }
}