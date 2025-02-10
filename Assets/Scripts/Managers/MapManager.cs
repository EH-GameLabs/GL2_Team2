using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Tooltip("MaxExclusive")]
    [SerializeField] private int maxObstaclesPerLine;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject obstaclePrefab;

    [Header("Container")]
    [SerializeField] private Transform mapContainer;

    List<bool> nextLine = new();
    List<bool> currentline = new();
    List<int> pathIndexes = new();
    List<int> tempPathIndexes = new();
    List<int> pathToRemoveIndexes = new();

    bool success;

    Vector3 spawnPoint = new Vector3(0, 0, 0.5f);

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            currentline.Add(true);
            pathIndexes.Add(i);
            nextLine.Add(false);
        }
        SpawnLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnLine();
        }
    }

    void SpawnLine()
    {
        //spawn
        /*Instantiate(linePrefab, spawnPoint, Quaternion.identity);
        RandomizeLine();
        spawnPoint.z += 1;*/
        Instantiate(linePrefab, spawnPoint, Quaternion.identity, mapContainer);
        do
        {
            RandomizeLine();
            CheckLine();
        } while (!success);
        spawnPoint.z += 1;
        for (int i = 0; i < nextLine.Count; i++)
        {
            currentline[i] = nextLine[i];
        }
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
                obstacleIndex = Random.Range(0, 9);
                //print("Index " + obstacleIndex);
            } while (chosenIndexes.Contains(obstacleIndex));
            chosenIndexes.Add(obstacleIndex);
            //Instantiate(obstaclePrefab, spawnPoint + new Vector3(-3.5f + obstacleIndex, 0.5f, 0), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
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
            string output = "";
            foreach (var item in nextLine)
            {
                output += " " + item;
            }
            //print(output);
            output = "";
            foreach (var item in currentline)
            {
                output += " " + item;
            }
            //print(output);

            for (int i = 0; i < nextLine.Count; i++)
            {
                if (!nextLine[i]) Instantiate(obstaclePrefab, spawnPoint + new Vector3(-3.5f + i, 0.5f, 0), Quaternion.identity, mapContainer);
            }
            foreach (var index in pathToRemoveIndexes)
            {
                pathIndexes.Remove(index);
            }
            foreach (var index in tempPathIndexes)
            {
                CheckAdiacenti(index);
            }
        }
    }

    void CheckAdiacenti(int index)
    {
        if (index + 1 < 9 && nextLine[index + 1] && !pathIndexes.Contains(index + 1))
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
}
