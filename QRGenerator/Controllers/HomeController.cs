using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QRGenerator.Models;
using static QRCoder.PayloadGenerator;

namespace QRGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            QRCodeModel model = new QRCodeModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(QRCodeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Payload payload = null;
                    string text = string.Empty;
                    switch (model.QRCodeType)
                    {
                        case 1: // website url
                            payload = new Url(model.WebsiteURL);
                            break;
                        case 2: // bookmark url
                            payload = new Bookmark(model.BookmarkURL, model.BookmarkURL);
                            break;
                        case 3: // compose sms
                            payload = new SMS(model.SMSPhoneNumber, model.SMSBody);
                            break;
                        case 4: // compose whatsapp message
                            payload = new WhatsAppMessage(model.WhatsAppNumber, model.WhatsAppMessage);
                            break;
                        case 5: //compose email
                            payload = new Mail(model.ReceiverEmailAddress, model.EmailSubject, model.EmailMessage);
                            break;
                        case 6: // wifi qr code
                            payload = new WiFi(model.WIFIName, model.WIFIPassword, WiFi.Authentication.WPA);
                            break;
                        case 7: // text message
                            text = model.Text;
                            break;
                    }
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData;
                    if (model.QRCodeType != 7)
                        qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                    else
                        qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

                    BitmapByteQRCode bitmapQRCode = new BitmapByteQRCode(qrCodeData);
                    byte[] qrCodeImage = bitmapQRCode.GetGraphic(20);
                    string qrCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCodeImage)}";
                    model.QRImageURL = qrCodeAsImageBase64;
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                };
                return View("Error", errorViewModel);
            }
            return View();
        }
    }
}