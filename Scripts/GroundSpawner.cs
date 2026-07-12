using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groundTile;
    [SerializeField] private GameObject gatePrefab;

    private Vector3 nextSpawnPoint;

    [SerializeField] private int gateInterval = 10;
    private int tileCount = 0;

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);

        nextSpawnPoint = temp.transform.GetChild(1).position;

        GroundTile tile = temp.GetComponent<GroundTile>();

        tileCount++;

        bool spawnGate = (tileCount % gateInterval == 0);

        if (spawnGate)
        {
            Debug.Log("SPAWNING GATE");

            Instantiate(
                gatePrefab,
                new Vector3(0, 2, nextSpawnPoint.z),
                Quaternion.identity
            );
        }

        if (spawnItems && !spawnGate)
        {
            tile.SpawnObstacle();
            tile.SpawnCoins();
        }
    }
    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnTile(i >= 3);
        }
    }
}