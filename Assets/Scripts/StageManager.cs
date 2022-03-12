using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float moveSpeed = 10;
    public Vector3 moveDirection = Vector3.left;
    public List<StageConfig> stages;
    public float stageWidth = 20;
    public float difficulty = 0;
    public Vector3 lastOffset = Vector3.zero;

    List<GameObject> stageInstances = new List<GameObject>();



    private void Start()
    {
        GenInitialStages(5);
    }

    private void Update()
    {
        Move(Time.deltaTime);
        CleanAndGen();
    }

    public void GenInitialStages(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GenStage(0);
        }
    }

    public void Move(float dt)
    {
        Vector3 displacement = moveDirection * moveSpeed * dt;
        this.transform.position += displacement;
        lastOffset += displacement;

        if (this.transform.position.x <= -1 * stageWidth)
        {
            this.transform.position -= moveDirection * stageWidth;
            foreach (var stage in stageInstances)
            {
                stage.transform.position += moveDirection * stageWidth;
            }
        }
    }

    public void CleanAndGen()
    {
        while (stageInstances[0].transform.position.x < -2 * stageWidth)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
            GenStage(difficulty);
        }
    }

    public void GenStage(float difficulty)
    {
        List<StageConfig> validStages = stages.Where(
            s => (s.minDifficulty <= difficulty && difficulty <= s.maxDifficulty)).ToList();

        int index = Random.Range(0, validStages.Count);
        GameObject newStage = Instantiate(validStages[index].prefab);
        newStage.transform.position = lastOffset;
        newStage.transform.parent = this.transform;
        lastOffset -= moveDirection * stageWidth;
        stageInstances.Add(newStage);
    }
}


[System.Serializable]
public class StageConfig
{
    public GameObject prefab;
    public float minDifficulty = 0;
    public float maxDifficulty = float.PositiveInfinity;
}
