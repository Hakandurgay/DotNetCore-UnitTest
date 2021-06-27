using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.APP
{
    public class CalculatorService:ICalculatorService
    {
        public int Add(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return 0;
            }

            if (a == 5) //bu hata fırlartma mock tarafından simüle edilebilir
            {
                throw new Exception();
            }
            return a + b;
        }
    }
}
