using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Managers
{
    public class ShopManager : SingeltonMonoBehaviour<ShopManager>
    {
        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private List<Clothes> _availableClothes;
        private Dictionary<Clothes, UI_ShopBlock> _availableMap = new Dictionary<Clothes, UI_ShopBlock>();
        [SerializeField] private List<Clothes> _allClothes;
        [SerializeField] private List<Clothes> _clothesToBuy = new List<Clothes>();
        [SerializeField] private List<Clothes> _equippedClothes = new List<Clothes>();
        [SerializeField] private UI_ShopBlock _blockPrefab;
        [SerializeField] private Transform _blockParent;
        [SerializeField] private Image _previewHat;
        [SerializeField] private Image _previewClothe;
        [SerializeField] private GameObject _buyButton;
        [SerializeField] private GameObject _sellButton;
        [SerializeField] private GameObject _equipButton;

        private Clothes _currentEquippedClothe;
        private Clothes _currentEquippedHat;

        private int _totalPrice;
        [SerializeField] private TextMeshProUGUI _totalPriceText;
        [SerializeField] private TextMeshProUGUI _playerCoins;

        private PlayerData _playerData;

        protected override void Awake()
        {
            base.Awake();
            _previewClothe.enabled = false;
            _previewHat.enabled = false;
            foreach (var clothe in _availableClothes)
            {
                var block = Instantiate(_blockPrefab, _blockParent);
                block.OnBuyButtonPressed += UpdatePreview;
                block.OnSellButtonPressed += SellItem;
                block.UpdateBlock(clothe);
                _availableMap.Add(clothe, block);
            }
        }

        public void InitializePlayerData(PlayerData playerData)
        {
            _playerData = playerData;
            _playerCoins.text = _playerData.Coins.ToString();
            foreach (var playerClothe in _playerData.GetClothes())
            {
                if (_availableMap.TryGetValue(playerClothe, out var block))
                    block.Sold();
            }

            _availableClothes = _availableClothes.Where(clothe => !_playerData.GetClothes().Contains(clothe)).ToList();
            foreach (var x in _playerData.GetCurrentClothes())
            {
                EquipClotheItem(x, true);
            }
        }

        private void UpdateTotalPrice(int newTotal)
        {
            _totalPrice = newTotal;
            _totalPriceText.text = _totalPrice.ToString();
        }

        public void BuyItem()
        {
            if (_playerData.Coins >= _totalPrice)
            {
                _playerData.UpdateCoins(-_totalPrice);
                _playerCoins.text = _playerData.Coins.ToString();
                _availableClothes = _availableClothes.Where(clothe => !_clothesToBuy.Contains(clothe)).ToList();
                foreach (var x in _clothesToBuy)
                {
                    _playerData.AddClothe(x);
                    if (_availableMap.TryGetValue(x, out var block))
                        block.Sold();
                }

                Debug.Log("Items Bought");
                _clothesToBuy.Clear();
                UpdateTotalPrice(0);
            }
            else
            {
                Debug.Log("Not enough coins");
            }
        }

        private void SellItem(Clothes clothe)
        {
            _availableClothes.Add(clothe);
            _playerData.UpdateCoins(Mathf.CeilToInt(clothe.price * 0.8f));
            _playerCoins.text = _playerData.Coins.ToString();
            _playerData.Remove(clothe);
            EquipClotheItem(clothe, true);
            UpdateTotalPriceByList(_clothesToBuy);
        }

        private void UpdateTotalPriceByList(List<Clothes> clothesList)
        {
            var aux = 0;
            foreach (var clothes in clothesList)
            {
                aux += clothes.price;
                UpdateTotalPrice(aux);
            }
        }

        public void OpenShop()
        {
            _shopPanel.SetActive(true);
            _playerCoins.text = _playerData.Coins.ToString();
            ScreenManager.Instance.Pause(true);
        }

        public void CloseShop()
        {
            var clothe = _equippedClothes.Find(x => x.id < 10);
            _playerData.ChangeCloth(_playerData.GetClothes().Contains(clothe) ? clothe : null);

            var hat = _equippedClothes.Find(x => x.id > 10);
            _playerData.ChangeHat(_playerData.GetClothes().Contains(hat) ? hat : null);
            _shopPanel.SetActive(false);
            ScreenManager.Instance.Resume();
        }

        #region ClotheItemsMethods

        private bool IsEquipped(int id)
        {
            switch (id)
            {
                case > 10:
                    if (!_currentEquippedHat) return false;
                    return _currentEquippedHat.id == id;
                case < 10:
                    if (!_currentEquippedClothe) return false;
                    return _currentEquippedClothe.id == id;
                default:
                    return false;
            }
        }

        private void EquipClotheItem(Clothes clothe, bool equip)
        {
            switch (clothe.id)
            {
                case > 10:
                    EquipHat(clothe, equip);
                    break;
                case < 10:
                    EquipClothe(clothe, equip);
                    break;
            }
        }

        void EquipItem(Clothes item, bool equip)
        {
            if (equip)
            {
                if (_availableClothes.Contains(item) && item == _currentEquippedHat)
                {
                    _clothesToBuy.Remove(_currentEquippedHat);
                    _equippedClothes.Remove(_currentEquippedHat);
                }
                else if (_availableClothes.Contains(item) && item == _currentEquippedClothe)
                {
                    _clothesToBuy.Remove(_currentEquippedClothe);
                    _equippedClothes.Remove(_currentEquippedClothe);
                }

                switch (item.id)
                {
                    case > 10:
                        _clothesToBuy.Remove(_currentEquippedHat);
                        _equippedClothes.Remove(_currentEquippedHat);
                        _currentEquippedHat = item;
                        break;
                    case < 10:
                        _equippedClothes.Remove(_currentEquippedClothe);
                        _clothesToBuy.Remove(_currentEquippedClothe);
                        _currentEquippedClothe = item;
                        break;
                }

                if (_availableClothes.Contains(item))
                {
                    _clothesToBuy.Add(item);
                }

                _equippedClothes.Add(item);
            }
            else
            {
                switch (item.id)
                {
                    case > 10:
                        _currentEquippedHat = null;
                        break;
                    case < 10:
                        _currentEquippedClothe = null;
                        break;
                }

                if (_availableClothes.Contains(item))
                {
                    _clothesToBuy.Remove(item);
                }

                _equippedClothes.Remove(item);
            }
        }

        private void EquipHat(Clothes hat, bool equip)
        {
            _previewHat.sprite = hat.weareable;
            _previewHat.enabled = equip;
            EquipItem(hat, equip);
        }

        private void EquipClothe(Clothes clothe, bool equip)
        {
            _previewClothe.sprite = clothe.weareable;
            _previewClothe.enabled = equip;
            EquipItem(clothe, equip);
        }

        private void UpdatePreview(int id)
        {
            var clothe = _allClothes.Find((x) => x.id == id);
            EquipClotheItem(clothe, !IsEquipped(id));
            if (_clothesToBuy.Count == 0)
            {
                UpdateTotalPrice(0);
            }
            else
            {
                UpdateTotalPriceByList(_clothesToBuy);
            }
        }

        #endregion
    }
}