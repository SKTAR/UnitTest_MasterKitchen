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
    public class Order_Food_Test
    {
        string responseStr = "";

        public enum OrderType
        {
            Dine_in = 0,
            Take_Away = 1,
            Delivery = 2,
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

            public string EmployeeID { get; set; }

            public int NumOfCust { get; set; }  // Number of customer  3 people
            public List<Order_Food_Items> FoodList { get; set; }

            public string Additional_Ingredient { get; set; }

        }




        [Fact]
        public async Task Send_Order_Type_Dine_In()
        {

             var payload = new Order_Food
                             {

                                 TypeOfOrder = OrderType.Dine_in,   // ประเภท การสั่ง   Eat in = 0 
                                 TableID = "0001",                // หมายเลขโต๊ะ [ 0001 ] ที่สั่ง  ส่งไปแล้ว  backend จะรู้ว่า จะส่งคำสั่งไปที่ station ไหน  
                                 NumOfCust = 3,                   // จำนวน  ลูกค้า 
                                 EmployeeID   = "W0001",
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
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
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



                // Act


                // Or 


                JToken expected = JToken.Parse(@"{  
                                   
                                   'BillNo'             :   '000001',
                                   'OrderType'          :    0 ,
                                   'BillStatus'         :   'Pending',
                                   'Station'            :   'Kitchen',
                                   'DateTimeCreated'    :    2012-04-23T18:25:43.511
                                   'OrderMenuItems'         :    
                                                        [ 
                                                            { 'MenuID' : 'M1001' , 'SellPrice' : 25 , 'Status' : 'In Process'}, 
                                                            { 'MenuID' : 'M1002' , 'SellPrice' : 25 , 'Status' : 'Queuing}, 
                                                        ]
                                   
                               }");


                JToken actual = JToken.Parse(responseStr);


                // Assert
                actual.Should().BeEquivalentTo(expected);



            }








        }
    }
}
