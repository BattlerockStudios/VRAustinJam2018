using UnityEngine;

public class FocusOnTarget : MonoBehaviour
{
    public Transform target;

    public bool canUseXAxis = true;
    public bool canUseYAxis = true;
    public bool canUseZAxis = true;

    public Vector3 offset = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            target = GameManager.Instance.target.transform;
        }

        if (offset != Vector3.zero)
        {
            var obj = new GameObject();
            obj.transform.parent = target;
            obj.transform.position = offset;

            target = obj.transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        var adjustedTarget = target.position;

        if (canUseXAxis == false)
        {
            adjustedTarget.x = 0;
        }

        if (canUseYAxis == false)
        {
            adjustedTarget.y = 0;
        }

        if (canUseZAxis == false)
        {
            adjustedTarget.z = 0;
        }

        transform.LookAt(adjustedTarget, Vector3.up);
    }
}
