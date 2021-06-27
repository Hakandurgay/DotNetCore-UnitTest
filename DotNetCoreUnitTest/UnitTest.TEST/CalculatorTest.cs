using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using UnitTest.APP;
using Xunit;

namespace UnitTest.TEST
{
    public class CalculatorTest
    {
        public Calculator calculator { get; set; }
        public Mock<ICalculatorService> myMock { get; set; }
        public CalculatorTest()
        {
            myMock = new Mock<ICalculatorService>();//ICalculator servisinin implemente ettiği calculatorservice taklit edilecek
            this.calculator=new Calculator(myMock.Object);
        }

        [Fact] //metod parametre almazsa fact attribute'u kullanılır. Ve test metodu olduğunu bildirir
        public void AddTest()
        {
            #region ilk ornek
            //Arrange  //değişkenlerin veya nesnelerin oluşturulduğu yer
            int a = 5;
            int b = 20;
            
            //Act
            var total = calculator.Add(a, b);

            //Assert doğrulama evresi. accteki sonucun doğru olup olmadığının test edildiği yer
            //Assert.Equal<int>(25, total); //beklenen değer, gelen değer
             Assert.NotEqual<int>(25,total); 
            #endregion

            #region contains ve doesnotcontains kullanımı

            //ikinci parametrede birincinin olup olmadığına bakar
            Assert.Contains("Hakan", "Hakan Durgay"); //ilk parametre beklenen, ikinicisi testten dönen

            var names = new List<string>() { "hakan", "ahmet", "mehmet" };
            //bu şekilde de kullanılabilir
            Assert.Contains(names, x => x == "hakan");

            //ikinci parametrede ilkinin olmamasını bekler
            //   Assert.DoesNotContain("ahmet", "Hakan Durgay");
            #endregion

            #region true-false kullanımı

            Assert.True(5<2);
            Assert.False(5<2);
            Assert.True("".GetType() == typeof(string));

            #endregion

            #region match- doesnotmatch kullanımı
            //regex ifadesi alır, beklediğimiz değer ile uyuyorsa true döner
            var regex = "^dog";  //dog ile başlamasa gerek
            Assert.Matches(regex,"dog hayvandır"); //regexe uyduğu için true döner

            #endregion

            #region startwith-endwith kullanımı
            Assert.StartsWith("hakan","hakan adım" ); //onunla başlayıp başlamadığını gösterir
            Assert.EndsWith("hakan","adım hakan" ); //onunla başlayıp başlamadığını gösterir
            #endregion

            #region Empty-NotEmpty kullanımı
            Assert.Empty(new List<string>()); //verilen dizinin dolu olup olmadığına bakar
            Assert.NotEmpty(new List<string>()); //boş olmamasını kontrol eder
            #endregion

            #region InRange-NotInrange kullanımı
            Assert.InRange(10,2,20);// ilk girilen değerin iki ve üçüncü değer aralığında olup olmadğını kontrol eder
            Assert.NotInRange(10,2,20);// ilk girilen değerin iki ve üçüncü değer aralığında olup olmadğını kontrol eder
            #endregion

            #region Single metodu
            Assert.Single<string>(new List<string>() {"Hakan"}); //tek elemanı olup olmadığına bakar
            #endregion

            #region IsType metodu
            Assert.IsType<string>("hakan");//içine girilen değerin tipinin generic tip değer ile aynı olup olmadığına bakar
            Assert.IsNotType<int>("hakan");
            #endregion

            #region IsAssignableFrom kullanımı

            //list IEnumerable'dan miras adlığı için true döner
            Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>()); //bir tipin bir tipe referans verilip verilmeyeceğini kontrol eder
            Assert.IsAssignableFrom<object>(2); //int object'ten miras aldığı için true döner

            #endregion

            #region Null-NotNull kullanımı
            string deger = null;
            Assert.Null(deger);//true
            Assert.NotNull(deger);//true
            #endregion

            #region Equal-NotEqual
            Assert.Equal<int>(2,2);
            #endregion
        }

        [Theory]
        [InlineData(2,5,7)] //parametre böyle verilir
        [InlineData(10, 2, 12)]
        public void AddTest2(int a, int b, int expedtedTotal)
        {
            var actualTotal = calculator.Add(a, b);
            Assert.Equal(expedtedTotal,actualTotal);
        }

        [Theory]
        [InlineData(2, 5, 7)] //parametre böyle verilir
        [InlineData(10, 2, 12)]
        public void AddTestMoq(int a, int b, int expedtedTotal)
        {
            myMock.Setup(x => x.Add(a, b)).Returns(expedtedTotal);  //Add metodunun içine girmeden o metodu taklit eder. 
            myMock.Verify(X=>X.Add(a,b),Times.Once); //metodun sadece bir kere çalışıp çalışmamasını test eder
            //myMock.Verify(X => X.Add(a, b), Times.AtLeast(2));//en az iki kere çağrılıp çağrılmadığını test eder
        }
        
        [Theory]
        [InlineData(5, 5)] 

        public void AddTestMoqFiveValueTest(int a, int b)
        {
            myMock.Setup(x => x.Add(a, b)).Throws(new Exception("hata"));
         Exception exception=   Assert.Throws<Exception>(() => calculator.Add(a, b));
            Assert.Equal("hata",exception.Message);
        }



        //metodlar isimlendirilirken aşağıdaki gibi olabilir
        //  [MethodName_StateUnderTest_ExpectedBehavior] //metod adı, neyi kontrol ettiği, döndüreceğin şey
        //  Add_SimpleValues_ReturnTotalValue

    }
}
