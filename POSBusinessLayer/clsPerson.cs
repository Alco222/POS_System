using HandlerErrors;
using POSDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBusinessLayer
{
    /// <summary>
    ///     These class are specific to all operations related to a person, such as Add New or Find.
    /// </summary>
    public class clsPerson
    {
        /// <summary>
        /// enMode is a process for changing the mode for this class.
        /// </summary>
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        /// <summary>
        /// This property is for reporting errors found in the class.
        /// </summary>
        public static string Message;
        public int? PersonID { get; set; }

        [Required(ErrorMessage = "FirstName Required!")]
        [StringLength(20,ErrorMessage ="FirstName must be less then 20 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required!")]
        [StringLength(20,ErrorMessage ="LastName must be less then 20 characters!")]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName+" "+ LastName; }
        }

        [Required(ErrorMessage = "National No Required!")]
        [StringLength(10,MinimumLength = 7, ErrorMessage ="National No must be between 7 and 10.")]
        public string NationalNo { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required!")]
        [StringLength(18,MinimumLength = 10,ErrorMessage = "Phone must be between 10 and 18.")]
        public string Phone { get; set; }
        public byte Gender { get; set; }

        [Required(ErrorMessage = "Address Required!")]
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }

        public clsPerson()
        {
            this.PersonID = null;
            this.FirstName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.Email = "";
            this.Phone = "";
            this.Gender = 0;
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        public clsPerson(int? PersonID, string FirstName, string LastName,string NationalNo, string Email,
            string Phone, byte Gender, string Address, DateTime DateOfBirth,string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.Email = Email;
            this.Phone = Phone;
            this.Gender = Gender;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
                this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.NationalNo, this.Email,
                                                          this.Phone, this.Gender, this.Address,
                                                          this.DateOfBirth, this.ImagePath);

                return (this.PersonID != null && this.PersonID > 0);
        }

        private bool _UpdatePerson()
        {
            if (this.PersonID == null || this.PersonID <= 0)
                return false;

            
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.NationalNo, this.Email,
                                                  this.Phone, this.Gender, this.Address,this.DateOfBirth,this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int? PersonID)
        {
            if (PersonID == null || PersonID <= 0)
                return false;

           return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople(int currentPage, int PAGE_SIZE)
        {
             return  clsPersonData.GetAllPeople(currentPage, PAGE_SIZE);
        }

        public static clsPerson Find(int? PersonID)
        {
            if (!PersonID.HasValue || PersonID <= 0)
                return null;

                string FirstName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
                byte Gender = 0;
                DateTime DateOfBirth = DateTime.Now;

                if (clsPersonData.GetPersonByPersonID(PersonID, ref FirstName, ref LastName, ref NationalNo, ref Email,
                                                      ref Phone, ref Gender, ref Address, ref DateOfBirth,
                                                      ref ImagePath))
                {
                    return new clsPerson(PersonID, FirstName, LastName, NationalNo, Email, Phone, Gender,
                                         Address, DateOfBirth, ImagePath);
                }
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            if (string.IsNullOrWhiteSpace(NationalNo))
                return null;

          
                int? PersonID = null;
                string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
                byte Gender = 0;
                DateTime DateOfBirth = DateTime.Now;

                if (clsPersonData.GetPersonByNationalNo(ref PersonID, ref FirstName, ref LastName, NationalNo, ref Email,
                                                        ref Phone, ref Gender, ref Address, ref DateOfBirth,
                                                        ref ImagePath))
                {
                    return new clsPerson(PersonID, FirstName, LastName, NationalNo, Email, Phone, Gender,
                                         Address, DateOfBirth, ImagePath);
                }
                return null;
        }

        public static bool IsPersonExists(int? PersonID)
        {
            if (!PersonID.HasValue || PersonID <= 0)
                return false;

            return clsPersonData.IsPersonExists(PersonID);
        }

        public static bool IsPersonExists(string NationalNo)
        {
            if (string.IsNullOrWhiteSpace(NationalNo))
                return false;

          
            return clsPersonData.IsPersonExists(NationalNo);
        }

    }
}

