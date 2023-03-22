using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entity
{
    public class GridCell  {

        // Create gridcell to be added to grid itself on x and z axis
        public int X;
        public int Y;
        public Vector3 position;
        public Bubble bubble;
        public bool visited;
        
        public GridCell (int gridX, int gridY, Vector3 realPos){
            X = gridX;
            Y = gridY;
            position = realPos;
            bubble = null;
            visited = false;
        }
    }
}
