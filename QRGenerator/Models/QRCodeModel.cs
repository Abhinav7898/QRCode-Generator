using System.ComponentModel.DataAnnotations;

namespace QRGenerator.Models
{
    public class QRCodeModel
    {
        [Required]
        [Range(1, 13, ErrorMessage = "Please select a valid QR code type.")]
        public int QRCodeType { get; set; }
        public string? QRImageURL { get; set; } = string.Empty;
        // for email qr codes
        public string? ReceiverEmailAddress { get; set; } = string.Empty;
        public string? EmailSubject { get; set; } = string.Empty;
        public string? EmailMessage { get; set; } = string.Empty;
        //for sms qr codes
        public string? SMSPhoneNumber { get; set; } = string.Empty;
        public string? SMSBody { get; set; } = string.Empty;
        //for website
        public string? WebsiteURL { get; set; } = string.Empty;
        // for whatsapp qr message code
        public string? WhatsAppNumber { get; set; } = string.Empty;
        public string? WhatsAppMessage { get; set; } = string.Empty;
        // for wifi qr code
        public string? WIFIName { get; set; } = string.Empty;
        public string? WIFIPassword { get; set; } = string.Empty;

        // for Text qr code
        public string? Text { get; set; } = string.Empty;
    }
}
