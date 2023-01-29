using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Hero : Actor
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a Hero
    /// </summary>
    [Serializable]
    public class Hero : Actor, ICombat {
        #region Class Level Variables
        private Weapon _EquippedWeapon = null;
        private bool _IsRunningAway = false;
        private DoorKey _DoorKey = null;
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets _EquippedWeapon
        /// </summary>
        /// <remarks> Read Only </remarks>
        public Weapon EquippedWeapon {
            get {
                return _EquippedWeapon;
            }
        }

        /// <summary>
        /// Property that gets and sets _IsRunningAway
        /// </summary>
        public bool IsRunningAway {
            get {
                return _IsRunningAway;
            }
            set {
                _IsRunningAway = value;
            }
        }

        /// <summary>
        /// Property that gets _DoorKey
        /// </summary>
        /// <remarks> Read Only </remarks>
        public DoorKey DoorKey {
            get {
                return _DoorKey;
            }
        }

        /// <summary>
        /// Property that returns true if Hero has a Weapon stored in _EquipWeapon
        /// </summary>
        /// <remarks> Read Only </remarks>
        public bool HasWeapon {
            get {
                if(EquippedWeapon != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Property that gets the Hero's attack damage
        /// </summary>
        /// <remarks> Read Only </remarks>
        public int AttackDamage {
            get {
                if (HasWeapon == true) {
                    return EquippedWeapon.AffectValue;
                } else {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Property that gets the hero specific AttackSpeed based on whether or not he has a weapon
        /// </summary>
        public override int AttackSpeed {
            get {
                if(HasWeapon == true) {
                    return base.AttackSpeed - EquippedWeapon.AttackSpeedMod;
                } else {
                    return base.AttackSpeed;
                }
            }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Hero
        /// </summary>
        /// <param name="name"> Name </param>
        /// <param name="title"> Title </param>
        /// <param name="hP"> Health points </param>
        /// <param name="attackSpeed"> Attack speed </param>
        /// <param name="mapPosition"> X and Y coordinates </param>
        public Hero(string name, string title, int hP, int attackSpeed, int[] mapPosition) : base(name, title, hP, attackSpeed, mapPosition) {
            
        }
        #endregion Constructors

        #region Methods
       /// <summary>
       /// Method to move a Hero
       /// </summary>
       /// <param name="direction"> Direction to move </param>
        public override void Move(Directions direction) {
            base.Move(direction);
        }

        /// <summary>
        /// Method to attack an Actor
        /// </summary>
        /// <param name="actor"> Actor to be attacked </param>
        /// <returns> True if the Actor is still alive </returns>
        public bool Attack(Actor actor) {
            actor.LoseHP(AttackDamage);
            return actor.IsAlive;
        }

        /// <summary>
        /// Method to appy an Item
        /// </summary>
        /// <param name="item"> Item to apply </param>
        /// <returns> Null if a Potion, the Item if anything else </returns>
        public Item ApplyItem(Item item) {
            //If the Item is a Potion
            if(item.GetType() == typeof(Potion)) {
                GainHP(item.AffectValue);
                return null;
                //If the Item is a Weapon
            } else if(item.GetType() == typeof(Weapon)) {
                Weapon previousWeapon = null;
                //Check to see if there was anything already inside of _EquippedWeapon
                if(EquippedWeapon != null) {
                    previousWeapon = (Weapon)EquippedWeapon.CreateCopy();
                }
                _EquippedWeapon = (Weapon)item;
                return previousWeapon;
                //If the Item is a DoorKey
            } else if(item.GetType() == typeof(DoorKey)) {
                DoorKey previousDoorKey = null;
                //Check to see if there was anything already inside of _DoorKey
                if (DoorKey != null) {
                    previousDoorKey = (DoorKey)DoorKey;
                }
                _DoorKey = (DoorKey)item;
                return previousDoorKey;
            } else {
                return item;
            }
        }
        #endregion Methods

        #region Overloaded Operators
        public static bool operator +(Hero hero, Monster monster) {
            //Variables to catch Attack method outcomes
            bool isMonsterAlive = true;
            bool isHeroAlive = true;
            //If the Hero is running away if statements
            if (hero.IsRunningAway) {
                if (hero.AttackSpeed > monster.AttackSpeed) {
                    //Hero escapes and nothing happens
                } else {
                    //Monster attacks before runs
                    isHeroAlive = monster.Attack(hero);
                }
                //If the Hero is not running away if statements
            } else {
                //Hero attacks first
                if (hero.AttackSpeed > monster.AttackSpeed) {
                    isMonsterAlive = hero.Attack(monster);
                    //Monster attacks if still alive
                    if (isMonsterAlive) {
                        isHeroAlive = monster.Attack(hero);
                    }
                    //Monster attacks first
                } else if (hero.AttackSpeed < monster.AttackSpeed) {
                    isHeroAlive = monster.Attack(hero);
                    //Hero attacks if still alive
                    if (isHeroAlive) {
                        isMonsterAlive = hero.Attack(monster);
                    }
                    //Both attack at the same time
                } else {
                    isMonsterAlive = hero.Attack(monster);
                    isHeroAlive = monster.Attack(hero);
                }
            }
            return isHeroAlive;
        }
        #endregion
    }
}
