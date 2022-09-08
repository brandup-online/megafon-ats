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
        public string CallId { get; set; }

        /// <summary>
        /// Личный номер телефона, через который прошел входящий вызов
        /// </summary>
        public string Diversion { get; set; }
    }
}