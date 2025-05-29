
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class FlyController : MonoBehaviour
{
    [Header("References")]
    public Transform headTransform;
    public Transform leftHand;
    public Transform rightHand;
    public Transform groundCheckPoint;
    public Rigidbody rb;
    public Animator animator;
    public Volume flyingVolume;
    public Transform[] spawnPoints;

    [Header("Flight Settings")]
    public float flySpeed = 4f;
    public float handAngleThreshold = 80f;
    public float handDistanceThreshold = 0.4f;
    public float flySmoothness = 3f;
    public float liftForce = 2.5f;

    [Header("Walk Settings")]
    public float walkThreshold = 0.1f;
    public float groundCheckDistance = 0.3f;
    public float stopSmoothing = 3f;

    private bool isFlying = false;
    private bool isWalking = false;

    void Start()
    {
        rb.useGravity = false;
    }

    void Update()
    {
        float handAngle = Vector3.Angle(leftHand.forward, rightHand.forward);
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);

        isFlying = handAngle > handAngleThreshold && handDistance > handDistanceThreshold;

        if (isFlying)
        {
            Vector3 flyDir = headTransform.forward;
            flyDir.y += liftForce;
            Vector3 targetVelocity = flyDir.normalized * flySpeed;

            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * flySmoothness);

            animator.SetBool("isFlying", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            isWalking = horizontalVelocity.magnitude > walkThreshold && IsGrounded();

            Vector3 slowedVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, Time.deltaTime * stopSmoothing);
            rb.linearVelocity = slowedVelocity;

            animator.SetBool("isFlying", false);
            animator.SetBool("isWalking", isWalking);

            if (isWalking)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        if (flyingVolume != null)
            flyingVolume.weight = isFlying ? 1f : 0f;
    }

    private bool IsGrounded()
    {
        if (groundCheckPoint == null) return false;
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ocean") && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawn = spawnPoints[randomIndex];
            transform.position = randomSpawn.position;
            rb.linearVelocity = Vector3.zero;
            animator.SetBool("isFlying", false);
            animator.SetBool("isWalking", false);
        }
    }
}
