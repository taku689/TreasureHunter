using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Presenter;
using TreasureHunter.Service;
using TreasureHunter.Model;
using TreasureHunter.Application;

namespace TreasureHunter
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObjectManager GameObjectManager;
        [SerializeField] private UIManager UIManager;

        private MainFsm _mainFsm;

        void Start()
        {
            Locator.CreateInstance();
            Locator.BuildMembers(this, GameObjectManager, UIManager);
            _mainFsm = new MainFsm();
        }

        void Update()
        {
            _mainFsm.Update();
        }

        public void ToTile()
        {
            Debug.Log("To Title");
        }

        public void ToRetry()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("main");
        }

    }
}