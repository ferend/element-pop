using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

namespace Game.Controllers
{
    public struct GridController
    {
        public GridCell[,] _bubbleGrid;

        private int _gridSizeX;
        private int _gridSizeY;
        private float _cellSizeX;
        private float _cellSizeY;
        public int GetGridSizeX() => _gridSizeX;
        public int GetGridSizeY() => _gridSizeY;
        
        public GridController(int gridSizeX,  int gridSizeY, float cellSizeX, float cellSizeY)
        {
            _gridSizeX = gridSizeX;
            _gridSizeY = gridSizeY;
            _cellSizeX = cellSizeX;
            _cellSizeY = cellSizeY;

            _bubbleGrid = new GridCell[_gridSizeX,  _gridSizeY];
            orphanBallList = new List<Bubble>();
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
        
        public GridCell FindNearestGridCellWithCell(GridCell cellType, Vector3 position)
        {
            var listNeighbors = GetNeighborCells(cellType.X,  cellType.Y);
            return FindNearestGridCellInList(listNeighbors, position);
        }
        
        private GridCell FindNearestGridCellInList(List<GridCell> listNeighbors, Vector3 position)
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
            }

            return nearestCell;
        }
        
        public List<GridCell> GetListBallsSameColor(Bubble bullet)
        {
            List<GridCell> sameColors = new List<GridCell>();
            List<GridCell> neighbors = GetNeighbors(bullet.GetGridPosition().X, bullet.GetGridPosition().Y, bullet.GetBallColor(), true);
            GridCell mainCell = bullet.GetGridPosition();
            do
            {
                List<GridCell> listTemp = new List<GridCell>();
                foreach (GridCell cell in neighbors)
                {
                    List<GridCell> list = GetNeighbors(cell.X,  cell.Y, mainCell.bubble.GetBallColor(), true);
                    list = list.FindAll(c => !neighbors.Contains(c));
                    list = list.FindAll(c => !listTemp.Contains(c));
                    list = list.FindAll(c => !sameColors.Contains(c));
                    if (list.Contains(mainCell))
                        list.Remove(mainCell);
                    listTemp.AddRange(list);
                }

                sameColors.AddRange(neighbors);
                neighbors = listTemp;
            } while (neighbors.Count >= 1);

            return sameColors;
        }
        
        public GridCell GetGridCell(int x,  int z)
        {
            try
            {
                return _bubbleGrid[x, z];

            }
            catch
            {
                return null;
            }
        }
        
        public void RunRecursion(int i, int j, MonoBehaviour mono)
        {
            Recursion(i,j); 
            RemoveOrphans(mono);
        }
        
        public void Recursion(int i, int j)
        {
            GridCell gridcell = GetGridCell(i, j);
            if (gridcell== null || gridcell.visited || gridcell.bubble == null ) return;
        
            gridcell.visited = true;
  
            for ( int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if ( x == 0 && y == 0) continue;
                    try
                    {
                        Recursion(i+x,j+y);
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
        }
        
        private List<Bubble> orphanBallList;

        public void RemoveOrphans(MonoBehaviour mono)
        {
            GridCell[,] grid = _bubbleGrid;
            for (int i = 0; i <_gridSizeX; i++)
            {
                for (int j = 0; j <_gridSizeY; j++)
                {
                    if (!grid[i, j].visited)
                    {
                        if (grid[i, j].bubble != null)
                        {                            
                            orphanBallList.Add(grid[i, j].bubble);
                            mono.StartCoroutine(grid[i, j].bubble.BubbleExplodeEffect());
                        }

                    }
                    grid[i, j].visited = false;
                }

            }
            orphanBallList.Clear();

        }

    }
}
