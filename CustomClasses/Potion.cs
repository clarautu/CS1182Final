using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Potion : Item
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a Potion
    /// </summary>
    [Serializable]
    public class Potion : Item, IRepeatable<Potion> {
        #region Class Level Variables
        private Colors _Color;
        public enum Colors { Red, Blue, White, Black};
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets and sets _Color
        /// </summary>
        public Colors Color {
            get {
                return _Color;
            }
            set {
                _Color = value;
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Potion
        /// </summary>
        /// <param name="name"> Name </param>
        /// <param name="affectValue"> Affect value </param>
        /// <param name="color"> Potion.Colors </param>
        public Potion(string name, int affectValue, Colors color) : base(name, affectValue) {
            Color = color;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Method to create a deep copy
        /// </summary>
        /// <returns> A new Potion with identical traits </returns>
        public Potion CreateCopy() {
            return new Potion(Name, AffectValue, Color);
        }
        #endregion Methods
    }
}
