using DriverLicenseBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSDataAccessLayer;
namespace POSBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public clsUsers()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            PasswordHash = "";
            RoleID = 2;
            IsActive = true;
            CreatedAt = DateTime.Now;
            UpdateAt = null;
            Mode = enMode.AddNew;
        }

        private clsUsers(int userID, int personID, string userName, string passwordHash,
                         int roleID, bool isActive, DateTime createdAt, DateTime? updateAt)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            PasswordHash = passwordHash;
            RoleID = roleID;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
            Mode = enMode.Update;
           
        }

        private bool _AddNewUser()
        {
            PasswordHash = clsSecurityHelper.ComputeHash(PasswordHash);
            RoleID = 0; // Default RoleID
            UserID = clsUserData.AddNewUser(UserName, PasswordHash, RoleID, IsActive, PersonID,CreatedAt,UpdateAt);

            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
           
            RoleID = 0;
            return clsUserData.UpdateUser(UserID, UserName,RoleID,IsActive,UpdateAt);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }

        public static clsUsers Find(int userID)
        {
            int personID = -1, roleID = 0;
            string userName = "", passwordHash = "";
            bool isActive = false;
            DateTime createdAt = DateTime.Now;
            DateTime? updateAt = null;

            if (clsUserData.GetUserByUserID(userID, ref userName, ref passwordHash,
                ref roleID, ref isActive, ref createdAt, ref personID,ref updateAt))
            {
                return new clsUsers(userID, personID, userName, passwordHash,
                                    roleID, isActive, createdAt, updateAt);
            }

            return null;
        }

        public static clsUsers FindByPersonID(int personID)
        {
            int userID = -1, roleID = 0;
            string userName = "", passwordHash = "";
            bool isActive = false;
            DateTime createdAt = DateTime.Now;
            DateTime? updateAt = null;

            if (clsUserData.GetUserByPersonID(personID,
                ref userID, ref userName, ref passwordHash, ref roleID,
                ref isActive, ref createdAt,ref updateAt))
            {
                return new clsUsers(userID, personID, userName, passwordHash,
                                    roleID, isActive, createdAt,updateAt);
            }

            return null;
        }

        public static clsUsers FindByUserName(string userName)
        {
            int userID = -1, personID = -1, roleID = 0;
            string passwordHash = "";
            bool isActive = false;
            DateTime createdAt = DateTime.Now;
            DateTime? updateAt = null;

            if (clsUserData.GetUserByUserName(ref userID, userName, ref passwordHash,
                                              ref roleID, ref isActive, ref createdAt, ref personID,ref updateAt))
            {
                return new clsUsers(userID, personID, userName, passwordHash,
                                    roleID, isActive, createdAt,updateAt);
            }

            return null;
        }

        public static clsUsers LoginUser(string userName, string password)
        {
            string passwordhashed = clsSecurityHelper.ComputeHash(password);

            int userID = -1, personID = -1, roleID = 0;
            bool isActive = false;
            DateTime createdAt = DateTime.Now;
            DateTime? updateAt = null;
            if (clsUserData.LoginUser(ref userID, userName, passwordhashed,
                ref roleID, ref isActive, ref createdAt,ref personID))
            {
                return new clsUsers(userID, personID, userName, passwordhashed,
                                    roleID, isActive, createdAt,updateAt);
            }

            return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static DataTable GetAllUsers2(int currentPage, int PAGE_SIZE)
        {
            return clsUserData.GetAllUsers2(currentPage,PAGE_SIZE);
        }


        public static bool ChangePassword(int userID, string newPassword)
        {
            string newHash = clsSecurityHelper.ComputeHash(newPassword);
            return clsUserData.ChangePassword(userID, newHash);
        }

        public static bool IsUserExists(string userName)
        {
            return clsUserData.IsUserExists(userName);
        }

        public static bool IsUserExists(int PersonID)
        {
            return clsUserData.IsUserExists(PersonID);
        }

    }

    /*    public class clsUsers
    {

        public enum enMode { AddNew = 1, Update = 2 };

        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public clsUsers()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            this.PersonID = -1;

            Mode = enMode.AddNew;
        }

        private clsUsers(int userID, string userName, string password, bool isActive, int personID)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonID = personID;
            // Composition: clsUsers يحتوي على clsPerson
            this.PersonInfo = clsPerson.Find(personID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.UserName, clsSecurityHelper.ComputeHash(this.Password), this.IsActive, this.PersonID);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, clsSecurityHelper.ComputeHash(this.Password), this.IsActive, this.PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:
                    return _UpdateUser();


            }
            return false;

        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static clsUsers Find(int UserID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.GetUserByUserID(UserID, ref UserName, ref Password, ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers FindByUserName(string UserName)
        {
            int UserID = -1;
            string Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.GetUserByUserName(ref UserID, UserName, ref Password, ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int UserID = -1;

            if (clsUserData.GetUserByPersonID(ref UserID, ref UserName, ref Password, ref IsActive, PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers LoginUser(string UserName, string Password)
        {
            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.LoginUser(ref UserID, UserName, clsSecurityHelper.ComputeHash(Password), ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static async Task<clsUsers> LoginUserAsync(string UserName, string Password)
        {
            var result = await clsUserData.LoginUserAsync(UserName, clsSecurityHelper.ComputeHash(Password));

            if (result.IsFound)
                return new clsUsers(result.UserID, UserName, Password, result.IsActive, result.PersonID);
            else
                return null;
        }

        public static DataTable GetAllUser()
        {
            return clsUserData.GetAllUser();
        }

        public static DataTable GetAllUser2(int currentPage, int PAGE_SIZE)
        {
            return clsUserData.GetAllUser2(currentPage, PAGE_SIZE);
        }

        public static bool IsUserExists(int UserID)
        {
            return clsUserData.IsUserExists(UserID);
        }

        public static bool IsUserExists(string UserName)
        {
            return clsUserData.IsUserExists(UserName);
        }

        public static bool IsUserExistsByPseronID(int PersonID)
        {
            return clsUserData.IsUserExistsByPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, clsSecurityHelper.ComputeHash(NewPassword));
        }
    }
*/
}
