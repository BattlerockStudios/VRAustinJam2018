using UnityEngine;

public class AutoConnectJoint : MonoBehaviour
{
    void Awake()
    {
        if (GetComponent<CharacterJoint>().connectedBody == null)
        {
            GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
        }
    }
}