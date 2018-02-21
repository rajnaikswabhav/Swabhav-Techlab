using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Service;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("VISITOR")]
    public class Visitor : MasterEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Education { get; set; }
        public string Income { get; set; }
        public string OTPCode { get; set; }
        public bool isMobileNoVerified { get; set; }
        public string FacebookId { get; set; }

        public virtual Organizer Organizer { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        
        public Visitor()
        {
            Categories = new List<Category>();
        }

        private static void Validated(string mobileNo, string emailId)
        {
            //if (String.IsNullOrEmpty(emailId))
            //    throw new ValidationException("Invalid Email");
        }

        private static void Validate(string firstName, string lastName, string address, int pinCode, string mobileNo, string emailId, bool gender, string education, string income)
        {
            //if (String.IsNullOrEmpty(firstName))
            //    throw new ValidationException("Invalid First Name");
            //if (String.IsNullOrEmpty(lastName))
            //    throw new ValidationException("Invalid Last Name");
            if (String.IsNullOrEmpty(emailId))
                throw new ValidationException("Invalid Email");
            //if (String.IsNullOrEmpty(education))
            //    throw new ValidationException("Invalid Education");
            //if (String.IsNullOrEmpty(income))
            //    throw new ValidationException("Invalid Income");
        }

        public Visitor(string firstName, string lastName, string address, string mobileNo, int pinCode, string emailId, string dateOfBirth, bool gender, string education, string income,string facebookId)
        {
            Validated(mobileNo, emailId);
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Pincode = pinCode;
            this.MobileNo = mobileNo;
            this.EmailId = emailId;
            if (dateOfBirth != "" && dateOfBirth != null)
            {
                this.DateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            this.Gender = gender;
            this.Education = education;
            this.Income = income;
            this.FacebookId = FacebookId;
        }

        public static Visitor Create(string firstName, string lastName, string address, string mobileNo, int pinCode, string emailId, string dateOfBirth, bool gender, string education, string income,string facebookId)
        {
            return new Visitor(firstName, lastName, address, mobileNo, pinCode, emailId, dateOfBirth, gender, education, income,facebookId);
        }

        public void Update(string firstName, string lastName, string address, string mobileNo, int pinCode, string emailId, string dateOfBirth, bool gender, string education, string income)
        {
            Validate(firstName, lastName, address, pinCode, mobileNo, emailId, gender, education, income);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Pincode = pinCode;
            if (string.IsNullOrEmpty(mobileNo) == false)
            {
                this.MobileNo = mobileNo;
            }
            if (string.IsNullOrEmpty(emailId) == false)
            {
                this.EmailId = emailId;
            }
            if (string.IsNullOrEmpty(dateOfBirth) == false)
            {
                this.DateOfBirth = Convert.ToDateTime(dateOfBirth);
            }

            this.Gender = gender;
            this.Education = education;
            this.Income = income;
        }

        public string GenerateActivationCode()
        {
            TokenGenerationService generator = new TokenGenerationService();
            generator.Exclusions = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            OTPCode = generator.Generate();
            return OTPCode;
        }

        public void ValidateActivationCode(string code)
        {
            if (OTPCode.Equals(code))
                return;
            else
                throw new ValidationException("Invalid OTP");
        }
    }
}
