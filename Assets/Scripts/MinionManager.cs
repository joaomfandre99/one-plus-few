using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    [SerializeField] private float whistleRadius = 10f;
    [SerializeField] private Transform target = default;
    [SerializeField] private Material highlightMaterial = default;
    [SerializeField] private Material originalMaterial = default;

    private List<Minion> allMinions= new List<Minion>();
    private int controlledMinions = 0;
    private Minion nearestMinion;

    private float longPressDuration = 0.5f;
    private float keyDownTime;
    private bool keyProcessed = false;

    private void Start()
    {
        allMinions.AddRange(FindObjectsByType<Minion>(FindObjectsSortMode.None));
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressHandler(KeyCode.Q);
        KeyPressHandler(KeyCode.E);
        // HighlightMinions();
    }

    private void AllFollowWhistle()
    {
        
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state != Minion.State.Follow)
            {
                minion.SetTarget(target, 0.25f);
                controlledMinions++;
            }
        }
    }

    private void FollowWhistle()
    {
        var nearestDistance = whistleRadius;
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < nearestDistance && minion.state != Minion.State.Follow)
            {
                nearestMinion = minion;
            }
        }
        nearestMinion.SetTarget(target, 0.25f);
        controlledMinions++;
    }

    private void AllWaitWhistle()
    {
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state != Minion.State.Idle)
            {
                minion.SetIdle();
                controlledMinions--;
            }
        }
    }

    private void WaitWhistle()
    {
        var nearestDistance = whistleRadius;
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < nearestDistance && minion.state != Minion.State.Idle)
            {
                nearestMinion = minion;
            }
        }
        nearestMinion.SetIdle();
        controlledMinions--;
    }

    private void HighlightMinions()
    {
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state == Minion.State.Idle)
            {
                minion.GetComponent<MeshRenderer>().material= highlightMaterial;
            } 
            else
            {
                minion.GetComponent<MeshRenderer>().material = originalMaterial;
            }
        }
    }

    private void KeyPressHandler(KeyCode targetKey)
    {
        if (Input.GetKeyDown(targetKey))
        {
            keyDownTime = Time.time;
            keyProcessed = false;
        }

        if (Input.GetKeyUp(targetKey))
        {
            if (!keyProcessed)
            {
                OnTap(targetKey);
            }
        }

        if (Input.GetKey(targetKey) && !keyProcessed)
        {
            float pressDuration = Time.time - keyDownTime;

            if (pressDuration >= longPressDuration)
            {
                OnLongPress(targetKey);
                keyProcessed = true;
            }
        }
    }

    private void OnLongPress(KeyCode targetKey)
    {
        if (targetKey == KeyCode.Q)
        {
            AllFollowWhistle();
        }
        else if (targetKey == KeyCode.E)
        {
            AllWaitWhistle();
        }
    }
    private void OnTap(KeyCode targetKey)
    {
        if (targetKey == KeyCode.Q)
        {
            FollowWhistle();
        }
        else if (targetKey == KeyCode.E)
        {
            WaitWhistle();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, whistleRadius);
    }
}
