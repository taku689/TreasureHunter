using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Presenter;

namespace TreasureHunter
{
    public class Locator
    {
        public static Locator instance { get; private set; }

        public static void CreateInstance()
        {
            if (instance != null)
            {
                return;
            }

            instance = new Locator();
        }

        public static void DisposeInstance()
        {
            if (instance == null)
            {
                return;
            }

            instance.Dispose();
            instance = null;
        }

        public GameManager _gameManager;
        public CellManager _cellManager;
        public PlayerManager _playerManager;
        public TurnManager _turnManager;
        public InGamePresenter _inGamePresenter;
        public OutGamePresenter _outGamePresenter;

        public static GameManager GameManager
        {
            get { return instance._gameManager; }
        }

        public static CellManager CellManager
        {
            get { return instance._cellManager; }
        }

        public static PlayerManager PlayerManager
        {
            get { return instance._playerManager; }
        }

        public static TurnManager TurnManager
        {
            get { return instance._turnManager; }
        }


        public static InGamePresenter InGamePresenter
        {
            get { return instance._inGamePresenter; }
        }

        public static OutGamePresenter OutGamePresenter
        {
            get { return instance._outGamePresenter; }
        }


        public static void BuildMembers(GameManager gameManager, GameObjectManager gameObjectManager, UIManager uiManager)
        {
            instance._gameManager = gameManager;
            instance._cellManager = new CellManager();
            instance._playerManager = new PlayerManager();
            instance._turnManager = new TurnManager();
            instance._inGamePresenter = new InGamePresenter(gameObjectManager);
            instance._outGamePresenter = new OutGamePresenter(uiManager);
        }

        public void Dispose()
        {

        }

    }
}