using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    [Serializable]
    public class Boss : Monster, IRepeatable<Boss>, ICombat {
        #region Class Level Variables
        private DoorKey _Key;
        #endregion

        #region Properties
        /// <summary>
        /// Property that gets and sets _Keys
        /// </summary>
        public DoorKey Key {
            get {
                return _Key;
            }
            set {
                _Key = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Boss
        /// </summary>
        /// <param name="name"> Name of Boss </param>
        /// <param name="title"> Title of Boss </param>
        /// <param name="hp"> Health points </param>
        /// <param name="attackSpeed"> Attack speed </param>
        /// <param name="mapPosition"> x and y map coordinates </param>
        /// <param name="attackValue"> Attack damage </param>
        /// <param name="key"> DoorKey </param>
        public Boss(string name, string title, int hp, int attackSpeed, int[] mapPosition, int attackValue, DoorKey key) :base(name, title, hp, attackSpeed, mapPosition, attackValue) {
            Key = key;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to create a deep copy
        /// </summary>
        /// <returns> A new Boss with identical traits </returns>
        public Boss CreateCopy() {
            return new Boss(Name, Title, HP, AttackSpeed, MapPosition, AttackValue, Key);
        }
        #endregion
    }
}
