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
    [Table("VISITORFEEDBACK")]
    public class VisitorFeedback : MasterEntity
    {
        public int SpendRange { get; set; }
        public int EventRating { get; set; }
        public bool RecommendToOther { get; set; }
        public int ReasonForVisiting { get; set; }
        public int KnowAboutUs { get; set; }
        public string Comment { get; set; }

        public virtual Event Event { get; set; }
        public virtual Visitor Visitor { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<ExhibitorType> ExhibitorTypes { get; set; }

        public VisitorFeedback()
        {
            Categories = new List<Category>();
            Countries = new List<Country>();
            ExhibitorTypes = new List<ExhibitorType>();
        }

        public VisitorFeedback(int spendRange, int eventRating, bool recommendToOther, int reasonForVisiting, int knowAboutUs, string comment) : this()
        {
            Validate(spendRange, eventRating, recommendToOther, reasonForVisiting, knowAboutUs, comment);

            this.SpendRange = spendRange;
            this.EventRating = eventRating;
            this.RecommendToOther = recommendToOther;
            this.ReasonForVisiting = reasonForVisiting;
            this.KnowAboutUs = knowAboutUs;
            this.Comment = comment;
        }

        private static void Validate(int spendRange, int eventRating, bool recommendToOther, int reasonForVisiting, int knowAboutUs, string comment)
        {
            //if (String.IsNullOrEmpty(name))
            //    throw new ValidationException("Invalid Category Name");
        }

        public static VisitorFeedback Create(int spendRange, int eventRating, bool recommendToOther, int reasonForVisiting, int knowAboutUs, string comment)
        {
            return new VisitorFeedback(spendRange, eventRating, recommendToOther, reasonForVisiting, knowAboutUs, comment);
        }

        public void Update(int spendRange, int eventRating, bool recommendToOther, int reasonForVisiting, int knowAboutUs, string comment)
        {
            Validate(spendRange, eventRating, recommendToOther, reasonForVisiting, knowAboutUs, comment);

            this.SpendRange = spendRange;
            this.EventRating = eventRating;
            this.RecommendToOther = recommendToOther;
            this.ReasonForVisiting = reasonForVisiting;
            this.KnowAboutUs = knowAboutUs;
            this.Comment = comment;
        }
    }
}
