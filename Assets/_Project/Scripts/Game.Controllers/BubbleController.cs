using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Controllers
{
    public class BubbleController : Manager
    {
        [SerializeField] private Bubble[] ballPrefabs;
        [SerializeField] private Transform _pivotTransform;

        private GridController _gridController;
        private GridLayout _gridLayout;

        private int MAX_ROW = 1000;
        private int ADDED_ROW_SIZE = -1;
        private int DEFAULT_ROW = 6;

        public override void Setup()
        {
            base.Setup();
            Bubble.OnBubbleCollision += (bubble, cell) => AssignBulletToGridCell(bubble, cell);
            Bubble.OnBubbleMatch += (bubble) => ExplodeSameColorBall(bubble);
            InitGrid();
        }
        
        private static GridController GridController(int x, int y, float cx,  float cy)
        {
            return new GridController(x,  y, cx, cy);
        }
        
        
        
        private void InitGrid()
        {
            _gridController = GridController(8, 2000, 0.52F, 0.52F);
            ADDED_ROW_SIZE = -1;
            for (int i = 0; i < _gridController.GetGridSizeX(); i++)
            {
                for (int k = MAX_ROW - DEFAULT_ROW; k < MAX_ROW; k++)
                {
                    Bubble bubble = InstantiateBubble(RandomBallColor(0, 8));
                    bubble.transform.SetParent(_pivotTransform);
                    AssignBubbleToGrid(bubble, i,  k);
                    bubble.FixPosition();
                }
            }
        }
        
        private void InitNewRow()
        {
            ADDED_ROW_SIZE++;
            int methodType = _gridController.GetGridSizeX();
            for (int k = MAX_ROW - DEFAULT_ROW - ADDED_ROW_SIZE - 1; k < MAX_ROW - DEFAULT_ROW - ADDED_ROW_SIZE; k++)
            {
                for (int i = 0; i < methodType; i++)
                {
                    Bubble ball = InstantiateBubble(RandomBallColor(0, 8));
                    ball.transform.SetParent(_pivotTransform);
                    ball.transform.SetSiblingIndex(i);
                    
                    AssignBubbleToGrid(ball, i, k);

                    ball.FixPosition();
                }
            }
            

        }
        
        private Bubble InstantiateBubble(Constants.BubbleColors colors)
        {
            Bubble go = Instantiate(ballPrefabs[(int) colors]);
            go.SetColor(colors);
            go.gameObject.tag = "Ball";
            return go;
        }
        
        private Constants.BubbleColors RandomBallColor(int num, int num2)
        {
            return (Constants.BubbleColors) Random.Range(num, num2);
        }
        
        private void AssignBubbleToGrid(Bubble bubble, int x, int z)
        {
            GridCell grid = _gridController.RegisterBall(x,  z, bubble);
            bubble.SetGridPosition(grid);
            bubble.transform.localPosition = bubble.GetGridPosition().position;
        }
        
        public void AssignBulletToGridCell(Bubble bullet, GridCell gridCellClue)
        {
            bullet.transform.SetParent(_pivotTransform);
            GridCell nearestCell = _gridController.FindNearestGridCellWithCell(gridCellClue, bullet.transform.localPosition);
           
            AssignBubbleToGrid(bullet, nearestCell.X, nearestCell.Y);
            // try
            // {
            //     AssignBallTo2DGrid(bullet, nearestCell.X, nearestCell.Z);
            //
            // }
            // catch 
            // {
            //      AssignBallToGrid(bullet, nearestCell3D.X, nearestCell3D.Y, nearestCell3D.Z);
            // }
        }
        
        public void ExplodeSameColorBall(Bubble bubble)
        {
            if (CheckAndExplodeSameColorBall(bubble))
            {
                //UIManager.Instance.SetPlayerScoreText();
                _gridController.RunRecursion(0 ,MAX_ROW-DEFAULT_ROW);
            }
            else
            {
                //shootCount++;
            }
        }
        private bool CheckAndExplodeSameColorBall(Bubble bullet) {
            
            List<GridCell> sameColorBalls = _gridController.GetListBallsSameColor(bullet);
            //List<GridCell> balls = _gridController.GetNeighbors(bullet.GetGridPosition().X, bullet.GetGridPosition().Y, bullet.GetBallColor(), false);
            //List<GridCell> otherNeighbors = new List<GridCell>();
            
            bool isExploded = sameColorBalls.Count >= 2;
            if (isExploded)
            {
                sameColorBalls.Add(bullet.GetGridPosition());
                //ScoreSystem.ScoreCalculator(sameColorBalls.Count, Constants.BallPoints[bullet.GetBallColor()]);
                
                foreach (GridCell cell in sameColorBalls)
                {
                    cell.bubble.BubbleExplodeEffect();
                    cell.bubble = null;
                }
            }

            return isExploded;
        }
        
        
        
    }
}
