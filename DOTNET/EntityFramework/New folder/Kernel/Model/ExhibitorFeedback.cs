using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("EXHIBITORFEEDBACK")]
    public class ExhibitorFeedback : MasterEntity
    {
        public int Satisfaction { get; set; }
        public int Objective { get; set; }
        public bool TargetAudience { get; set; }
        public int QualityOfVisitor { get; set; }
        public bool ExpectedBusiness { get; set; }
        public int IIMTFSatisfaction { get; set; }
        public int IIMTFTeam { get; set; }
        public int IIMTFFacility { get; set; }
        public virtual ICollection<Venue> MarketIntrested { get; set; }
        public string Comment { get; set; }

        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Event Event { get; set; }

        public ExhibitorFeedback()
        {
            MarketIntrested = new List<Venue>();
        }

        public ExhibitorFeedback(int satisfaction, int objective, bool targetAudience, int qualityOfVisitor, bool expectedBusiness, int iimtfSatisfaction, int iimtfTeam, int iimtfFacility, string comment) : this()
        {
            Validate(satisfaction, objective, targetAudience, qualityOfVisitor, expectedBusiness, iimtfSatisfaction, iimtfTeam, iimtfFacility, comment);

            this.Satisfaction = satisfaction;
            this.Objective = objective;
            this.TargetAudience = targetAudience;
            this.QualityOfVisitor = qualityOfVisitor;
            this.ExpectedBusiness = expectedBusiness;
            this.IIMTFSatisfaction = iimtfSatisfaction;
            this.IIMTFTeam = iimtfTeam;
            this.IIMTFFacility = iimtfFacility;
            this.Comment = comment;
        }

        private static void Validate(int satisfaction, int objective, bool targetAudience, int qualityOfVisitor, bool expectedBusiness, int iimtfSatisfaction, int iimtfTeam, int iimtfFacility, string comment)
        {
            //if (String.IsNullOrEmpty(name))
            //    throw new ValidationException("Invalid Category Name");
        }

        public static ExhibitorFeedback Create(int satisfaction, int objective, bool targetAudience, int qualityOfVisitor, bool expectedBusiness, int iimtfSatisfaction, int iimtfTeam, int iimtfFacility, string comment)
        {
            return new ExhibitorFeedback(satisfaction, objective, targetAudience, qualityOfVisitor, expectedBusiness, iimtfSatisfaction, iimtfTeam, iimtfFacility, comment);
        }

        public void Update(int satisfaction, int objective, bool targetAudience, int qualityOfVisitor, bool expectedBusiness, int iimtfSatisfaction, int iimtfTeam, int iimtfFacility, string comment)
        {
            Validate(satisfaction, objective, targetAudience, qualityOfVisitor, expectedBusiness, iimtfSatisfaction, iimtfTeam, iimtfFacility, comment);

            this.Satisfaction = satisfaction;
            this.Objective = objective;
            this.TargetAudience = targetAudience;
            this.QualityOfVisitor = qualityOfVisitor;
            this.ExpectedBusiness = expectedBusiness;
            this.IIMTFSatisfaction = iimtfSatisfaction;
            this.IIMTFTeam = iimtfTeam;
            this.IIMTFFacility = iimtfFacility;
            this.Comment = comment;
        }
    }
}
