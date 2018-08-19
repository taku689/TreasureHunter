using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.Presenter
{
    public class InGamePresenter
    {
        public BasePresenter BasePresenter { get; private set; }
        public BlockPresenter BlockPresenter { get; private set; }

        private GameObjectManager _gameObjectManager;

        public InGamePresenter(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
            BasePresenter = new BasePresenter();
            BlockPresenter = new BlockPresenter();
        }

        public void Initialize()
        {
            BasePresenter.Initialize(_gameObjectManager.Bases);
            BlockPresenter.Initialze(_gameObjectManager.Blocks);
        }

    }
}
