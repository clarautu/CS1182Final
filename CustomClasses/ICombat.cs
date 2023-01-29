using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - ICombat
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Interface for classes that can participate in combat
    /// </summary>
    public interface ICombat {
        #region Forced Methods
        bool Attack(Actor actor);
        #endregion
    }
}
