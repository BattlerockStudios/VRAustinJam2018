using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject spawnedARObject;

    public GameObject targetPrefab;

    public GameObject target;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        target = Instantiate(targetPrefab);
    }
}
