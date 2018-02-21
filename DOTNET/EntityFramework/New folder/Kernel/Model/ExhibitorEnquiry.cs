using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("EXHIBITORENQUIRY")]
    public class ExhibitorEnquiry : MasterEntity
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string CompanyDescription { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public string Comment { get; set; }

        public virtual Event Event { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual ExhibitorType ExhibitorType { get; set; }
        public virtual ExhibitorIndustryType ExhibitorIndustryType { get; set; }
        public virtual ExhibitorStatus ExhibitorStatus { get; set; }
        public virtual ExhibitorRegistrationType ExhibitorRegistrationType { get; set; }

        public ExhibitorEnquiry()
        {
        }

        private ExhibitorEnquiry(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string comment)
        {
            Validate(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode);

            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.CompanyName = companyName;
            this.Designation = designation;
            this.CompanyDescription = companyDescription;
            this.Address = address;
            this.PinCode = pinCode;
            this.Comment = comment;
        }

        private static void Validate(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public void Update(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string comment)
        {
            Validate(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode);
            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.CompanyName = companyName;
            this.Designation = designation;
            this.CompanyDescription = companyDescription;
            this.Address = address;
            this.PinCode = pinCode;
            this.Comment = comment;
        }

        public static ExhibitorEnquiry Create(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string comment)
        {
            return new ExhibitorEnquiry(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode, comment);
        }
    }
}
