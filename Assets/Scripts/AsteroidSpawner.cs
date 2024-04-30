using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> AsteroidListe;
    public int SpawnerScale;
    public int Margin;

    private int[,] _spawnerPosition;
    private Vector3Int _originTableSpawnerPosition;
    // Start is called before the first frame update
    void Start()
    {
        _spawnerPosition = new int[SpawnerScale, SpawnerScale];
        _originTableSpawnerPosition.x = (int)gameObject.transform.position.x - SpawnerScale / 2 * Margin;
        _originTableSpawnerPosition.y = (int)gameObject.transform.position.y - SpawnerScale / 2 * Margin;
        _originTableSpawnerPosition.z = (int)gameObject.transform.position.z;
        InitPositionsToSpawn();
        SpawnAstéroid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPositionsToSpawn() {
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++) {
                _spawnerPosition[i, j] = Random.Range(0, AsteroidListe.Count+1);
            }
        }
    }

    public void SpawnAstéroid() {
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++) {
                if (_spawnerPosition[i, j] != AsteroidListe.Count)
                {
                    GameObject x = Instantiate(AsteroidListe[_spawnerPosition[i, j]]);
                    Vector3 transformPosition = x.transform.position;
                    transformPosition.x = _originTableSpawnerPosition.x + i * Margin;
                    transformPosition.y = _originTableSpawnerPosition.y + j * Margin;
                    transformPosition.z = _originTableSpawnerPosition.z;
                    x.transform.position = transformPosition;
                }
            }
        }
    }
}
