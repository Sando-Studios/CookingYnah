using UnityEngine;

public class ArcGrenade : MonoBehaviour
{
    private Vector3 targetPosition;
    private float arcHeight = 5f;
    private float timeToReachTarget = 2f;

    public void InitializeGrenade(Vector3 targetPos)
    {
        targetPosition = targetPos;

        Vector3 initialVelocity = CalculateInitialVelocity(targetPosition, arcHeight, timeToReachTarget);
        GetComponent<Rigidbody>().velocity = initialVelocity;
    }

    private Vector3 CalculateInitialVelocity(Vector3 target, float height, float time)
    {
        float displacementY = target.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2f * Physics.gravity.y * height);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY;
    }
}
