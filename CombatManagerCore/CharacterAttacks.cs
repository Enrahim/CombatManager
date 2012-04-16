﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CombatManager
{



    public class CharacterAttacks : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;



        private ObservableCollection<AttackSet> _MeleeAttacks;
        private ObservableCollection<Attack> _RangedAttacks;

        private List<List<WeaponItem>> _MeleeWeaponSets;
        private ObservableCollection<WeaponItem> _RangedWeapons;
        private ObservableCollection<WeaponItem> _NaturalAttacks;

        private int _Hands;

        
        public CharacterAttacks()
        {
            _MeleeAttacks = new ObservableCollection<AttackSet>();


        }

        public CharacterAttacks(ObservableCollection<AttackSet> melee, ObservableCollection<Attack> ranged)
        {

            SetupAttacks(melee, ranged);
        }


        public CharacterAttacks(Monster stats)
        {
            ObservableCollection<AttackSet> melee = new ObservableCollection<AttackSet>(stats.MeleeAttacks) ;
            ObservableCollection<Attack> ranged = new ObservableCollection<Attack>(stats.RangedAttacks);

            SetupAttacks(melee, ranged);
        }

        public void SetupAttacks(ObservableCollection<AttackSet> melee, ObservableCollection<Attack> ranged)
        {
            MeleeAttacks = new ObservableCollection<AttackSet>();

            foreach (AttackSet set in melee)
            {
                MeleeAttacks.Add((AttackSet)set.Clone());
            }

            RangedAttacks = new ObservableCollection<Attack>();
            foreach (Attack attack in ranged)
            {
                RangedAttacks.Add((Attack)attack.Clone());
            }

            //find melee weapon sets
            MeleeWeaponSets = new List<List<WeaponItem>>();

            NaturalAttacks = new ObservableCollection<WeaponItem>();

            _Hands = 2;
            
            foreach (AttackSet set in MeleeAttacks)
            {
                List<WeaponItem> weapons = new List<WeaponItem>();

                bool main = true;
                //find melee attacks
                foreach (Attack attack in set.WeaponAttacks)
                {
                    if (attack.Weapon != null)
                    {
                        WeaponItem item = new WeaponItem(attack);

                        int count = item.Count;
                        item.Count = 1;
                        for (int i=0; i<count; i++)
                        {
                            WeaponItem newItem = (WeaponItem)item.Clone();

                            if (main)
                            {
                                newItem.MainHand = true;
                                main = false;
                            }

                            weapons.Add(newItem);
                        }
                    }

                    _Hands = Math.Max(_Hands, set.Hands);
                }
                

                if (weapons.Count > 0)
                {
                    MeleeWeaponSets.Add(weapons);
                }

                ObservableCollection<WeaponItem> newAttacks = new ObservableCollection<WeaponItem>();

                //find natural attacks
                foreach (Attack attack in set.NaturalAttacks)
                {
                    if (attack.Weapon != null)
                    {                                                   
                        WeaponItem item = new WeaponItem(attack);

                        newAttacks.Add(item);                        
                    }
                }
                if (newAttacks.Count >= NaturalAttacks.Count)
                {
                    NaturalAttacks = newAttacks;
                }

            }


            //find ranged weapons
            RangedWeapons = new ObservableCollection<WeaponItem>();

            foreach (Attack attack in RangedAttacks)
            {
                if (attack.Weapon != null)
                {
                    WeaponItem item = new WeaponItem(attack);
                    RangedWeapons.Add(item);
                }
            }

        }

        public ObservableCollection<AttackSet> MeleeAttacks
        {
            get { return _MeleeAttacks; }
            set
            {
                if (_MeleeAttacks != value)
                {
                    _MeleeAttacks = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("MeleeAttacks")); }
                }
            }
        }
        public ObservableCollection<Attack> RangedAttacks
        {
            get { return _RangedAttacks; }
            set
            {
                if (_RangedAttacks != value)
                {
                    _RangedAttacks = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("RangedAttacks")); }
                }
            }
        }
        public List<List<WeaponItem>> MeleeWeaponSets
        {
            get { return _MeleeWeaponSets; }
            set
            {
                if (_MeleeWeaponSets != value)
                {
                    _MeleeWeaponSets = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("MeleeWeaponSets")); }
                }
            }
        }
        public ObservableCollection<WeaponItem> RangedWeapons
        {
            get { return _RangedWeapons; }
            set
            {
                if (_RangedWeapons != value)
                {
                    _RangedWeapons = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("RangedWeapons")); }
                }
            }
        }
        public ObservableCollection<WeaponItem> NaturalAttacks
        {
            get { return _NaturalAttacks; }
            set
            {
                if (_NaturalAttacks != value)
                {
                    _NaturalAttacks = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("NaturalAttacks")); }
                }
            }
        }
        public int Hands
        {
            get
            {
                return _Hands;
            }
            set
            {
                if (_Hands != value)
                {
                    _Hands = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Hands")); }
                }
            }
        }

    }
}
