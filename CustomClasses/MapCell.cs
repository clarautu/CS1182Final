using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - MapCell
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates a MapCell
    /// </summary>
    [Serializable]
    public class MapCell {
        #region Class Level Variables
        private int[] _Location;
        private bool _IsDiscovered = false;
        private Monster _Monster = null;
        private Item _Item = null;
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets _Location
        /// </summary>
        /// <remarks> Read only </remarks>
        public int[] Location {
            get {
                return _Location;
            }
        }

        /// <summary>
        /// Property that gets and sets _IsDiscovered
        /// </summary>
        public bool IsDiscovered {
            get {
                return _IsDiscovered;
            }
            set {
                _IsDiscovered = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _Monster
        /// </summary>
        public Monster Monster {
            get {
                return _Monster;
            }
            set {
                _Monster = value;
            }
        }

        /// <summary>
        /// Property that returns true if _Monster has been set
        /// </summary>
        /// <remarks> Read Only </remarks>
        public bool ContainsMonster {
            get {
                if (Monster != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Property that gets and sets _Item
        /// </summary>
        public Item Item {
            get {
                return _Item;
            }
            set {
                _Item = value;
            }
        }

        /// <summary>
        /// Property that returns true if _Item has been set
        /// </summary>
        /// <remarks> Read Only </remarks>
        public bool ContainsItem {
            get {
                if (Item != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Default constructor that instantiates a MapCell
        /// </summary>
        public MapCell() {

        }

        /// <summary>
        /// Overloaded constructor that instantiates a MapCell
        /// </summary>
        /// <param name="location"> Location of the MapCell </param>
        public MapCell(int[] location) {
            _Location = location;
        }
        #endregion Constructors
    }
}
