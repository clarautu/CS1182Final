using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Door
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a Door
    /// </summary>
    [Serializable]
    public class Door : Item {
        #region Class Level Variables
        private string _Code;
        #endregion

        #region Properties
        /// <summary>
        /// Property that gets _Code
        /// </summary>
        /// <remarks> Read Only </remarks>
        public string Code {
            get {
                return _Code;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Door
        /// </summary>
        /// <param name="name"> Name </param>
        /// <param name="affectValue"> Affect value </param>
        /// <param name="code"> Code - must match DoorKey </param>
        public Door(string name, int affectValue, string code) : base(name, affectValue) {
            _Code = code;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that checks if Code matches DoorKey.Code
        /// </summary>
        /// <param name="doorKey"> DoorKey to match to Door </param>
        /// <returns> True if the two codes match </returns>
        public bool DoesMatchKey(DoorKey doorKey) {
            return Code == doorKey.Code;
        }
        #endregion
    }
}
