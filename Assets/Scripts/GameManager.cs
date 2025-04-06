using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject minionPrefab;

    public static GameManager Instance;

    public int minionCount = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }
    }

    void Start()
    {
        if (minionCount > 0)
        {
            SpawnMinions(minionCount);
        }
    }

    private void SpawnMinions(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var minion = Instantiate(minionPrefab, player.transform.position + Random.insideUnitSphere, Quaternion.identity);
            minion.GetComponent<Minion>().SetTarget(player.transform, 0.25f);
        }
    }
}
