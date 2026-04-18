using Unity.VisualScripting;
using UnityEngine;

public class GerstnerDisplacement
{
    public static Vector3 getHeightAtPosition(float targetX, float targetZ)
    {
        Vector3 currentGuess = new Vector3(targetX, 0, targetZ);

        for (int i = 0; i < 3; i++)
        {
            Vector3 displacement = getDisplacementOnly(currentGuess.x, currentGuess.z);

            Vector3 calculatedPos = currentGuess + displacement;

            Vector2 error = new Vector2(targetX - calculatedPos.x, targetZ - calculatedPos.z);

            currentGuess.x += error.x;
            currentGuess.z += error.y;
        }

        Vector3 finalDisplacement = getDisplacementOnly(currentGuess.x, currentGuess.z);
        return new Vector3(targetX, finalDisplacement.y, targetZ);
    }

    private static Vector3 getDisplacementOnly(float alpha, float beta)
    {
        float angularFrequency = Mathf.Sqrt(OceanMaster.gravity * OceanMaster.direction.magnitude * (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth));
        float phaseAngle = (OceanMaster.direction.x * alpha) + (OceanMaster.direction.z * beta) - (angularFrequency * (Time.time * OceanMaster.waveSpeed)) - OceanMaster.phase;

        float x_offset = -((OceanMaster.direction.x / OceanMaster.direction.magnitude) * (OceanMaster.amplitude / (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth)) * Mathf.Sin(phaseAngle));
        float y_offset = OceanMaster.amplitude * Mathf.Cos(phaseAngle);
        float z_offset = -((OceanMaster.direction.z / OceanMaster.direction.magnitude) * (OceanMaster.amplitude / (float)System.Math.Tanh(OceanMaster.direction.magnitude * OceanMaster.depth)) * Mathf.Sin(phaseAngle));

        return new Vector3(x_offset, y_offset, z_offset);
    }

    public static Vector3 getDisplacement(float alpha, float beta)
    {
        return new Vector3(alpha, 0, beta) + getDisplacementOnly(alpha, beta);
    }
}