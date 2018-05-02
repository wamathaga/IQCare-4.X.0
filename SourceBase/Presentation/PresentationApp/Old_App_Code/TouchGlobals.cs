#region Usings

//.Net Libs
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using Interface.Clinical;

#endregion

namespace Touch
{
    /// <summary>
    /// Summary description for TouchGlobals
    /// </summary>
    public static class TouchGlobals
    {
        /// <summary>
        /// Full site title tag at root.
        /// </summary>
        static public string RootTitle { get; set; }

        /// <summary>
        /// Sets whether to use Delta 9 or IQCare when searching for patient
        /// </summary>
        static public bool D9Search { get; set; }

        /// <summary>
        /// Sets whether to use Delta 9 or IQCare when searching for patient
        /// </summary>
        static public string OnScKeyboard { get; set; }

        /// <summary>
        /// Object to hold ALL the Touch Forms
        /// </summary>
        static public FormObjects.FormObject AllForms { get; set; }

        static public string ModuleName { get; set; }

        static public string ModuleId { get; set; }

        /// <summary>
        /// Set Touch globals
        /// </summary>
        static TouchGlobals()
        {
            // Cache all these values in static properties.
            RootTitle = WebConfigurationManager.AppSettings["TouchAppName"];
            D9Search = bool.Parse(WebConfigurationManager.AppSettings["D9Search"]);
            ModuleName = WebConfigurationManager.AppSettings["ModuleName"];
            ModuleId = WebConfigurationManager.AppSettings["ModuleID"];
            AllForms = new FormObjects.FormObject();
            OnScKeyboard = "$(function(){ " +
            "var script = document.createElement( 'script' );" +
"script.type = 'text/javascript';" +
"script.src = 'Scripts/keyb/jquery.keyboard.js';" +
"$('head')[0].appendChild(script);" +
"var script = document.createElement( 'script' );" +
"script.src = 'Scripts/keyb/demo/demo.js?reload';" +
"$('head')[0].appendChild(script);" +
"var script = document.createElement( 'script' );" +
"script.src = 'Scripts/keyb/jquery.keyboard.extension-typing.js';" +
"$('head')[0].appendChild(script); });"; 

            //OnScKeyboard = "$(function(){ $('head')[0].appendChild(\"<script src='Scripts/keyb/jquery.keyboard.js' type='text/javascript'></script>\");" +
            //            "$('head')[0].appendChild(\"<script type='text/javascript' src='Scripts/keyb/demo/demo.js?reload'></script>\"); " +
            //            "$('head')[0].appendChild(\"<script src='Scripts/keyb/jquery.keyboard.extension-typing.js' type='text/javascript'></script>\"); });";
        }
    }
    /**
    * Calculate Age in C#
    * https://gist.github.com/faisalman
    *
    * Copyright 2012-2013, Faisalman <fyzlman@gmail.com>
    * Licensed under The MIT License
    * http://www.opensource.org/licenses/mit-license
    */

    public class Age
    {
        public int Years;
        public int Months;
        public int Days;

        public Age(DateTime Bday)
        {
            this.Count(Bday);
        }

        public Age(DateTime Bday, DateTime Cday)
        {
            this.Count(Bday, Cday);
        }

        public Age Count(DateTime Bday)
        {
            return this.Count(Bday, DateTime.Today);
        }

        public Age Count(DateTime Bday, DateTime Cday)
        {
            try
            {
                if ((Cday.Year - Bday.Year) > 0 ||
                    (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                      ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
                {
                    int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                    int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                    if (Cday.Month > Bday.Month)
                    {
                        this.Years = Cday.Year - Bday.Year;
                        this.Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                        this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                    }
                    else if (Cday.Month == Bday.Month)
                    {
                        if (Cday.Day >= Bday.Day)
                        {
                            this.Years = Cday.Year - Bday.Year;
                            this.Months = 0;
                            this.Days = Cday.Day - Bday.Day;
                        }
                        else
                        {
                            this.Years = (Cday.Year - 1) - Bday.Year;
                            this.Months = 11;
                            this.Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                        }
                    }
                    else
                    {
                        this.Years = (Cday.Year - 1) - Bday.Year;
                        this.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                        this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                    }
                }
                else
                {
                    throw new ArgumentException("Birthday date must be earlier than current date");
                }
                return this;
            }
            catch (ArgumentException ex)
            {
                this.Years = 0;
                this.Months = 0;
                this.Days = 0;
                return this;
            }
            finally
            {
                
            }
        }
    }

    /**
     * Usage example:
     * ==============
     * DateTime bday = new DateTime(1987, 11, 27);
     * DateTime cday = DateTime.Today;
     * Age age = new Age(bday, cday);
     * Console.WriteLine("It's been {0} years, {1} months, and {2} days since your birthday", age.Year, age.Month, age.Day);
     */

    //Used for the Form Objects such as the forms themselves
    namespace FormObjects
    {
        /// <summary>
        /// Registration form object
        /// </summary>
        //public class objRegistration
        //{
        //    public objRegistration(){
        //        MotherPMTCTdrugsYN = -1;
        //        ChildPMTCTdrugsYN = -1;
        //    }
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; }
        //    public string DOB { get; set; }
        //    public int Sex { get; set; }
        //    public string RegistrationDate { get; set; }
        //    public string HouseNo { get; set; }
        //    public string Suburb { get; set; }
        //    public string District { get; set; }
        //    public string SubDistrict { get; set; }
        //    public string TelephoneNo { get; set; }
        //    public string Addresscomments { get; set; }
        //    public string PostalAddress { get; set; }
        //    public string PostalCode { get; set; }
        //    public string EntryPoint { get; set; }
        //    public string OtherEntryPoint { get; set; }
        //    public string CareGiverName { get; set; }
        //    public string CareGiverDOB { get; set; }
        //    public decimal CareGiverAge { get; set; }
        //    public int CareGiverGender { get; set; }
        //    public int CareGiverRelationship { get; set; }
        //    public string OtherCareGiver { get; set; }
        //    public string CareGiverTelephone { get; set; }
        //    public string MotherName { get; set; }
        //    public bool MotherAliveYN { get; set; }
        //    public int MotherPMTCTdrugsYN { get; set; }
        //    public List<int> MotherPMTCTdrugs { get; set; }
        //    public int ChildPMTCTdrugsYN { get; set; }
        //    public List<int> ChildPMTCTdrugs { get; set; }
        //    public int MotherARTYN { get; set; }
        //    public int FeedingOption { get; set; }
        //    public string DateConfirmedHIVPositive { get; set; }
        //    public string DateEnrolledHIVCare { get; set; }
        //    public int WHOStageAtEnrollment { get; set; }
        //    public string TransferInDate { get; set; }
        //    public int FromDistrict { get; set; }
        //    public string Facility { get; set; }
        //    public string DateStart { get; set; }
        //    public List<int> Regimen { get; set; }
        //    public string Weight { get; set; }
        //    public string Height { get; set; }
        //    public string BMI { get; set; }
        //    public int WHOStageAtTransfer { get; set; }
        //    public string PriorART { get; set; }
        //    public List<int> PriorARTRegimen { get; set; }
        //    public string PriorARTDateLastUsed { get; set; }
        //    public List<DrugAllergy> DrugAllergies { get; set; }
            
        //    public class DrugAllergy
        //    {
        //        public string Allergen { get; set; }
        //        public string TypeOfReaction { get; set; }
        //        public string DateOfAllergy { get; set; }
        //        public string MedicalConditions { get; set; }
        //    }

        //}
        /// <summary>
        /// Pharmacy form object
        /// </summary>
        public class objPharmacy
        {
            string Name { get; set; }
            int ID { get; set; }

        }

        /// <summary>
        /// Used to hold a cached version of the Form object collection
        /// </summary>
        public class FormObject
        {
            public objRegistration Registration { get; set; }
            public objPharmacy Pharmacy { get; set; }

            public FormObject(objRegistration objRegistration = null, objPharmacy objPharmacy = null)
            {
                Registration = objRegistration;
                Pharmacy = objPharmacy;
            }
        }
        public class FormObjects : IEnumerable
        {
            private FormObject[] _form;
            public FormObjects(FormObject[] pArray)
            {
                _form = new FormObject[pArray.Length];

                for (int i = 0; i < pArray.Length; i++)
                {
                    _form[i] = pArray[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public FormObjectEnum GetEnumerator()
            {
                return new FormObjectEnum(_form);
            }
        }
        public class FormObjectEnum : IEnumerator
        {
            public FormObject[] _form;

            // Enumerators are positioned before the first element 
            // until the first MoveNext() call. 
            int position = -1;

            public FormObjectEnum(FormObject[] list)
            {
                _form = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _form.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public FormObject Current
            {
                get
                {
                    try
                    {
                        return _form[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
