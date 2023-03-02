using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

namespace Game.Controllers
{
    public class GridController
    {
        private GridCell[,] _bubbleGrid;

        private int _gridSizeX;
        private int _gridSizeY;
        private float _cellSizeX;
        private float _cellSizeY;
        public int GetGridSizeX() => _gridSizeX;
        public int GetGridSizeZ() => _gridSizeY;
        
        public GridController(int gridSizeX,  int gridSizeY, float cellSizeX, float cellSizeY)
        {
            _gridSizeX = gridSizeX;
            _gridSizeY = gridSizeY;
            _cellSizeX = cellSizeX;
            _cellSizeY = cellSizeY;

            _bubbleGrid = new GridCell[_gridSizeX,  _gridSizeY];
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int k = 0; k < _gridSizeY; k++)
                {
                    _bubbleGrid[i, k] = new GridCell(i, k, GridPositionToVectorPosition(i,  k));
                }
            }
            
        }
        
        private Vector3 GridPositionToVectorPosition(int x, int y)
        {
            float pivotTopLeftX = (y % 2 == 0) ? 0 : _cellSizeX / 2.0f;
            float positionX = pivotTopLeftX + x * _cellSizeX + _cellSizeX / 2.0f;
            float positionY = -y * _cellSizeY - _cellSizeY / 2f;
            return new Vector3(positionX, positionY, 0);
        }
        
        public GridCell RegisterBall(int x, int y, Bubble ball)
        {
            _bubbleGrid[x, y].bubble = ball;
            _bubbleGrid[x, y].position = GridPositionToVectorPosition(x,  y);
            return _bubbleGrid[x, y];
        }
        
        public GridCell FindNearestGridCell(GridCell cellClue, Vector3 position)
        {
            var listNeighbors = GetNeighborCells(cellClue.X,  cellClue.Y);
            return FindNearestGridCell(listNeighbors, position);
        }
        

        public List<GridCell> GetNeighborCells(int x, int y)
        {
            var neighbors = new List<GridCell>();

            for (int i = -1; i < 2; i++)
            {
                for (int k = -1; k < 2; k++)
                {
                    if (i == 0 && k == 0) continue;

                    int _x = x + i; 
                    int _y = y + k;
                    try 
                    {
                        neighbors.Add(_bubbleGrid[_x, _y]);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return neighbors;
        }

        private GridCell FindNearestGridCell(List<GridCell> listNeighbors, Vector3 position)
        {
            float smallestDistance = Mathf.Infinity;
            GridCell nearestCell = null;
            foreach (GridCell gridCell in listNeighbors)
            {
                if (gridCell.bubble != null) continue;
                float currentDistance = Vector3.Distance(gridCell.position, position);

                if (currentDistance < smallestDistance)
                {
                    smallestDistance = currentDistance;
                    nearestCell = gridCell;
                }

                gridCell.visited = true;
            }

            return nearestCell;
        }
        
        public List<GridCell> GetNeighbors(int x, int z, Constants.BubbleColors color, bool checkColor)
        {
            List<GridCell> list = new List<GridCell>();
            List<GridCell> neighbors = GetNeighborCells(x,  z);
            foreach (GridCell cell in neighbors)
            {
                if (checkColor)
                {
                    if (IsOccupied(cell.X,  cell.Y) && cell.bubble.GetBallColor() == color)
                    {
                        list.Add(cell);
                    }
                }
                else if (!checkColor)
                {
                    if (IsOccupied(cell.X,  cell.Y))
                    {
                        list.Add(cell);
                    }
                }
            }

            return list;
        }
        
        private bool IsOccupied(int x, int z)
        {
            return _bubbleGrid[x, z].bubble != null;
        }
        
    }
}
