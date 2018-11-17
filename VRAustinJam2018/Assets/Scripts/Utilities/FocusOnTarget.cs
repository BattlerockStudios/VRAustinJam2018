using UnityEngine;

public class FocusOnTarget : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.LookAt(target, Vector3.up);
    }
}
