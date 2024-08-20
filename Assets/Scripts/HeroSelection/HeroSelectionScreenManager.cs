using System;
using System.Collections.Generic;
using Currency;
using DefaultNamespace;
using Hero;
using UnityEngine;

namespace HeroSelection
{
    public class HeroSelectionScreenManager : MonoBehaviour
    {
        public event Action<HeroController> HeroSelected;
        public event Action<HeroController> HeroChanged;
    
        [SerializeField] private CurrencyManager _currencyManager;
        [SerializeField] private HeroSelectionScreenView _heroSelectionScreenView;
        [SerializeField] private HeroSwitcher _heroSwitcher;

        private HeroController _currentHeroController;
        private IReadOnlyList<HeroController> _heroControllers;

        public void Initialize(IReadOnlyList<HeroController> heroControllers)
        {
            _heroControllers = heroControllers;
            _heroSwitcher.Initialize(_heroControllers);
        }
    
        public void OpenScreen(HeroController heroController)
        {
            gameObject.SetActive(true);
            _currentHeroController = heroController;
        
            _heroSelectionScreenView.UpdateHeroSelectionView(_currentHeroController.HeroSettings);

            _heroSwitcher.HeroChanged += OnHeroChanged;
            _heroSwitcher.SetCurrentHero(heroController);
        }

        public void TryBuyHero()
        {
            var price = _currentHeroController.HeroSettings.Price;

            if (_currencyManager.TryBuy(price))
            {
                _currentHeroController.HeroSettings.WasBought = true;
                
                //TODO: my code;
                //PlayerPrefs.SetInt(_currentHeroController.HeroSettings.Name + KeysForSavingData.BOUGHT_KEY, _currentHeroController.HeroSettings.WasBought ? 1 : 0);
                //PlayerPrefs.Save();
                PrefsManager.SaveBool(_currentHeroController.HeroSettings.Name + KeysForSavingData.BOUGHT_KEY, true);
                
                
                
                _heroSelectionScreenView.UpdateHeroSelectionView(_currentHeroController.HeroSettings);
            }
        }

        public void SelectHero()
        {
            foreach (var heroController in _heroControllers)
            {
                heroController.HeroSettings.IsSelected = _currentHeroController == heroController;
                heroController.gameObject.SetActive(heroController.HeroSettings.IsSelected);
                
                //TODO: my code;
                //PlayerPrefs.SetInt(heroController.HeroSettings.Name + KeysForSavingData.HERO_KEY, heroController.HeroSettings.IsSelected ? 1 : 0);
                PrefsManager.SaveBool(heroController.HeroSettings.Name + KeysForSavingData.HERO_KEY, heroController.HeroSettings.IsSelected);
            }
            
            //TODO: my code;
            //PlayerPrefs.Save();
            
            
            
            
            HeroSelected?.Invoke(_currentHeroController);
        }

        public void OnBackButtonClicked()
        {
            foreach (var heroController in _heroControllers)
            {
                heroController.gameObject.SetActive(heroController.HeroSettings.IsSelected);
            }
        }

        private void OnHeroChanged(HeroController currentHeroController)
        {
            _currentHeroController = currentHeroController;
            _heroSelectionScreenView.UpdateHeroSelectionView(_currentHeroController.HeroSettings);
            HeroChanged?.Invoke(currentHeroController);
        }


        private void OnDisable()
        {
            _heroSwitcher.HeroChanged -= OnHeroChanged;
        }
    }
}