using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.View.UI;
using TreasureHunter.ViewModel;
using UnityEngine.Assertions;
using System;

namespace TreasureHunter.Presenter
{
    public class BoardUIPresenter
    {
        private Dictionary<long, Life> _IdToLifeDict = new Dictionary<long, Life>();
        public void Initialize(PlayerViewModel viewModel, Transform lifeParent)
        {
            _SetLifes(viewModel.LifeIdToPosDict, lifeParent, viewModel.IsEnemy);
        }

        private void _SetLifes(Dictionary<long, Vector3> idToPosDict, Transform lifeParent, bool isEnemy)
        {
            foreach (var val in idToPosDict)
            {
                _SetLife(val.Key, val.Value, isEnemy, lifeParent);
            }
        }

        private void _SetLife(long id, Vector3 worldPos, bool isEnemy, Transform lifeParent)
        {
            var pos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
            var life = Life.Create(pos, isEnemy, lifeParent);
            _IdToLifeDict.Add(id, life);
        }

        public void DisappearLife(long id)
        {
            var life = _IdToLifeDict[id];
            life.Dispose();
            _IdToLifeDict.Remove(id);
        }
    }
}