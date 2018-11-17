using UnityEngine;

public class AutoConnectJoint : MonoBehaviour
{
    void Awake()
    {
        GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }
}