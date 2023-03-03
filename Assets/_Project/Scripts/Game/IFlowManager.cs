using System;

namespace Game
{
    public interface IFlowManager
    {
        event Action OnGameOver;
        event Action OnGameStart;
        event Action OnGameRestart;


        void GameStart();
        void GameOver();
        void SetPaused(bool isPaused);
    }
}
