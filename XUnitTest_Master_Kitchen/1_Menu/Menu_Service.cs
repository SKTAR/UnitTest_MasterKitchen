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
    public class Menu_Services
    {
        string responseStr = "";


        public class MenuItem
        {   // [JsonProperty("menuid")]
            public string menuID { get; set; }
            public string menunameTH { get; set; }
            public string menunameEN { get; set; }
            public string category { get; set; }
            public float sellPrice { get; set; }
        }


        public class MenuCategory
        {
            public string MenuCategoryID { get; set; }
            public string MenuCateGoryName { get; set; }
        }



        [Fact]
        public async Task GET_Recieved_All_List_Menu_Category()
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

                var actual = JsonConvert.DeserializeObject<List<MenuCategory>>(responseStr);



                // Act
                            
                var expected = new List<MenuCategory>
               {
                      new MenuCategory()
                    {
                        MenuCategoryID = "C1001",
                        MenuCateGoryName = "ของดีเมืองอุบล", // 
                    },
                       new MenuCategory()
                    {
                        MenuCategoryID = "C1002",
                        MenuCateGoryName = "ส้มตำ",
                    },
                       new MenuCategory()
                    {
                        MenuCategoryID = "C1003",
                        MenuCateGoryName = "ทานเล่น ",
                    },
                       new MenuCategory()
                    {
                        MenuCategoryID = "C1004",
                        MenuCateGoryName = "ต้ม",
                    }
                    ,
                       new MenuCategory()
                    {
                        MenuCategoryID = "C1005",
                        MenuCateGoryName = "แกง",
                    },
                      new MenuCategory()
                    {
                        MenuCategoryID = "C1006",
                        MenuCateGoryName = "นึ่ง",
                    },
                     new MenuCategory()
                    {
                        MenuCategoryID = "C1006",
                        MenuCateGoryName = "หมก",
                    }
                     ,
                     new MenuCategory()
                    {
                        MenuCategoryID = "C1007",
                        MenuCateGoryName = "ข้าว",
                    }
                      ,
                     new MenuCategory()
                    {
                        MenuCategoryID = "C1008",
                        MenuCateGoryName = "เครื่องดื่ม",
                    }
                     ,
                     new MenuCategory()
                    {
                        MenuCategoryID = "C1009",
                        MenuCateGoryName = "ขนม",
                    }
               };


                // Assert
                actual
                .Should()
                .BeOfType<List<MenuCategory>>()
                .And
                .HaveCount(9)
                .And
                .BeSameAs(expected);

            }

        }



        [Fact]
        public async Task POST_Recieve_Menu_Item_By_Category()
        {

 

            string postJsonData = @"{  
                                        'MenuCategoryID'  : 'C1007',
                                    }";

            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, new StringContent(postJsonData, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }

               JToken actual = JToken.Parse(responseStr);
            

                // Act

                JToken expected = JToken.Parse(@"{  
                                   'MenuCategoryID'     : 'C1007',
                                   'MenuCateGoryName'   : 'ข้าว',
                                   'MenuItems': [ 
                                                    { 'menuID' : 'M1001' , 'MenuNameTH' : 'ข้าวเหนียว' , 'sellPrice' : 25 }, 
                                                    { 'menuID' : 'M1002' , 'MenuNameTH' : 'ข้าวจ้าว'   , 'sellPrice' : 25 }, 
                                                ]
                                   
                               }");
            
              


                // Assert
                actual
                .Should()
                .BeSameAs(expected);

            }

        }


       





    }
}
