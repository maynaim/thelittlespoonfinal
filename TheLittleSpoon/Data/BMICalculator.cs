using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLittleSpoon.Data
{
    public static class BMICalculator
    {
        public static BmiReport GetBMIValue (double weight, double hiegth)
        {
            BmiServiceSoapClient bmi = new BmiServiceSoapClient(BmiServiceSoapClient.EndpointConfiguration.BmiServiceSoap);
            Task<getBmiReportResponse> x =  bmi.getBmiReportAsync(weight, hiegth);

            return x.Result.Body.getBmiReportResult;
        }
    }
}
