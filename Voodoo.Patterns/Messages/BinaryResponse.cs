namespace Voodoo.Messages
{
    public class BinaryResponse : Response
    {
        public byte[] Data { get; set; }

        /// <summary>
        /// application/pdf, image/gif, image/png, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, etc
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// inline or attachment; file name is optional
        /// </summary>
        public string ContentDisposition { get; set; }

        public string FileName { get; set; }
    }
}