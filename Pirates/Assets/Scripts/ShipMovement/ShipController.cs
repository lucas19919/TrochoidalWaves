using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public InputActionReference ship;
    public Transform shipRudder;

    [SerializeField] float steeringSpeed = 1000000000f;

    float steeringAngle;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        steeringAngle = ship.action.ReadValue<float>();
        Debug.Log(steeringAngle);

        rb.AddForceAtPosition(shipRudder.transform.up * steeringSpeed * -steeringAngle, shipRudder.position, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(shipRudder.position, shipRudder.position + shipRudder.transform.up * -steeringSpeed * -steeringAngle * 0.3f);
    }
}
