using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [Header("Control Scheme")]
    [SerializeField] InputActionReference controls;

    [Header("Ship Parts")]
    [SerializeField] Transform rudder;
    [SerializeField] Transform prop;

    [Header("General Settings")]
    [SerializeField] float steeringSensitivity;
    [SerializeField] float propSpeed;

    [Header("Physics Settings")]
    [SerializeField] float fluidDensity;
    [SerializeField] float rudderSurfaceArea;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 input = controls.action.ReadValue<Vector2>();
        float yaw = input.x;
        float thrust = input.y;

        rb.AddForceAtPosition(this.transform.forward * thrust * propSpeed, prop.position);
        rudder.Rotate(0, -yaw * steeringSensitivity * Time.deltaTime, 0);

        Vector3 fluidVelocity = -rb.GetPointVelocity(rudder.position);
        float attackAngle = Vector3.SignedAngle(fluidVelocity, -rudder.transform.forward, rudder.transform.up) * Mathf.Deg2Rad;
        
        float cL = Mathf.Sin(attackAngle * 2f) * 3.0f;
        float cD = Mathf.Pow(Mathf.Sin(attackAngle), 2) * 2.0f;

        float liftForce = 0.5f * fluidDensity * fluidVelocity.sqrMagnitude * rudderSurfaceArea * cL;
        float dragForce = 0.5f * fluidDensity * fluidVelocity.sqrMagnitude * rudderSurfaceArea * cD;

        Vector3 liftDirection = Vector3.Cross(fluidVelocity.normalized, rudder.transform.up);
        Vector3 dragDirection = fluidVelocity.normalized;

        rb.AddForceAtPosition(liftDirection * liftForce, rudder.position);
        rb.AddForceAtPosition(dragDirection * dragForce, rudder.position);

        Debug.Log("L: " + liftDirection * liftForce);
        Debug.Log("D: " + dragDirection * dragForce);
    }

    private void OnDrawGizmos()
    {
        //rudder forward
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rudder.position, rudder.position - rudder.transform.forward);

        //rudder tangent
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rudder.position, rudder.position + rudder.transform.right);

        //forward
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 5);
    }
}
