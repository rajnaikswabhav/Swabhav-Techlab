using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Views;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("EXHIBITOR")]
    public class Exhibitor : MasterEntity
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string CompanyDescription { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Comment { get; set; }

        /*
        [Key]
        [Column(Order = 2)]
        public int TenantId { get; set; }*/

        public virtual Organizer Organizer { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual ExhibitorType ExhibitorType { get; set; }
        public virtual ExhibitorIndustryType ExhibitorIndustryType { get; set; }
        public virtual ExhibitorStatus ExhibitorStatus { get; set; }
        public virtual ExhibitorRegistrationType ExhibitorRegistrationType { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Stall> Stalls { get; set; }

        public Exhibitor()
        {
            Exhibitions = new List<Exhibition>();
            Categories = new List<Category>();
            Stalls = new List<Stall>();
        }

        private Exhibitor(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string password, int age)
        {
            Validate(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode, password);

            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.CompanyName = companyName;
            this.Designation = designation;
            this.CompanyDescription = companyDescription;
            this.Address = address;
            this.PinCode = pinCode;
            this.Password = password;
            this.Age = age;
        }

        private static void Validate(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string password)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public void Update(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string password, int age)
        {
            Validate(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode, password);
            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.CompanyName = companyName;
            this.Designation = designation;
            this.CompanyDescription = companyDescription;
            this.Address = address;
            this.PinCode = pinCode;
            this.Password = password;
            this.Age = age;
        }

        public static Exhibitor Create(string name, string emailId, string phoneNo, string companyName, string designation, string companyDescription, string address, int pinCode, string password, int age)
        {
            return new Exhibitor(name, emailId, phoneNo, companyName, designation, companyDescription, address, pinCode, password, age);
        }

        public void Allocate(Guid exhibitionId, StallAllocation[] allocations)
        {
            var mappedExhibition = Exhibitions.FirstOrDefault(x => x.Id == exhibitionId);

            if (mappedExhibition == null)
            {
                var exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
                mappedExhibition = exhibitionRepository.GetById(exhibitionId);
                if (mappedExhibition == null)
                    throw new ValidationException("Invalid Exhibition");

                Exhibitions.Add(mappedExhibition);
            }
            else
                RemoveStallsForExhibition(exhibitionId);

            var stallRepository = new EntityFrameworkRepository<Stall>();
            foreach (var allocation in allocations)
            {
                var stall = stallRepository.GetById(allocation.StallId);
                if (stall.Pavilion.Id != allocation.PavilionId)
                    throw new ValidationException("Stall not present in specified pavilion");

                Stalls.Add(stall);
            }
        }

        private void RemoveStallsForExhibition(Guid exhibitionId)
        {
            var existingStallsForExhibition = Stalls.Where(x => x.Pavilion.Exhibition.Id == exhibitionId);
            if (existingStallsForExhibition.Count() > 0)
            {
                foreach (var existingStall in existingStallsForExhibition)
                {
                    Stalls.Remove(existingStall);
                }
            }
        }
    }
}
