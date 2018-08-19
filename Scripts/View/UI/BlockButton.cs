using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace TreasureHunter.View.UI
{
    public class BlockButton : MonoBehaviour
    {
        private const float ON_DRAG_BLOCK_Y_POS_DIFF = 150;
        private float _onDragBlockYPosDiff = ON_DRAG_BLOCK_Y_POS_DIFF;
        [SerializeField] private DragUI _dragUI;
        private Block _block = null;
        //GC対応用にプールしている
        private Vector3 _moveDiff = new Vector3();
        private long _id;
        Func<Vector2, long, bool> _onDragEnd;

        public void Initialize(bool isEnemy)
        {
            if (isEnemy)
            {
                _onDragBlockYPosDiff = -_onDragBlockYPosDiff;
            }
            _dragUI.Initialize(OnPointerDown, OnPointerUp, OnDrag);
        }

        public void UpdateBlock(List<Vector3> blockPositions, long id, Func<Vector2, long, bool> onDragEnd)
        {
            var block = Block.Create(blockPositions, transform);
            if (_block != null) _block.Dispose();
            _block = block;
            _onDragEnd = onDragEnd;
            _id = id;
        }

        public void OnPointerDown(PointerEventData data)
        {
            if (!_Validate()) return;
            _moveDiff.Set(0.0f, _onDragBlockYPosDiff, 0.0f);
            _block.MoveBlock(_moveDiff);
            _block.MoveBlockBegin();
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (!_Validate()) return;
            _onDragEnd(_block.GetBasePosition(), _id);
            _block.MoveBlockEnd();
        }

        public void OnDrag(PointerEventData data)
        {
            if (!_Validate()) return;
            _moveDiff.Set(data.delta.x, data.delta.y, 0.0f);
            _block.MoveBlock(_moveDiff);
        }

        private bool _Validate()
        {
            if (_block == null) return false;
            return true;
        }
    }
}