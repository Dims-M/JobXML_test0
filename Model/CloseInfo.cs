using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.Model
{
    /// <summary>
    /// Класс описывает Закрывшийсся Ип и Компании
    /// </summary>
   public class CloseInfo
    {
        /// <summary>
        /// ID Огранизации
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// дата закрытия Компании
        /// </summary>
        public DateTime CloseDate { get; set; }

      
        /// <summary>
        /// время существования организации в днях
        /// </summary>
        public int TimeDayClose { get; set; }
    }
}
