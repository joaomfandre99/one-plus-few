using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject minionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.minionCount > 0)
        {
            SpawnMinions(GameManager.Instance.minionCount);
        }
    }

    private void SpawnMinions(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var minion = Instantiate(minionPrefab, transform.position + Random.insideUnitSphere, Quaternion.identity);
            minion.GetComponent<Minion>().SetTarget(player.transform, 0.25f);
        }
    }
}
