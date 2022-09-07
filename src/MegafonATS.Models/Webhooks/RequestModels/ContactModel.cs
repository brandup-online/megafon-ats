namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class ContactModel
    {
        /// <summary>
        /// номер телефона клиента 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// уникальный id звонка, совпадает для всех связанных звонков
        /// </summary>
        public string Callid { get; set; }
    }
}