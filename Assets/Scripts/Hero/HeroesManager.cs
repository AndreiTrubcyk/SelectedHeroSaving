using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Hero.Settings;
using UnityEngine;

namespace Hero
{
    public class HeroesManager : MonoBehaviour
    {
        public static readonly HeroSettings MaxSettings = new()
        {
            Health = 200,
            Attack = 20,
            Defense = 10,
            Speed = 10
        };

        [SerializeField] private HeroController[] _heroPrefabs;
        [SerializeField] private Transform _heroHolder;


        private readonly List<HeroController> _heroControllers = new();

        private void Awake()
        {
            foreach (var heroPrefab in _heroPrefabs)
            {
                InstantiateHero(heroPrefab);
            }
            
            var hero = _heroControllers.FirstOrDefault(hero => hero.HeroSettings.IsSelected);
            if (hero == null)
            {
                ActivateSelectedHero(_heroControllers[0]);
                _heroControllers[0].HeroSettings.IsSelected = true;
            }
            else
            {
                ActivateSelectedHero(hero);
                hero.gameObject.SetActive(true);
            }
        }
        
        public IReadOnlyList<HeroController> GetHeroes()
        {
            return _heroControllers;
        }
        
        public void ActivateSelectedHero(HeroController hero)
        {
            var selectedHeroName = hero.HeroSettings.Name;
            _heroControllers.FirstOrDefault(heroController => 
                heroController.HeroSettings.Name == selectedHeroName)?.gameObject.SetActive(true);
        }

        private void InstantiateHero(HeroController heroPrefab)
        {
            var heroController = Instantiate(heroPrefab, _heroHolder);
            
            heroController.HeroSettings.IsSelected = PrefsManager.LoadBool(heroController.HeroSettings.Name + KeysForSavingData.HERO_KEY, false);
            heroController.HeroSettings.WasBought = PrefsManager
                .LoadBool(heroController.HeroSettings.Name + KeysForSavingData.BOUGHT_KEY,
                    heroController.HeroSettings.Name == _heroPrefabs[0].HeroSettings.Name);
            
            _heroControllers.Add(heroController);
        }
    }
}