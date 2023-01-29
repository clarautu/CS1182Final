using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Item
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates an Item
    /// </summary>
    [Serializable]
    public abstract class Item {
        #region Class Level Variables
        private string _Name;
        private int _AffectValue;
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets and sets _Name
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        /// <summary>
        /// Propertty that gets and sets _EffectValue
        /// </summary>
        public int AffectValue {
            get {
                return _AffectValue;
            }
            set {
                _AffectValue = value;
            }
        }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates an Item
        /// </summary>
        /// <param name="name"> Name of Item </param>
        /// <param name="effectValue"> Value of the item's effect </param>
        public Item(string name, int affectValue) {
            Name = name;
            AffectValue = affectValue;
        }
        #endregion Constructors
    }
}
