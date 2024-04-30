using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class StaticAsteroidSpawner : MonoBehaviour
{
    public List<GameObject> AsteroidListe;
    public int SpawnerScaleX;
    public int SpawnerScaleY;
    public int SpawnerScaleZ;
    public int Margin;
   
    [Range(0,2)]public float SpawnChanceMultiplier;

    private int[,,] _spawnerPosition;
    private Vector3Int _originTableSpawnerPosition;
    private float _marginModifier;
    private int _objectCount = 0;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Handles.Label(transform.position, $"Object count : {_objectCount}");
    }

    // Start is called before the first frame update
    void Start() {
        _spawnerPosition = new int[SpawnerScaleX, SpawnerScaleY, SpawnerScaleZ];
        _originTableSpawnerPosition.x = (int)gameObject.transform.position.x - SpawnerScaleX / 2 * Margin;
        _originTableSpawnerPosition.y = (int)gameObject.transform.position.y - SpawnerScaleY / 2 * Margin;
        _originTableSpawnerPosition.z = (int)gameObject.transform.position.z - SpawnerScaleZ / 2 * Margin;
        InitPositionsToSpawn();
        SpawnAstéroid();
    }
    public void InitPositionsToSpawn() {
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++) {
                for (int k = 0; k < _spawnerPosition.GetLength(2); k++) {
                    int tmp = Random.Range(0, AsteroidListe.Count);
                    if (tmp < AsteroidListe.Count && Random.Range(1, 100) <= (SpawnChanceInSpawnerposition(i,j,k))*SpawnChanceMultiplier) {
                        _spawnerPosition[i, j, k] = tmp;
                    }
                    else
                    {
                        _spawnerPosition[i, j, k] = AsteroidListe.Count;
                    }
                }
            }
        }
    }
    public void SpawnAstéroid() {
        for (int i = 0; i < _spawnerPosition.GetLength(0); i++) {
            for (int j = 0; j < _spawnerPosition.GetLength(1); j++) {
                for (int k = 0; k < _spawnerPosition.GetLength(2); k++)
                {
                    _marginModifier = Random.Range(0f, 2f);
                    if (_spawnerPosition[i, j, k] != AsteroidListe.Count)
                    {
                        GameObject x = Instantiate(AsteroidListe[_spawnerPosition[i, j, k]]);
                        Vector3 transformPosition = x.transform.position;
                        transformPosition.x = _originTableSpawnerPosition.x + i  * (Margin*_marginModifier);
                        transformPosition.y = _originTableSpawnerPosition.y + j * (Margin*_marginModifier);
                        transformPosition.z = _originTableSpawnerPosition.z + k * (Margin*_marginModifier);
                        x.transform.position = transformPosition;
                        Vector3 randomRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360),
                            Random.Range(0, 360));
                        x.transform.Rotate(randomRot);
                        _objectCount++;
                    }
                }
                
            }
        }
    }
    public int SpawnChanceByIndex(int index, int scale) {
        int midle = scale / 2;
        if (index < midle) return index * 100 / midle;
        return (index - midle) * 100 / midle;
    }
    public int SpawnChanceInSpawnerposition(int Xindex, int Yindex,int Zindex) {
        return (SpawnChanceByIndex(Xindex, SpawnerScaleX) + SpawnChanceByIndex(Yindex, SpawnerScaleY) +
               SpawnChanceByIndex(Zindex, SpawnerScaleZ) )/ 3;

    }
}
