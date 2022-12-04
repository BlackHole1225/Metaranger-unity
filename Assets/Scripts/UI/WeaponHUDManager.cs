using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class WeaponHUDManager : MonoBehaviour
    {
        [Tooltip("UI panel containing the layoutGroup for displaying weapon ammo")]
        public RectTransform AmmoPanel;

        [Tooltip("Prefab for displaying weapon ammo")]
        public GameObject AmmoCounterPrefab;

        PlayerWeaponsManager m_PlayerWeaponsManager;
        List<AmmoCounter> m_AmmoCounters = new List<AmmoCounter>();
        int activeWeaponIndex;

        void Start()
        {
            m_PlayerWeaponsManager = FindObjectOfType<PlayerWeaponsManager>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, WeaponHUDManager>(m_PlayerWeaponsManager,
                this);

            WeaponController activeWeapon = m_PlayerWeaponsManager.GetActiveWeapon();
            activeWeaponIndex = m_PlayerWeaponsManager.ActiveWeaponIndex;

            List<WeaponController> startingWeapons = m_PlayerWeaponsManager.StartingWeapons;

            // if (activeWeapon)
            // {
            //     AddWeapon(activeWeapon, m_PlayerWeaponsManager.ActiveWeaponIndex);
            //     ChangeWeapon(activeWeapon);
            // }

            // For some reason, the below isn't working
            // m_PlayerWeaponsManager.OnAddedWeapon += AddWeapon;
            // m_PlayerWeaponsManager.OnRemovedWeapon += RemoveWeapon;
            // m_PlayerWeaponsManager.OnSwitchedToWeapon += ChangeWeapon;
            AddAmmoCounters();
        }

        void Update()
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(AmmoPanel);
        }

        bool ValidateOwnedWeapon(string weaponName)
        {
            if (weaponName == "Blaster") return true;
            string ownString = PlayerPrefs.GetString(weaponName + "BaseOwned");

            if (Boolean.TryParse(ownString, out bool owns))
            {
                return owns;
            }
            else
            {
                return false;
            }
        }

        void AddAmmoCounters()
        {
            List<WeaponController> startingWeapons = m_PlayerWeaponsManager.StartingWeapons;
            int i = 0;
            foreach (var weapon in startingWeapons)
            {
                if (ValidateOwnedWeapon(weapon.WeaponName))
                {
                    AddWeapon(weapon, activeWeaponIndex + i);
                    ChangeWeapon(weapon);
                    i++;
                }

            }
        }

        public void AddWeapon(WeaponController newWeapon, int weaponIndex)
        {
            GameObject ammoCounterInstance = Instantiate(AmmoCounterPrefab, AmmoPanel);
            AmmoCounter newAmmoCounter = ammoCounterInstance.GetComponent<AmmoCounter>();
            DebugUtility.HandleErrorIfNullGetComponent<AmmoCounter, WeaponHUDManager>(newAmmoCounter, this,
                ammoCounterInstance.gameObject);

            newAmmoCounter.Initialize(newWeapon, weaponIndex);

            m_AmmoCounters.Add(newAmmoCounter);
        }

        void RemoveWeapon(WeaponController newWeapon, int weaponIndex)
        {
            int foundCounterIndex = -1;
            for (int i = 0; i < m_AmmoCounters.Count; i++)
            {
                if (m_AmmoCounters[i].WeaponCounterIndex == weaponIndex)
                {
                    foundCounterIndex = i;
                    Destroy(m_AmmoCounters[i].gameObject);
                }
            }

            if (foundCounterIndex >= 0)
            {
                m_AmmoCounters.RemoveAt(foundCounterIndex);
            }
        }

        void ChangeWeapon(WeaponController weapon)
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(AmmoPanel);
        }
    }
}