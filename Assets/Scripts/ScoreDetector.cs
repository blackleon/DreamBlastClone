using System.Collections.Generic;
using UnityEngine;

public static class ScoreDetector
{
    public static List<BallController> Detect(BallController clickedBall)
    {
        List<BallController> spawnedBalls = SpawnController.Instance.GetSpawnedBalls();
        
        List<BallController> foundBalls = new List<BallController> {clickedBall};
        List<BallController> ballsFoundInThisLoop = new List<BallController> {clickedBall};
        
        while (ballsFoundInThisLoop.Count > 0)
        {
            ballsFoundInThisLoop = new List<BallController>();
            
            foreach (var foundBall in foundBalls)
            {
                foreach (var ball in spawnedBalls)
                {
                    if(foundBalls.Contains(ball))
                        continue;
                    if(ballsFoundInThisLoop.Contains(ball))
                        continue;
                    if(ball.color != foundBall.color)
                        continue;
                    if (Vector3.Distance(ball.transform.position, foundBall.transform.position) > ball.transform.localScale.x * 0.25f)
                        continue;
                    
                    ballsFoundInThisLoop.Add(ball);
                }
            }
            
            foundBalls.AddRange(ballsFoundInThisLoop);
        }
        
        return foundBalls;
    }
}