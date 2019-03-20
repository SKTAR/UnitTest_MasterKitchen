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

namespace XUnitTest_Master_Kitchen._1_Menu
{
    public class Test_Menu_Services
    {
        string responseStr = "";

       
            public class MenuItem
            {
            // [JsonProperty("menuid")]
                public string menuID { get; set; }
                public string menunameTH { get; set; }
                public string menunameEN { get; set; }
                public string category { get; set; }
                public float sellPrice { get; set; }

            }
        

        [Fact]
        public async Task Get_Menu_List_Recieved_2_MenuItem()
        {

            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/GetMenuItems";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }

                  var actual = JsonConvert.DeserializeObject<List<MenuItem>>(responseStr);

                

                // Act

                var expected = new List<MenuItem>
               {
                      new MenuItem()
                    {
                        menuID = "A1001",
                        menunameTH = "ปลาส้ม",
                        menunameEN = "",
                        category = "ของดีเมืองอุบล",
                        sellPrice = 170.00f
                    },
                      new MenuItem()
                    {
                        menuID = "A1002",
                        menunameTH = "หมูยอนึ่ง",
                        menunameEN = "",
                        category = "ของดีเมืองอุบล",
                        sellPrice = 110.00f
                    }
               };
                

                // Assert
                actual
                .Should()
                .BeOfType<List<MenuItem>>()
                .And
                .HaveCount(2)
                .And
                .BeSameAs(expected);

            }

        }


        [Fact]
        public async Task Get_All_MenuItems_()
        {

            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/GetAllMenuItems";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }

                var actual = JsonConvert.DeserializeObject<List<MenuItem>>(responseStr);


                int allMenuCount = 100;  // Assume

                // Act

                var expected = new List<MenuItem>
               {
                    // All Menu Items
               };


                // Assert
                actual
                .Should()
                .BeOfType<List<MenuItem>>()
                .And
                .HaveCount(allMenuCount)
                .And
                .BeSameAs(expected);

            }

        }





    }
}
