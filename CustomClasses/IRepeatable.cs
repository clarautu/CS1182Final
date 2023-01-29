using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - IRepeatable
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Interface for classes that can be cloned
    /// </summary>
    public interface IRepeatable <T> {
        #region Forced Methods
        T CreateCopy();
        #endregion
    }
}
