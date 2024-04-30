using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DynamicAsteroidSpawner : MonoBehaviour
{
    public List<GameObject> AsteroidListe;
    public int SpawnerScaleX;
    public int SpawnerScaleY;
    //public int SpawnerScaleZ;
    public int Margin;
    [Range(0,1)]public float SpawnChanceMultiplier;
    //[Range(1,100)]public float SpawnChance;
    public float TimerSpawn;

    private int[,] _spawnerPosition;
    private Vector3Int _originTableSpawnerPosition;
    private float _marginModifier;
    private int _objectCount = 0;
    private float _currentTimeSpawn;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Handles.Label(transform.position, $"Object count : {_objectCount}");
    }

    // Start is called before the first frame update
    void Start() {
        _currentTimeSpawn = TimerSpawn;
        _spawnerPosition = new int[SpawnerScaleX, SpawnerScaleY];
        InitPositionsToSpawn();
        SpawnAsteroid();
    }

    private void Update() {
        if (_currentTimeSpawn > 0) _currentTimeSpawn -= Time.deltaTime;
        else {
            InitPositionsToSpawn();
            SpawnAsteroid();
            _currentTimeSpawn = TimerSpawn;
        }
        //Debug.Log(_currentTimeSpawn);
    }

    public void InitPositionsToSpawn() {
        Debug.Log("init");
        Vector3 position = gameObject.transform.position;
        _originTableSpawnerPosition.x = (int)position.x - SpawnerScaleX / 2 * Margin;
        _originTableSpawnerPosition.y = (int)position.y - SpawnerScaleY / 2 * Margin;
        _originTableSpawnerPosition.z = (int)position.z ;
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++) {
                int tmp = Random.Range(0, AsteroidListe.Count);
                if (tmp < AsteroidListe.Count && Random.Range(1, 100) <= SpawnChanceInSpawnerposition(i,j)*SpawnChanceMultiplier) {
                    _spawnerPosition[i, j] = tmp;
                }
                else
                {
                    _spawnerPosition[i, j] = AsteroidListe.Count;
                }
            }
        }
    }
    public void SpawnAsteroid() {
        Debug.Log("spawn");
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++)
            {
                _marginModifier =1 ;//Random.Range(0f, 2f);
                if (_spawnerPosition[i, j] != AsteroidListe.Count) {
                    GameObject x = Instantiate(AsteroidListe[_spawnerPosition[i, j]]);
                    Vector3 transformPosition = x.transform.position;
                    transformPosition.x = _originTableSpawnerPosition.x + i * (Margin*_marginModifier);
                    transformPosition.y = _originTableSpawnerPosition.y + j * (Margin*_marginModifier);
                    transformPosition.z = _originTableSpawnerPosition.z ;
                    x.transform.position = transformPosition;
                    Vector3 randomRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360),
                        Random.Range(0, 360));
                    x.transform.Rotate(randomRot);
                    _objectCount++;
                }
            }
        }
    }
    public int SpawnChanceByIndex(int index, int scale) {
        int quart = scale / 4;
        if (index < quart || index > quart*3 ) return 15;
        return 50;
    }
    public int SpawnChanceInSpawnerposition(int Xindex, int Yindex) {
        return (SpawnChanceByIndex(Xindex, SpawnerScaleX) + SpawnChanceByIndex(Yindex, SpawnerScaleY) )/ 2;
    }
}
