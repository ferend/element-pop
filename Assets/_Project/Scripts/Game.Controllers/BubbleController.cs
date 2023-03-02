using System;
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
        
        private Bubble InstantiateBubble(Constants.BubbleColors colors)
        {
            Bubble go = Instantiate(ballPrefabs[(int) colors]);
            go.SetColor(colors);
            //go.gameObject.tag = "Ball";
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
        

    }
}
