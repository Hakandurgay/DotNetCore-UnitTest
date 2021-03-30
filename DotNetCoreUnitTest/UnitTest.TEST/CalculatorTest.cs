using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.APP;
using Xunit;

namespace UnitTest.TEST
{
    public class CalculatorTest
    {
        [Fact]
        public void AddTest()
        {
            //Arrange  //değişkenlerin veya nesnelerin oluşturulduğu yer
            int a = 5;
            int b = 20;
            var calculator = new Calculator();

            //Act
            var total = calculator.Add(a, b);

            //Assert doğrulama evresi. accteki sonucun doğru olup olmadığının test edildiği yer
            Assert.Equal<int>(25,total); //beklenen değer, gelen değer
           // Assert.NotEqual<int>(25,total); 

        }
    }
}
