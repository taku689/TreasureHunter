using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TreasureHunter.ViewModel;

namespace TreasureHunter.View.UI
{
    public class Result : MonoBehaviour
    {
        [SerializeField] Button _titleButton;
        [SerializeField] Button _retryButton;
        [SerializeField] Text _resultText;

        public static readonly string PREFAB_PATH = "Prefabs/UI/Result";

        public static Result Create(ResultViewModel viewModel, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Result>();
            script._Initialize(viewModel);
            return script;
        }

        private void _Initialize(ResultViewModel viewModel)
        {
            _titleButton.onClick.AddListener(viewModel.ToTitle);
            _retryButton.onClick.AddListener(viewModel.ToRetry);
            _resultText.text = viewModel.ResultText;
        }
    }
}
