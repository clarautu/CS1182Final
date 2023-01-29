using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - DoorKey
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a DoorKey
    /// </summary>
    [Serializable]
    public class DoorKey : Item {
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
        /// Overloaded constructor that instantiates a DoorKey
        /// </summary>
        /// <param name="name"> Name </param>
        /// <param name="affectValue"> Value of affect </param>
        /// <param name="code"> Code - must match Door </param>
        public DoorKey(string name, int affectValue, string code) : base(name, affectValue) {
            _Code = code;
        }
        #endregion
    }
}
