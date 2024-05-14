using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Clothes> _availableClothes;
    [SerializeField] private List<Clothes> _allClothes;
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
    private void Awake()
    {
        _previewClothe.enabled = false;
        _previewHat.enabled = false;
        foreach (var clothe in _availableClothes)
        {
            var block = Instantiate(_blockPrefab, _blockParent);
            block.OnButtonPressed += UpdatePreview;
            block.UpdateBlock(clothe.icon, clothe.clothName, clothe.price, clothe.id);
        }
    }

    void UpdateTotalPrice(int add)
    {
        _totalPrice += add;
        _totalPriceText.text = _totalPrice.ToString();
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

    private void EquipHat(Clothes clothe, bool equip)
    {
        _previewHat.sprite = clothe.weareable;
        _previewHat.enabled = equip;
        if (equip)
        {
            if(_currentEquippedHat)
                UpdateTotalPrice(-_currentEquippedHat.price);
            _currentEquippedHat = clothe;
            UpdateTotalPrice(clothe.price);
        }
        else
        {
            _currentEquippedHat = null;
            UpdateTotalPrice(-clothe.price);
        }
    }

    private void EquipClothe(Clothes clothe, bool equip)
    {
        _previewClothe.sprite = clothe.weareable;
        _previewClothe.enabled = equip;
        if (equip)
        {
            if(_currentEquippedClothe)
                UpdateTotalPrice(-_currentEquippedClothe.price);
            _currentEquippedClothe = clothe;
            UpdateTotalPrice(clothe.price);
        }
        else
        {
            _currentEquippedClothe = null;
            UpdateTotalPrice(-clothe.price);
        }
    }

    private void UpdatePreview(int id)
    {
        var clothe = _allClothes.Find((x) => x.id == id);
        EquipClotheItem(clothe, !IsEquipped(id));
    }

    #endregion
   
}