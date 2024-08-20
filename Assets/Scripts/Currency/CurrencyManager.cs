using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Currency
{
    public class CurrencyManager : MonoBehaviour
    {
        public UnityEvent<int> MoneyValueChanged;
        [SerializeField] private int _money = 30000;

        private void Start()
        {
            if (PlayerPrefs.HasKey(KeysForSavingData.CURRENCY_KEY))
            {
                _money = PrefsManager.LoadInt(KeysForSavingData.CURRENCY_KEY);
            }
            MoneyValueChanged.Invoke(_money);
        }

        public bool TryBuy(int price)
        {
            if (_money >= price)
            {
                _money -= price;
                PrefsManager.SaveInt(KeysForSavingData.CURRENCY_KEY, _money);
                MoneyValueChanged.Invoke(_money);
                return true;
            }

            return false;
        }
    
    }
}