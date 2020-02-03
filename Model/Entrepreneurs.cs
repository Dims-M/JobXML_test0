using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Model
{
    /// <summary>
    /// Класс  описывает Индивидуальных предпринимателей
    /// </summary>
   public class Entrepreneurs
    {

        /// <summary>
        /// ID Огранизации
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Навзание  компании
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///2е Навзание  компании
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Выручка организации
        /// </summary>
        public double Profit { get; set; }

        /// <summary>
        /// дата создания Компании
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// дата формирования отчета
        /// </summary>
        public DateTime ReportDate { get; set; }

    }
}
