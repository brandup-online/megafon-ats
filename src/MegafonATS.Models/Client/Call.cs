﻿using MegafonATS.Models.Enums;
using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client
{
    public class Call
    {
        /// <summary>
        /// Уникальный идентификатор звонка
        /// </summary>
        [JsonPropertyName("uid")]
        public string CallId { get; set; }
        /// <summary>
        /// Тип вызова: in / out / missed
        /// </summary>
        public CallDirection Direction { get; set; }
        /// <summary>
        /// Статус звонка (успешный/пропущенный/не состоялся)
        /// </summary>
        public CallStatus Status { get; set; }
        /// <summary>
        /// Номер клиента
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// Логин сотрудника, который разговаривал с клиентом или имя группы или код:
        /// ivr / fax, если звонок не дошел до сотрудника
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Номер телефона, через который пришел звонок
        /// </summary>
        public string Diversion { get; set; }
        /// <summary>
        /// Адресат входящего звонка
        /// </summary>
        public string Destination { get; set; }
        /// <summary> 
        /// Логин сотрудника
        /// </summary>
        public string User { get; set; }
        /// <summary> 
        /// Имя сотрудника
        /// </summary>
        public string UserName { get; set; }
        /// <summary> 
        /// Имя группы через которую прошел звонок
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Время начала звонка в UTC
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// Время ожидания на линии (секунд)
        /// </summary>
        public int Wait { get; set; }
        /// <summary>
        /// Длительность разговора (секунд)
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// Ссылка на запись разговора
        /// </summary>
        public Uri Record { get; set; }
        /// <summary>
        /// Оценка качества разговора, если есть
        /// </summary>
        public string Rating { get; set; }
    }
}