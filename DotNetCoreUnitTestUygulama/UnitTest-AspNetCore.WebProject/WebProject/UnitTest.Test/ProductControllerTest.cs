using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Moq;
using WebProject.Controllers;
using WebProject.Models;
using WebProject.Repository;
using Xunit;

namespace UnitTest.Test
{

    public class ProductControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;

        private readonly ProductsController _controller;

        private List<Product> products;
        public ProductControllerTest()
        {
                _mockRepo=new Mock<IRepository<Product>>();
                _controller=new ProductsController(_mockRepo.Object);
                products=new List<Product>{new Product{Id = 1,Name = "Silgi",Price=200,Stock = 100,Color = "Mavi"}};
        }
        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);    //result'ın viewresult olup olmadığına bakar
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnProductList()
        {
            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(products);  //getall metodu çalıştığında yukarıda oluşturulan fake product döner
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result); //tipin viewresult olup olmadığına bakar
            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model); //atanabilir bir nesne dönüyor mu ona bakar 
            Assert.Equal<int>(1,productList.Count()); //dönen sayıya bakar
        }
        [Fact]
        public async void Details_IdIsNull_ReturnRedirectToIndexAction()
        {
            var result = await _controller.Details(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index",redirect.ActionName);
        }

        [Fact]
        public async void Details_IsIdValid_ReturnNotFound()
        {
            Product product = null;
            _mockRepo.Setup(x => x.GetById(0)).ReturnsAsync(product);
            var result = await _controller.Details(0);
            var redirect = Assert.IsType<NotFoundResult>(result);
            Assert.Equal<int>(404,redirect.StatusCode);
        }

        [Fact]
        [InlineData(1)]
        public async void Details_ValidId_ReturnProduct(int productId)
        {
            Product product = products.First(x => x.Id == productId);
            _mockRepo.Setup(repo => repo.GetById(productId)).ReturnsAsync(product);
            var result = await _controller.Details(productId); 
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal(product.Id,resultProduct.Id);
        }

        [Fact]
        public async void Create_InvalidModelState_ReturnView()
        {
            _controller.ModelState.AddModelError("hakan","hakan"); //metodda modelstate.isvalid kontrol edilidği için invalid durumda düzgün çalışması kontrol ediliyir. Fake hata oluşturuldu
            var result = await _controller.Create(products.First()); //create metoduna atanan değer burada önemsiz çünkü nasılsa hataya düşecek
            var viewResult = Assert.IsType<ViewResult>(result); //dönen type'in viewresult olup olmadığı kontrol ediliyor
            Assert.IsType<Product>(viewResult.Model); //viewResult'ın product olup olmadığı kontrol ediliyor


        }
    }
}
