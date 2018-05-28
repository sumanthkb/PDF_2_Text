using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_2_Text
{
    class Program
    {
        static void Main(string[] args)
        {
            string OCRText = ExtractTextFromPdf(@"C:\Users\80056\Desktop\FileWatcher\Image_11866 (1).pdf");
            if (OCRText.Length > 0)
            {
                OCRText.Replace("\n\n", "\n");//removing toomany "\n"
                OCRText = OCRText.Replace("\n", Environment.NewLine) + Environment.NewLine;
            }
            System.IO.File.WriteAllText(@"C:\Users\80056\Desktop\FileWatcher\PDF1.txt", OCRText);
        }

        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }

        private static string ExtractTextFromPdfFromPDFBox(string path)
        {
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(path);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }
    }
}
