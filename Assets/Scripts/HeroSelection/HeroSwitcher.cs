using System;
using System.Collections.Generic;
using DefaultNamespace;
using Hero;
using UnityEngine;

namespace HeroSelection
{
    public class HeroSwitcher : MonoBehaviour
    {
        public event Action<HeroController> HeroChanged;

        private IReadOnlyList<HeroController> _heroControllers;
        private HeroController _currentHeroController;
        private int _currentHeroIndex;

        public void Initialize(IReadOnlyList<HeroController> heroControllers)
        {
            _heroControllers = heroControllers;
        }

        public void SetCurrentHero(HeroController heroController)
        {
            _currentHeroController = heroController;
            _currentHeroIndex = GetCurrentHeroIndex(heroController);
        }

        public void GoToPreviousHero()
        {
            ChangeHero(-1);
        }

        public void GoToNextHero()
        {
            ChangeHero(1);
        }

        private void ChangeHero(int direction)
        {
            if (_currentHeroController != null)
            {
                _currentHeroController.gameObject.SetActive(false);
            }

            _currentHeroIndex = (_currentHeroIndex + direction + _heroControllers.Count) % _heroControllers.Count;
            _currentHeroController = _heroControllers[_currentHeroIndex];

            HeroChanged?.Invoke(_currentHeroController);
        }

        private int GetCurrentHeroIndex(HeroController heroController)
        {
            for (var i = 0; i < _heroControllers.Count; i++)
            {
                if (_heroControllers[i] == heroController)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}