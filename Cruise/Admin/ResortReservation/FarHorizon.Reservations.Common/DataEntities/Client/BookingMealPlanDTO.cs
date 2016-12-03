using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Client
{
    public class BookingMealPlanDTO
    {
        #region Variables
        private int _iBookingId;
        private DateTime _dtMealPlanDate;
        private int _iMealPlanId;
        private string _sMealPlanCode;        
        private bool _bWelcomeDrink;
        private bool _bBreakfast;
        private bool _bLunch;
        private bool _bEveSnacks;

        
        private bool _bDinner; 
        #endregion

        #region Properties
        public int BookingId
        {
            get { return _iBookingId; }
            set { _iBookingId = value; }
        }
        public DateTime MealPlanDate
        {
            get { return _dtMealPlanDate; }
            set { _dtMealPlanDate = value; }
        }
        public int MealPlanId
        {
            get { return _iMealPlanId; }
            set { _iMealPlanId = value; }
        }
        public string MealPlanCode
        {
            get { return _sMealPlanCode; }
            set { _sMealPlanCode = value; }
        }
        public bool WelcomeDrink
        {
            get { return _bWelcomeDrink; }
            set { _bWelcomeDrink = value; }
        }
        public bool Breakfast
        {
            get { return _bBreakfast; }
            set { _bBreakfast = value; }
        }
        public bool Lunch
        {
            get { return _bLunch; }
            set { _bLunch = value; }
        }
        public bool EveningSnacks
        {
            get { return _bEveSnacks; }
            set { _bEveSnacks = value; }
        }
        public bool Dinner
        {
            get { return _bDinner; }
            set { _bDinner = value; }
        } 
        #endregion
    }
}
