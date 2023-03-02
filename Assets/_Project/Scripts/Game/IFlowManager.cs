using System;

namespace Game
{
    public interface IFlowManager
    {
        event Action OnGameOver;
        event Action OnGameStart;
        event Action OnGameFinish;
        event Action OnGameRestart;


        void GameStart();
        void GameFinish();
        void GameOver();
        void SetPaused(bool isPaused);
    }
}
