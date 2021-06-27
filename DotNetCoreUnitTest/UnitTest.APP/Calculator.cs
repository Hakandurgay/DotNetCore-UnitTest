using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.APP
{
    public class Calculator
    {
        public ICalculatorService _CalculatorService { get; set; }
        public Calculator(ICalculatorService calculatorService)  //Mock yapısının uygulanması için DI prensibinin uygulanması gerek
        {
            this._CalculatorService = calculatorService;
        }
        public int Add(int a, int b)
        {
            return _CalculatorService.Add(a, b);
        }
    }
}
