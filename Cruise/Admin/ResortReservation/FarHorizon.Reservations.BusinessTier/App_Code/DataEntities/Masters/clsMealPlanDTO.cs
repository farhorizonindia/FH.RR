using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsMealPlanDTO
    {
        private int _MealPlanId;
        private string _MealPlanCode;
        private string _MealPlan;
        private string _MealPlanDesc;
        private bool _WelcomeDrink;
        private bool _Breakfast;
        private bool _Lunch;
        private bool _EveSnacks;        
        private bool _Dinner;
                
        #region MealPlanMasterProperties

        public int MealPlanId
        {
            get { return _MealPlanId; }
            set { _MealPlanId = value; }
        }

        public string MealPlanCode
        {
            get { return _MealPlanCode; }
            set { _MealPlanCode = value; }
        }

        public string MealPlan
        {
            get { return _MealPlan; }
            set { _MealPlan = value; }
        }

        public string MealPlanDesc
        {
            get { return _MealPlanDesc; }
            set { _MealPlanDesc = value; }
        }

        public bool WelcomeDrink
        {
            get { return _WelcomeDrink; }
            set { _WelcomeDrink = value; }
        }
        public bool Breakfast
        {
            get { return _Breakfast; }
            set { _Breakfast = value; }
        }
        public bool Lunch
        {
            get { return _Lunch; }
            set { _Lunch = value; }
        }
        public bool EveningSnacks
        {
            get { return _EveSnacks; }
            set { _EveSnacks = value; }
        }
        public bool Dinner
        {
            get { return _Dinner; }
            set { _Dinner = value; }
        }


        #endregion MealPlanMasterProperties
    }
}
