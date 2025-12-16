using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using TMPro;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;
        public TMP_Text textMesh;
        public float lifePackCount = 1;


        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        private void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int) SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, (int) SaveManager.Instance.Setup.health);
        }

        private void Reset()
        {
            foreach(var i in itemSetups)
            {
                i.soInt.Value = 0;
            }
            UpdateUI();
        
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
           return itemSetups.Find(i => i.itemType == itemType);

        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.Value += amount;
            UpdateUI();
            if (lifePackCount == 1)
            {
                lifePackCount = lifePackCount + 1;
                textMesh.text = "Life Pack = L button";
                Invoke(nameof(DeleteText), 3f);
            }
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.Value -= amount;

            if(item.soInt.Value <0) item.soInt.Value = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

        private void UpdateUI()
        {
            //uiTextCoins.text = coins.value.ToString();
        }

        private void DeleteText()
	    {
		    textMesh.text = "";
	    }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
