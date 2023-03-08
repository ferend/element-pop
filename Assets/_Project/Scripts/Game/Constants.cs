using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Constants
    {

        public const string LayerWallLine = "WallForAimingLine";
        public const string LayerBubble = "Bubble";
        public const int numToMoveGrid = 3;

        
        public enum BubbleColors
        {
            Blue,
            DarkBlue,
            Red,
            Green,
            Purple,
            Pink,
            Yellow,
            Orange
        }
        public static readonly Dictionary<BubbleColors, Color> ColorCodes = new Dictionary<BubbleColors, Color>()
        {
            [BubbleColors.Blue] = Color.cyan,
            [BubbleColors.DarkBlue] = Color.blue,
            [BubbleColors.Red] = Color.red,
            [BubbleColors.Green] = Color.green,
            [BubbleColors.Purple] = new UnityEngine.Color(99/255f, 0/255f, 255/255f),
            [BubbleColors.Pink] =new UnityEngine.Color(255/255f, 0/255f, 189/255f),
            [BubbleColors.Yellow] = Color.yellow,
            [BubbleColors.Orange] = new UnityEngine.Color(255/255f, 138/255f, 0f/255f)
        };

        #region UI
        public const float defaultTransitionDuration = 0.25f;
        public const float overlayTransitionDuration = 0.5f;
        public const float splashScreenDuration = 2.0f;
        public const float popupOpenDuration = 0.5f;
        public const float popupUnderlayTransitionDuration = 0.5f;
        public const float popupCloseDuration = 0.5f;
        #endregion
    }
}

