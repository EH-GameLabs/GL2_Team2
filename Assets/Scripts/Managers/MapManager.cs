using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeightedElement
{
    public GameObject element;
    [Range(0f, 1f)] public float weight;
}

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [Header("MapManager Info")]
    [Tooltip("CrossyRoad -> 9")]
    [SerializeField] private int mapLength = 9;
    [Tooltip("MaxExclusive")]
    [SerializeField] private int maxObstaclesPerLine;

    [Header("Map Prefabs")]
    [SerializeField] private List<WeightedElement> elements = new List<WeightedElement>();
    [SerializeField] private List<GameObject> obstaclesPrefab;
    [SerializeField] private WeightedElement coin;

    [Header("Container")]
    [SerializeField] private Transform mapContainer;

    List<bool> nextLine = new();
    List<bool> currentline = new();
    List<int> pathIndexes = new();
    List<int> tempPathIndexes = new();
    List<int> pathToRemoveIndexes = new();

    bool success;
    private GameObject lineTmp;

    Vector3 spawnPoint = new Vector3(0, 0, 0.5f);

    private void Start()
    {
        for (int i = 0; i < mapLength; i++)
        {
            currentline.Add(true);
            pathIndexes.Add(i);
            nextLine.Add(false);
        }
        // finché non arrivi al collider -> 
        for (int i = 0; i < 10; i++)
        {
            SpawnLine(ChooseLine());
        }
    }

    private void Update()
    {
        //SpawnLine(ChooseLine());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnLine(ChooseLine());
        }
    }

    public GameObject ChooseLine()
    {
        //return linePrefabs[Random.Range(0, linePrefabs.Count)];
        float randomValue = Random.value; // Valore tra 0 e 1
        float cumulative = 0f;

        foreach (var element in elements)
        {
            cumulative += element.weight;
            if (randomValue <= cumulative)
                return element.element;
        }

        return elements.Last().element; // Fallback
    }

    public void SpawnLine(GameObject line)
    {
        lineTmp = Instantiate(line, spawnPoint, Quaternion.identity, mapContainer);
        if (line.name.Equals("BaseMapLine"))
        {
            do
            {
                RandomizeLine();
                CheckLine();
            } while (!success);
            for (int i = 0; i < nextLine.Count; i++)
            {
                currentline[i] = nextLine[i];
            }
        }
        else
        {
            for (int i = 0; i < mapLength; i++)
            {
                currentline[i] = true;
            }
        }
        spawnPoint.z += 1;
    }

    void RandomizeLine()
    {
        success = false;
        int obstaclesNumber = Random.Range(0, maxObstaclesPerLine);
        //print("N. " + obstaclesNumber);
        List<int> chosenIndexes = new();
        for (int i = 0; i < obstaclesNumber; i++)
        {
            int obstacleIndex;
            do
            {
                obstacleIndex = Random.Range(0, mapLength);
                //print("Index " + obstacleIndex);
            } while (chosenIndexes.Contains(obstacleIndex));
            chosenIndexes.Add(obstacleIndex);
            //Instantiate(obstaclePrefab, spawnPoint + new Vector3(-3.5f + obstacleIndex, 0.5f, 0), Quaternion.identity);
        }
        for (int i = 0; i < mapLength; i++)
        {
            if (chosenIndexes.Contains(i)) nextLine[i] = false; else nextLine[i] = true;
        }
    }

    void CheckLine()
    {
        tempPathIndexes = new();
        pathToRemoveIndexes = new();
        for (int i = 0; i < currentline.Count; i++)
        {
            if (currentline[i] && nextLine[i] && pathIndexes.Contains(i))
            {
                tempPathIndexes.Add(i);
                success = true;
            }
            else
            {
                pathToRemoveIndexes.Add(i);
            }
        }
        if (success)
        {
            for (int i = 0; i < nextLine.Count; i++)
            {
                GameObject _tmp = obstaclesPrefab[Random.Range(0, obstaclesPrefab.Count)];
                if (!nextLine[i]) Instantiate(_tmp, spawnPoint + new Vector3(-3.5f + i, 0, 0), Quaternion.identity, lineTmp.transform);
            }
            foreach (var index in pathToRemoveIndexes)
            {
                pathIndexes.Remove(index);
            }
            foreach (var index in tempPathIndexes)
            {
                CheckAdiacenti(index);
            }
            foreach (var index in pathIndexes)
            {
                bool hasToSpawn = Random.Range(0f, 1f) < coin.weight ? true : false;
                if (hasToSpawn)
                {
                    Instantiate(coin.element, spawnPoint + new Vector3(-3.5f + index, 0, 0), Quaternion.identity, lineTmp.transform);
                }
            }
        }
    }

    void CheckAdiacenti(int index)
    {
        if (index + 1 < mapLength && nextLine[index + 1] && !pathIndexes.Contains(index + 1))
        {
            pathIndexes.Add(index + 1);
            CheckAdiacenti(index + 1);
        }
        if (index - 1 > -1 && nextLine[index - 1] && !pathIndexes.Contains(index - 1))
        {
            pathIndexes.Add(index - 1);
            CheckAdiacenti(index - 1);
        }
    }

    #region RANDOM WEIGHT
    private void OnValidate()
    {
        NormalizeWeights();
    }

    private void NormalizeWeights()
    {
        float total = elements.Sum(e => e.weight);
        if (total == 0) return;

        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].weight = Mathf.Round((elements[i].weight / total) * 100f) / 100f; // Arrotonda a due cifre decimali
        }

        // Assicura che la somma sia esattamente 1 (aggiustando l'ultimo valore)
        float adjustedTotal = elements.Sum(e => e.weight);
        if (adjustedTotal != 1f && elements.Count > 0)
        {
            elements[^1].weight += (1f - adjustedTotal);
        }
    }
    #endregion
}
