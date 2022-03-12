using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float moveSpeed = 10;
    public Vector3 moveDirection = Vector3.left;
    public List<GameObject> stages;
    public float stageWidth = 20;

    public Vector3 lastOffset = Vector3.zero;

    List<GameObject> stageInstances = new List<GameObject>();



    private void Start()
    {
        GenStages(5);
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
        CleanOldStage();
    }

    public void GenStages(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GenNext();
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
            GenNext();
        }
    }

    public void CleanOldStage()
    {
        while (stageInstances[0].transform.position.x < -2 * stageWidth)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
        }
    }

    public void GenNext()
    {
        int index = Random.Range(0, stages.Count);
        GameObject newStage = Instantiate(stages[index]);
        newStage.transform.position = lastOffset;
        newStage.transform.parent = this.transform;
        lastOffset -= moveDirection * stageWidth;
        stageInstances.Add(newStage);
    }
}
