using Unity.VisualScripting;
using UnityEngine;

public class GerstnerDisplacement
{
    public static Vector3 getDisplacement(float alpha, float beta)
    {
        Vector3 displacement = Vector3.zero;

        float angularFrequency = Mathf.Sqrt(OceanMaster.gravity * OceanMaster.direction.magnitude * (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth));
        float phaseAngle = (OceanMaster.direction.x * alpha) + (OceanMaster.direction.z * beta) - (angularFrequency * (Time.time * OceanMaster.waveSpeed)) - OceanMaster.phase;

        float x_offset = -((OceanMaster.direction.x / OceanMaster.direction.magnitude) * (OceanMaster.amplitude / (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth)) * Mathf.Sin(phaseAngle));
        float y_offset = OceanMaster.amplitude * Mathf.Cos(phaseAngle);
        float z_offset = -((OceanMaster.direction.z / OceanMaster.direction.magnitude) * (OceanMaster.amplitude / (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth)) * Mathf.Sin(phaseAngle));

        displacement += new Vector3(x_offset, y_offset, z_offset);

        return new Vector3(alpha, 0, beta) + displacement;
    }
}