using Hero;
using HeroSelection;
using Lobby;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeroesManager _heroesManager;
    [SerializeField] private LobbyScreenManager _lobbyScreenManager;
    [SerializeField] private HeroSelectionScreenManager _heroSelectionScreenManager;

    private void Start()
    {
        var heroes = _heroesManager.GetHeroes();
        _lobbyScreenManager.Initialize(heroes);
        _heroSelectionScreenManager.Initialize(heroes);

        _heroSelectionScreenManager.HeroChanged += OnHeroChanged;
        
        DontDestroyOnLoad(_heroesManager);
    }
    
    private void OnHeroChanged(HeroController selectedHero)
    {
        _heroesManager.ActivateSelectedHero(selectedHero);
    }
    
    private void OnDestroy()
    {
        _heroSelectionScreenManager.HeroChanged -= OnHeroChanged;
    }
}