using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.View;

namespace TreasureHunter.Presenter
{
    public class BasePresenter
    {
        private const int Z_MIN = -5;
        private const int X_MIN = -5;
        private const int Z_MAX = 5;
        private const int X_MAX = 5;
        private const float Y_OFFSET = -0.5f;

        public void Initialize(Transform parent)
        {
            for (var z = Z_MIN; z <= Z_MAX; z++)
            {
                for (var x = X_MIN; x <= X_MAX; x++)
                {
                    Base.Create(new Vector3(x, Y_OFFSET, z), _GetMaterialType(x, z), parent);
                }
            }
        }

        private Base.MaterialType _GetMaterialType(int x, int z)
        {
            var val = x + z;
            if (val % 2 == 0) return Base.MaterialType.A;
            return Base.MaterialType.B;
        }

    }
}