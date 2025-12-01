using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqedCalculator
{
    public interface ICalculator
    {
        double Add(double left, double right);
        double Subtract(double left, double right);
        double Multiply(double left, double right);
        double Divide(double left, double right);

        double ConvertUsdToChf(double usd);
        double ConvertChfToUsd(double chf);
    }
}
