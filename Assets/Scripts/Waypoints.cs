using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private static Waypoints instance = null;
    public static Waypoints self
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Waypoints>();

            return instance;
        }
    }


    [SerializeField] private Transform startPoint = null;
    [SerializeField] private Transform finishPoint = null;
    [SerializeField] private List<Transform> points = null;

    public static int Index
    {
        get => index;
        set
        {
            index = value;

            if (index >= self.points.Count)
                self.isGointToFinishPoint = true;
        }
    }
    private static int index = 0;


    public static bool IsGointToFinishPoint => self.isGointToFinishPoint;
    private bool isGointToFinishPoint = false;


    public static Vector3 StartPosition => self.startPoint.position;
    public static Vector3 FinishPosition => self.finishPoint.position;
    public static Vector3 NextPosition => self.points[Index++].position;
}
