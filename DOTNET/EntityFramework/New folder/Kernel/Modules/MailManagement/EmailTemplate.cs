using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.MailManagement
{
    [Table("EMAILTEMPLATE")]
    public class EmailTemplate : MasterEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string EmailTemplateText { get; set; }

        public virtual EmailType EmailType { get; set; }

        public EmailTemplate()
        {

        }

        public EmailTemplate(string name, string subject,string emailTemplateText) : this()
        {
            Validate(name, subject, emailTemplateText);

            this.Name = name;
            this.Subject = subject;
            this.EmailTemplateText = emailTemplateText;
        }

        private static void Validate(string name, string subject, string emailTemplateText)
        {
            if (string.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public static EmailTemplate Create(string name, string subject, string emailTemplateText)
        {
            return new EmailTemplate(name, subject, emailTemplateText);
        }

        public void Update(string name, string subject, string emailTemplateText)
        {
            Validate(name, subject, emailTemplateText);

            this.Name = name;
            this.Subject = subject;
            this.EmailTemplateText = emailTemplateText;
        }
    }
}
