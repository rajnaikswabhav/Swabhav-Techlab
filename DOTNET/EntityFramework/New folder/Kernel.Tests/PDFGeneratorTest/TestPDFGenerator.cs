using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Service.PDFGenerator;

namespace Kernel.Tests.PDFGeneratorTest
{
    class TestPDFGenerator
    {
        public static void Main() {

            String html = "<html><body><h1>Welcome to pDF</h1></body></html>";
            string file = ConfigurationManager.AppSettings["PdfFolderLocation"];

            PDFGenerator gen = new PDFGenerator();
            gen.CreatePDFFromHTMLFile(html, "test.pdf");

        }
    }
}
