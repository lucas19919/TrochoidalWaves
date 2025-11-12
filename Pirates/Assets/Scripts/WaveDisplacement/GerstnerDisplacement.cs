using UnityEngine;

public class GerstnerDisplacement
{
    public static Vector3 getDisplacement(float alpha, float beta, GerstnerWave[] waves)
    {
        Vector3 displacement = Vector3.zero;

        if (waves == null)
        {
            return new Vector3(alpha, 0, beta);
        }

        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].direction.magnitude == 0)
                continue;

            float angularFrequency = Mathf.Sqrt(OceanController.gravity * waves[i].direction.magnitude * (float)System.Math.Tanh(waves[i].direction.magnitude * OceanController.depth));
            float phaseAngle = (waves[i].direction.x * alpha) + (waves[i].direction.z * beta) - (angularFrequency * (Time.time * waves[i].waveSpeed)) - waves[i].phase;

            float x_offset = -((waves[i].direction.x / waves[i].direction.magnitude) * (waves[i].amplitude / (float)System.Math.Tanh(waves[i].direction.magnitude * OceanController.depth)) * Mathf.Sin(phaseAngle));
            float y_offset = waves[i].amplitude * Mathf.Cos(phaseAngle);
            float z_offset = -((waves[i].direction.z / waves[i].direction.magnitude) * (waves[i].amplitude / (float)System.Math.Tanh(waves[i].direction.magnitude * OceanController.depth)) * Mathf.Sin(phaseAngle));

            displacement += new Vector3(x_offset, y_offset, z_offset);
        }

        return new Vector3(alpha, 0, beta) + displacement;
    }
}