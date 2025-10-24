using UnityEngine;

public class Floaters : MonoBehaviour
{
    [SerializeField] Transform[] floaters;
    [SerializeField] Rigidbody rb;
    [SerializeField] waveManager instance;

    public float magnitude;

    private void FixedUpdate()
    {
        foreach (Transform floater in floaters)
        {
            Vector3 floaterPos = floater.transform.position;

            float waveHeight = instance.getWaveHeight(floaterPos.x, floaterPos.z);

            if (waveHeight > floaterPos.y)
            {
                Vector3 bouyancyForce = (magnitude * Vector3.up) / floaters.Length;
                rb.AddForceAtPosition(bouyancyForce, floaterPos, ForceMode.Acceleration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform floater in floaters)
        {
            Gizmos.DrawSphere(floater.position, 0.2f);
        }
    }
}
