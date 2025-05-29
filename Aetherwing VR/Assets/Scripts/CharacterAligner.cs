using UnityEngine;

public class CharacterAligner : MonoBehaviour
{
    public Transform xrRig;
    public Transform character;
    public Animator animator;
    public float heightOffset = 0f;

    void LateUpdate()
    {
        bool isFlying = animator.GetBool("isFlying");

        if (!isFlying)
        {
            Vector3 rigPosition = xrRig.position;
            Vector3 newPosition = new Vector3(rigPosition.x, rigPosition.y + heightOffset, rigPosition.z);
            character.position = newPosition;
        }
    }
}
