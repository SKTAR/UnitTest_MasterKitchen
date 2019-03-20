using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace XUnitTest_Master_Kitchen._2_Order_Food
{
    public class Test_Order_Food_Service
    {
        string responseStr = "";

        public enum OrderType
        {
            Eat_in = 0,
            Take_Away=1,
            Delivery=2,
            Other = 3
            
        }
        public class Order_Food_Items
        {
            public string menuID { get; set; }
            public int quantity { get; set; }
         
        }
        public class Order_Food
        {
            public OrderType TypeOfOrder { get; set; }
            public string TableID { get; set; }

            public string WaiterID { get; set; }

            public int NumOfCust { get; set; }  // Number of customer  3 people
            public List<Order_Food_Items> FoodList { get; set; }

            public string Additional_Ingredient { get; set; }
           
        }


        public class Bill_Order
        {

            [JsonProperty("orderid")]  
            public string OrderID { get; set; }

            [JsonProperty("ordertype")]
            public OrderType TypeOfOrder { get; set; }

            [JsonProperty("foodlist")]
            public List<Order_Food_Items> foodList { get; set; }

           
        }

        [Fact]
        public async Task Send_Order_To_Station()
        {


            var payload = new Order_Food
            {

                TypeOfOrder = OrderType.Eat_in,   // ประเภท การสั่ง   Eat in = 0 
                TableID = "0001",                // หมายเลขโต๊ะ [ 0001 ] ที่สั่ง  ส่งไปแล้ว  backend จะรู้ว่า จะส่งคำสั่งไปที่ station ไหน  
                NumOfCust = 3,                   // จำนวน  ลูกค้า 

                FoodList = new List<Order_Food_Items>()
                {
                    new Order_Food_Items()
                    {
                        menuID = "A1001",       // ปลาส้ม , category = "ของดีเมืองอุบล",  sellPrice = 170.00f
                       
                        quantity = 2
                    },
                    new Order_Food_Items()
                    {
                        menuID = "A1002",       // หมูยอนึ่ง , category = "ของดีเมืองอุบล",  sellPrice = 110.00f
                        quantity = 1
                    }
                }

            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrderToStation";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, httpContent);


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }

                //  var actual = JsonConvert.DeserializeObject<List<MenuItem>>(responseStr);


                // Act

                JToken expected = JToken.Parse(@"{ ""orderId"": ""1234567""}");
                
                JToken actual = JToken.Parse(@"{ ""orderId"": ""1234567"" }");


                // Assert
                actual.Should().BeEquivalentTo(expected);



            }








        }
        }
}
