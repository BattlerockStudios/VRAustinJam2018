using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject spawnedARObject;

    public GameObject targetPrefab;

    public GameObject target;

    public GameObject environment;

    public int points = 0;

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        if (target == null)
        {
            target = Instantiate(targetPrefab);
        }
    }
}
