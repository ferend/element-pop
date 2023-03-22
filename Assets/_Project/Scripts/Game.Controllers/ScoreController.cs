using TMPro;
using UnityEngine;

namespace Game.Controllers
{
    public class ScoreController
    {
        private static int _score = 0;

        public static void IncreaseScore(int amount)
        {
            _score += amount;
        }

        public static void SetScoreText(TextMeshProUGUI text)
        {
            text.text = $"Score: {_score}";
        }
    }
}
