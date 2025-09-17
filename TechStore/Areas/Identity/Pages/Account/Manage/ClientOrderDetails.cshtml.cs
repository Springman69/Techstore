using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Areas.Identity.Pages.Account.Manage
{
    public class ClientOrderDetailsModel : PageModel
    {
        private int _orderId;
        private readonly ApplicationDbContext _context;

        public ClientOrderDetailsModel(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        private List<Order> Orders { get; set; }

        [BindProperty]
        public Order Order { get; set; }

        public void OnGet(int id)
        {
            TempData["OrderId"] = id.ToString();
            _orderId = id;
            Order = LoadData();
        }

        public IActionResult OnPostGenerateInvoice()
        {
            _orderId = Convert.ToInt32(TempData["OrderId"]);
            TempData["OrderId"] = _orderId.ToString();
            Order order = LoadData();
            MemoryStream memoryStream = new MemoryStream();
            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Tabela z danymi odbiorcy i adresem dostawy
            PdfPTable dataTable = new PdfPTable(2);
            dataTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Kolumny dla danych odbiorcy
            PdfPTable clientTable = new PdfPTable(1);
            clientTable.AddCell($"Imiê i nazwisko: {order.Client.FirstName} {order.Client.LastName}");
            clientTable.AddCell($"Telefon: {order.Client.PhoneNumber}");
            clientTable.AddCell($"E-mail: {order.Client.Email}");

            // Kolumny dla adresu dostawy
            PdfPTable addressTable = new PdfPTable(1);
            addressTable.AddCell($"Lokalizacja: {order.ShippingAddress.Locality}");
            addressTable.AddCell($"Adres: {order.ShippingAddress.Street} {order.ShippingAddress.BuildingNumber}");
            addressTable.AddCell($"Kod pocztowy: {order.ShippingAddress.PostalCode} {order.ShippingAddress.Locality}");

            // Dodanie kolumn do tabeli g³ównej obok siebie
            dataTable.AddCell(clientTable);
            dataTable.AddCell(addressTable);

            document.Add(dataTable);

            // Dodanie tabeli z zamówieniem
            PdfPTable orderTable = new PdfPTable(4);
            orderTable.AddCell("Zdjêcie");
            orderTable.AddCell("Nazwa produktu");
            orderTable.AddCell("Iloœæ");
            orderTable.AddCell("Cena");

            foreach (var pr in order.ProductOrderRelations)
            {
                orderTable.AddCell(pr.Product.Image);
                orderTable.AddCell(pr.Product.Name);
                orderTable.AddCell($"{pr.Quantity} szt.");
                orderTable.AddCell($"{pr.Product.Price} z³");
            }

            document.Add(orderTable);

            document.Close();
            writer.Close();

            byte[] fileBytes = memoryStream.ToArray();
            return File(fileBytes, "application/pdf", "Invoice.pdf");
        }

        private Order LoadData()
        {
            Orders = _context.Orders
               .Include(x => x.Client)
               .Include(x => x.ShippingAddress)
               .Include(x => x.ProductOrderRelations)
               .ThenInclude(pr => pr.Product)
               .ToList();
            Order = Orders.FirstOrDefault(o => o.Id == _orderId) ?? new Order();
            return Order;
        }

        private string ModifyFileData(string filePath)
        {
            string modifiedFilePath = "ModifiedFaktura.pdf";

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                PdfReader reader = new PdfReader(fs);
                using (FileStream outStream = new FileStream(modifiedFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfStamper stamper = new PdfStamper(reader, outStream);

                    PdfContentByte canvas = stamper.GetOverContent(1);
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    canvas.BeginText();
                    canvas.SetFontAndSize(bf, 12);
                    canvas.ShowTextAligned(Element.ALIGN_LEFT, "Nowy tekst w PDF", 100, 700, 0);
                    canvas.EndText();

                    stamper.Close();
                }
            }

            return modifiedFilePath;
        }
    }
}
