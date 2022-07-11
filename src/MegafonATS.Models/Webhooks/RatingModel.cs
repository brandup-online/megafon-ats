namespace MegafonATS.Models.Webhooks
{
    public class RatingModel
    {
        /// <summary>
        /// уникальный id звонка 
        /// </summary>
        public string Callid { get; set; }

        /// <summary>
        /// оценка качества 
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// номер телефона клиента, с которого или
        /// на который произошел звонок
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС, если есть
        /// </summary>
        public string Ext { get; set; }
    }
}