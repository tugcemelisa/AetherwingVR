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

    [Header("Flight Settings")]
    public float flySpeed = 4f;
    public float handAngleThreshold = 80f;
    public float handDistanceThreshold = 0.4f;
    public float flySmoothness = 2f;

    [Header("Walk Settings")]
    public float walkThreshold = 0.1f;
    public float groundCheckDistance = 0.2f;
    public float stopSmoothing = 2f;

    private bool isFlying = false;
    private bool isWalking = false;

    void Start()
    {
        // Rigidbody ayarları (düşmeyi önlemek için)
        rb.useGravity = false;
    }

    void Update()
    {
        float handAngle = Vector3.Angle(leftHand.forward, rightHand.forward);
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);

        isFlying = handAngle > handAngleThreshold && handDistance > handDistanceThreshold;

        if (isFlying)
        {
            Vector3 flyDirection = headTransform.forward;
            Vector3 targetVelocity = flyDirection * flySpeed;

            // Hemen aşağı düşmesin diye sadece yavaşça yön değiştir
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * flySmoothness);

            animator.SetBool("isFlying", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            // Uçmuyorsan hızla düşme, sadece yumuşakça yere in
            Vector3 currentVel = rb.linearVelocity;
            Vector3 slowedVel = new Vector3(0, Mathf.Lerp(currentVel.y, 0, Time.deltaTime * stopSmoothing), 0);
            rb.linearVelocity = slowedVel;

            Vector3 horizontalVelocity = new Vector3(currentVel.x, 0, currentVel.z);
            isWalking = horizontalVelocity.magnitude > walkThreshold && IsGrounded();

            animator.SetBool("isFlying", false);
            animator.SetBool("isWalking", isWalking);

            if (isWalking)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        if (flyingVolume != null)
        {
            flyingVolume.weight = isFlying ? 1f : 0f;
        }
    }

    private bool IsGrounded()
    {
        if (groundCheckPoint == null)
            return false;

        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance);
    }
}
