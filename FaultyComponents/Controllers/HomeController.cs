using FaultyComponents;
using FaultyComponents.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ResetComponents.Controllers
{
    public class HomeController : Controller
    {
        SMDCOMPONETSEntities smd = new SMDCOMPONETSEntities();
        //QAEntities QAEntities = new QAEntities();
        public ActionResult Index()
        {
            return View(smd.NewFujiScrapSQliteLine1.Select(c => c.Line).Distinct().ToList());
            //return View();
        }

        public ActionResult GetDataLine(string Line)
        {
            var date = GetListDate();
            var date1 = date[0];
            var date2 = date[1];

            var Data = smd.NewFujiScrapSQliteLine1.Where(c => c.Line == Line & c.DateTime > date1 & c.DateTime < date2)
                .GroupBy(c => new { c.Module }).Select(c => new Module
                {
                    Number = c.Key.Module,
                    Line = c.Select(b => b.Line).FirstOrDefault(),
                    Slots = c.GroupBy(b => b.Slot).Select(b => new Slot()
                    {

                        Number = b.Key,
                        PGs = b.GroupBy(x => new { Recipe = x.Recipe, Part = x.Part }).Select(x => new PGdatas()
                        {

                            Recipe = x.Key.Recipe,
                            Part = x.Key.Part,
                            Всего_Взятий = x.Select(z => z.PickUp).Sum(),
                            Всего_Использовано = x.Select(z => z.Usage).Sum(),
                            Всего_Потеряно = x.Select(z => z.TotalError).Sum(),
                            Ошибка_Распознования = x.Select(z => z.Error).Sum(),
                            Сброшено_ПоКоманде = x.Select(z => z.Reject).Sum(),
                            Потеря_Компонента = x.Select(z => z.Dislodged).Sum(),
                            Ошибка_Взятия = x.Select(z => z.NoPickUp).Sum(),
                            Процент_ТехПотерь = x.Select(z => z.TotalError).Sum() / x.Select(z => z.Usage).Sum() * 100,

                        }).Where(x => x.Процент_ТехПотерь.Value >= 1.5).OrderByDescending(x => x.Процент_ТехПотерь).Take(3).ToList(),


                    }).ToList(),



                }).OrderByDescending(c => c.Slots.Sum(b => b.PGs.Sum(x => x.Процент_ТехПотерь))).Take(5).ToList();



            //Data.ForEach(x => x.PGs.ForEach(c => c.Процент_ТехПотерь.Value.ToString("##.##")));


            if (Data.Count != 0)
            {
                Data.FirstOrDefault().date = "с " + date1 + " по " + date2;

            }



            return View(Data);
            //return View();
        }



        List<DateTime> GetListDate()
        {
            var NOW = DateTime.UtcNow.AddHours(2).Hour;
            List<DateTime> list = new List<DateTime>();

            if (NOW >= 8 & NOW <= 20)
            {
                //var r = DateTime.Parse( DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd") + "08:00:00");
                list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd") + " 08:00:00"));
                list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd") + " 20:00:00"));
                return list;
            }
            else
            {
                if (NOW >= 20 & NOW <= 23)
                {
                    list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd") + " 20:00:00"));
                    list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).AddDays(1).ToString("yyyy-MM-dd") + " 08:00:00"));
                    return list;
                }
                else
                {
                    list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00"));
                    list.Add(DateTime.Parse(DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd") + " 08:00:00"));
                    return list;
                }
            }
        }
    }
}