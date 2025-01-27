using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BoardGame_Quiz.Controllers
{
    public class CertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateCertificate(string playerName, string achievement, string level)
        //, string date, string signature)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Certificate";

            // Add a new page
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            // Create XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Define fonts
            XFont titleFont = new XFont("Times New Roman", 36, XFontStyleEx.Bold);
            XFont subtitleFont = new XFont("Times New Roman", 20, XFontStyleEx.Italic);
            XFont bodyFont = new XFont("Times New Roman", 18, XFontStyleEx.Regular);
            XFont footerFont = new XFont("Times New Roman", 16, XFontStyleEx.Bold);

            // Set page boundaries
            double margin = 40;
            double pageWidth = page.Width.Point - margin * 2;
            double pageHeight = page.Height.Point - margin * 2;

            // Determine level color
            XColor levelColor;
            switch (level.ToLower())
            {
                case "gold":
                    levelColor = XColors.Gold;
                    break;
                case "silver":
                    levelColor = XColors.Silver;
                    break;
                case "bronze":
                    levelColor = XColors.SaddleBrown;
                    break;
                default:
                    levelColor = XColors.Black; // Default for unknown levels
                    break;
            }

            // Draw border with level color
            XPen pen = new XPen(levelColor, 8);
            gfx.DrawRectangle(pen, margin, margin, pageWidth, pageHeight);

            // Draw title
            gfx.DrawString("CERTIFICATE", titleFont, new XSolidBrush(levelColor),
                new XRect(0, 70, page.Width.Point, 50), XStringFormats.TopCenter);

            // Subtitle
            gfx.DrawString("OF ACHIEVEMENT", subtitleFont, new XSolidBrush(levelColor),
                new XRect(0, 120, page.Width.Point, 30), XStringFormats.TopCenter);

            // Recipient Name
            gfx.DrawString("Presented to", bodyFont, XBrushes.Black,
                new XRect(0, 180, page.Width.Point, 30), XStringFormats.TopCenter);
            gfx.DrawString(playerName, titleFont, XBrushes.Black,
                new XRect(0, 220, page.Width.Point, 50), XStringFormats.TopCenter);

            // Achievement
            gfx.DrawString($"For {achievement}", bodyFont, XBrushes.Black,
                new XRect(0, 280, page.Width.Point, 30), XStringFormats.TopCenter);

            // Level Badge
            gfx.DrawString($"Level: {level.ToUpper()}", subtitleFont, new XSolidBrush(levelColor),
                new XRect(0, 330, page.Width.Point, 30), XStringFormats.TopCenter);

            // Date and signature
            gfx.DrawString($"Date: {DateTime.Now:dd/MM/yyyy}", footerFont, XBrushes.Black,
                new XRect(80, page.Height.Point - 150, 200, 30), XStringFormats.TopLeft);
            gfx.DrawString($"Signed: Superior", footerFont, XBrushes.Black,
                new XRect(page.Width.Point - 300, page.Height.Point - 150, 200, 30), XStringFormats.TopRight);

            // Save the document to memory stream
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                byte[] pdfData = stream.ToArray();

                // Define the path for saving the file in the temp folder
                string tempFilePath = Path.Combine(Path.GetTempPath(), $"{level}_Certificate.pdf");

                // Save the PDF file to the temp directory
                System.IO.File.WriteAllBytes(tempFilePath, pdfData);

                // Send the email with the generated PDF attachment
                SendEmailWithGeneratedCertificate(tempFilePath);

                // Delete the generated file after sending email
                //if (System.IO.File.Exists(tempFilePath))
                //{
                //    System.IO.File.Delete(tempFilePath);
                //}

                // Return the file as a download response (optional)
                return File(pdfData, "application/pdf", $"{level}_Certificate.pdf");
            }
        }

        static void SendEmailWithGeneratedCertificate(string pdfFilePath)
        {
            // Configure the email client
            string fromAddress = "arunbalaji.sf3996@gmail.com";
            string toAddress = "arunjegan21@gmail.com";
            string subject = "Subject: Certificate of Winning";
            string body = "Hello, please find the attached Certification of winning.";

            // Create the email message
            MailMessage mail = new MailMessage(fromAddress, toAddress, subject, body);

            // Attach the PDF file
            mail.Attachments.Add(new Attachment(pdfFilePath));

            // Set up SMTP client
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("arunbalaji.sf3996@gmail.com", "Arunbalaji@21");
            SmtpServer.EnableSsl = true;

            // Send the email
            try
            {
                SmtpServer.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                //Console.WriteLine("Failed recipient: " + ex.FailedRecipient);
                Console.WriteLine("Status code: " + ex.StatusCode);
            }
        }
    }
}
