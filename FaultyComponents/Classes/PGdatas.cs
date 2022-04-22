using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaultyComponents.Classes
{
    public class PGdatas
    {
        public string Recipe { get; set; }

        public string Part { get; set; }
        public double? Всего_Взятий { get; set; }

        public double? Всего_Использовано { get; set; }

        public double? Всего_Потеряно { get; set; }

        public double? Ошибка_Распознования { get; set; }

        public double? Сброшено_ПоКоманде { get; set; }

        public double? Потеря_Компонента { get; set; }

        public double? Ошибка_Взятия { get; set; }

        public double? Процент_ТехПотерь { get; set; }
    }
}