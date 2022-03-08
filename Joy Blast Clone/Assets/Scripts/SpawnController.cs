using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance;
    
    [SerializeField] private int Count;
    [SerializeField] private BallController BallPrefab;
    [SerializeField] private float SpawnHeight;
    [SerializeField] private float SpawnInterval;
    
    private List<BallController> pooledBalls;
    private List<BallController> spawnedBalls;
    
    private bool clickBlocked;
    private void Awake()
    {
        Instance = this;
        Pool();
    }
    
    private void Start()
    {
        SpawnAvailableBalls();
    }
    
    private void SpawnAvailableBalls()
    {
        StartCoroutine(SpawnRoutine());

        IEnumerator SpawnRoutine()
        {
            clickBlocked = true;
            while (pooledBalls.Count > 0)
            {
                SpawnBall();
                yield return new WaitForSeconds(SpawnInterval);
            }
            clickBlocked = false;
        }
    }
    
    private void Pool()
    {
        pooledBalls = new List<BallController>();
        spawnedBalls = new List<BallController>();
        
        for (int i = 0; i < Count; i++)
        {
            BallController spawnedBall = InstantiateBall();
            spawnedBall.gameObject.SetActive(false);
            pooledBalls.Add(spawnedBall);
        }
    }
    
    private void SpawnBall()
    {
        BallController ball = pooledBalls[0];

        pooledBalls.Remove(ball);
        spawnedBalls.Add(ball);

        int randomColor = Random.Range(0, 6);
        
        string colorString;
        switch (randomColor)
        {
            case 0:
                colorString = "red";
                break;
            case 1:
                colorString = "green";
                break;
            case 3:
                colorString = "blue";
                break;
            case 4:
                colorString = "yellow";
                break;
            case 5:
                colorString = "cyan";
                break;
            case 6:
                colorString = "magenta";
                break;
            default:
                colorString = "white";
                break;
        }
        
        ball.Spawn(colorString);
        ball.transform.position = Vector3.up * SpawnHeight + Vector3.right * Random.Range(-2.5f, 2.5f);
        ball.gameObject.SetActive(true);
    }
    
    private BallController InstantiateBall()
    {
        BallController ballPrefab = Instantiate(BallPrefab, transform);
        return ballPrefab;
    }
    
    public void PoolBall(BallController spawnedBall)
    {
        spawnedBalls.Remove(spawnedBall);
        pooledBalls.Add(spawnedBall);
        spawnedBall.gameObject.SetActive(false);
    }
    
    public List<BallController> GetSpawnedBalls()
    {
        return spawnedBalls;
    }
    
    public void CheckScore(BallController clickedBall)
    {
        if(clickBlocked) return;
        
        List<BallController> detectedBalls = ScoreDetector.Detect(clickedBall);
        if(detectedBalls.Count < 2) return;
        
        foreach (var ball in detectedBalls)
        {
            ball.Pool();
        }
        
        SpawnAvailableBalls();
    }
}