using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace TreasureHunter.View.UI
{
    [RequireComponent(typeof(Image))]
    public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Action<PointerEventData> _onPointerDown;
        private Action<PointerEventData> _onPointerUp;
        private Action<PointerEventData> _onDrag;
        public void Initialize(
            Action<PointerEventData> onPointerDown, Action<PointerEventData> onPointerUp, Action<PointerEventData> onDrag)
        {
            _onPointerDown = onPointerDown;
            _onPointerUp = onPointerUp;
            _onDrag = onDrag;

        }
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            _onPointerDown(pointerEventData);
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            _onPointerUp(pointerEventData);
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            _onDrag(pointerEventData);
        }
    }
}