using System;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Data;

namespace TheLittleSpoon.Controllers
{
    public class BMIController : Controller
    {
        public IActionResult BMI(Models.BMIFormData data)
        {
            data.BMIReport = BMICalculator.GetBMIValue(data.UserWeight, data.UserHieght);
            data.BMIReport.BmiValue = Math.Round(data.BMIReport.BmiValue, 2);

            ViewBag.BmiResult = data.BMIReport.BmiValue;
            ViewBag.BmiDesc = data.BMIReport.BmiDesc;

            return View();
        }
    }
}