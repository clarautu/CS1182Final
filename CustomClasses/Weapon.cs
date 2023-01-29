using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Weapon
    /// Autumn Clark
    /// CS 1182
    /// Professor
    /// Class that instantiates a Map
    /// </summary>
    [Serializable]
    public class Weapon : Item, IRepeatable <Weapon> {
        #region Class Level Variables
        private int _AttackSpeedMod;
        #endregion

        #region Properties
        /// <summary>
        /// Property that gets _AttackSpeedMod.
        /// </summary>
        public int AttackSpeedMod {
            get {
                return _AttackSpeedMod;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Weapon
        /// </summary>
        /// <param name="name"> Name of the weapon </param>
        /// <param name="affectValue"> Value of effect </param>
        /// <param name="attackSpeedMod"> How much it decreases speed by </param>
        public Weapon(string name, int affectValue, int attackSpeedMod) : base(name, affectValue) {
            if(attackSpeedMod > 0) {
                //Set through private variable as property is read only
                _AttackSpeedMod = attackSpeedMod;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to create a deep copy
        /// </summary>
        /// <returns> A new Weapon with identical properties </returns>
        public Weapon CreateCopy() {
            return new Weapon(Name, AffectValue, AttackSpeedMod);
        }
        #endregion
    }
}
