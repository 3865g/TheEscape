﻿using Data;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.ChangedValue += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            //Counter.text = $"{_worldData.LootData.Collected}";
        }
    }
}