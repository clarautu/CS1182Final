using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Monster : Actor
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a Monster
    /// </summary>
    [Serializable]
    public class Monster : Actor, IRepeatable <Monster>, ICombat {
        #region Class Level Variables
        private int _AttackValue;
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets _AttackValue
        /// </summary>
        /// <remarks> Read only </remarks>
        public int AttackValue {
            get {
                return _AttackValue;
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Overloaded constructor to instantiate a Monster
        /// </summary>
        /// <param name="name"> Name </param>
        /// <param name="title"> Title </param>
        /// <param name="hP"> Health points </param>
        /// <param name="attackSpeed"> Attack speed </param>
        /// <param name="mapPosition"> X and Y coordinate position </param>
        /// <param name="attackValue"> Attack value </param>
        public Monster(string name, string title, int hP, int attackSpeed, int[] mapPosition, int attackValue) : base(name, title, hP, attackSpeed, mapPosition) {
            //Set _AttackValue if it's greater than 0
            //Set through private variable as it shouldn't change, so property is read only
            if(attackValue > 0) {
                _AttackValue = attackValue;
            }
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Method to create a deep copy
        /// </summary>
        /// <returns> A new Monster with identical traits </returns>
        public Monster CreateCopy() {
            return new Monster(Name, Title, HP, AttackSpeed, MapPosition, AttackValue);
        }

        /// <summary>
        /// Method to attack an Actor
        /// </summary>
        /// <param name="actor"> Actor to be attacked </param>
        /// <returns> True if the Actor is still alive </returns>
        public bool Attack(Actor actor) {
            actor.LoseHP(AttackValue);
            return actor.IsAlive;
        }
        #endregion
    }
}
