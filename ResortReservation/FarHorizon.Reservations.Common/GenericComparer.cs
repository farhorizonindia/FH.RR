using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;

namespace FarHorizon.Reservations.Common
{
    /// <summary>
    /// This class is used to compare any 
    /// type(property) of a class for sorting.
    /// This class automatically fetches the 
    /// type of the property and compares.

    /// </summary>

    public sealed class GenericComparer<T> : IComparer<T>
    {
        public enum SortOrder { Ascending, Descending };

        #region member variables

        private string sortColumn;

        private SortOrder sortingOrder;

        #endregion


        #region constructor

        public GenericComparer(string sortColumn, SortOrder sortingOrder)
        {
            this.sortColumn = sortColumn;
            this.sortingOrder = sortingOrder;
        }

        #endregion

        #region public property
        /// <summary>
        /// Column Name(public property of the class) to be sorted.
        /// </summary>

        public string SortColumn
        {
            get { return sortColumn; }
        }
        /// <summary>
        /// Sorting order.
        /// </summary>

        public SortOrder SortingOrder
        {
            get { return sortingOrder; }
        }
        #endregion

        #region public methods

        /// <summary>
        /// Compare interface implementation
        /// </summary>
        /// <param name="x">custom Object</param>
        /// <param name="y">custom Object</param>
        /// <returns>int</returns>

        public int Compare(T x, T y)
        {

            PropertyInfo propertyInfo = typeof(T).GetProperty(sortColumn);
            IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
            IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);
            if (sortingOrder == SortOrder.Ascending)
            {
                return (obj1.CompareTo(obj2));
            }
            else
            {
                return (obj2.CompareTo(obj1));
            }
        }

        #endregion
    }

}

