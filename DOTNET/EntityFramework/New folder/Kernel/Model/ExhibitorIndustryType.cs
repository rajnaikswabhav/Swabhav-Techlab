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
    [Table("EXHIBITORINDUSTRYTYPE")]
    public class ExhibitorIndustryType:MasterEntity
    {
        public string IndustryType { get; set; }
        public string Color { get; set; }

        public ExhibitorIndustryType()
        {
        }

        public ExhibitorIndustryType(string industryType) : this()
        {
            Validate(industryType);
            this.IndustryType = industryType;
        }

        private static void Validate(string industryType)
        {
            if (String.IsNullOrEmpty(industryType))
                throw new ValidationException("Invalid Exhibitor Type");
        }

        public static ExhibitorIndustryType Create(string industryType)
        {
            return new ExhibitorIndustryType(industryType);
        }

        public void Update(string industryType,string color)
        {
            Validate(industryType);
            this.IndustryType = industryType;
            this.Color = color;
        }
    }
}
