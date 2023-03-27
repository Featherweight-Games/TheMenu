using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{

    public TextMeshProUGUI PointsGained;

    public bool DoublePoints;
    public bool HasteEnabled;
    public Image StarImage;

    public static List<Point> PointsActive = new List<Point>();
    public static Stack<Point> PointsPool = new Stack<Point>();

    void Start()
    {
        transform.position = new Vector3(Random.Range(169, 1200f), 1200f, 0);
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-30f, 30f));
        PointsActive.Add(this);
        StarImage.gameObject.SetActive(DoublePoints);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, HasteEnabled ? -4f : -2f,0));
        if (transform.position.y <= -100f)
        {
            //PointsPool.Push(this);
            Destroy(this.gameObject);
        }
    }

}
