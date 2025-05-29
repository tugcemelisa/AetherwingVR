
using UnityEngine;

public class CharacterAligner : MonoBehaviour
{
    public Transform xrRig;
    public Transform character;
    public float heightOffset = 0f;

    void LateUpdate()
    {
        Vector3 rigPosition = xrRig.position;
        character.position = new Vector3(rigPosition.x, rigPosition.y + heightOffset, rigPosition.z);
    }
}