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
        public enum OrderType
        {
            Dine_in = 0,
            Take_Away = 1,
            Delivery = 2,
            Other = 9

        }
        string responseStr = "";

       
        [Fact]
        public async Task Send_Order_Type_Dine_In()
        {



            String postData = @"{  
                                   
                    'OrderType'          :    1 ,
                    'Table'              :   'T0001',
                    'NumberOfCustomer'   :    5     ,
                    'EmployeeID'         :   'W0001',
                    'OrderMenuItems'     : [   
                                                         
                                             { 
                                               'MenuID'                   : 'M1001' , 
                                               'Quantity'                 :  2 , 
                                               'AdditionalIngredientsID'  :  [ 'I0001' ,'I0002' ]  ,
                                               'Removed'                  :  [ 'I0004' ,'I0005' ]  ,
                                             },
                                             {  
                                               'MenuID'                   : 'M1002' , 
                                               'Quantity'                 :  3     
                        
                                             }
                                           ]
                                   
                               }"; // OrderType = 1 ,  Dine in 





            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, new StringContent(postData, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }



                // Act


                JToken expected = JToken.Parse(@"{  
                                   
                                   'BillNo'             :   '100001',
                                   'OrderType'          :    1 ,
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

        [Fact]
        public async Task Send_Order_Type_Take_Away()
        {



            String postData = @"{  
                                   
                    'OrderType'          :    2 ,
                    'EmployeeID'         :   'W0001',
                    'OrderMenuItems'     : [   
                                                         
                                             { 
                                               'MenuID'                   : 'M1001' , 
                                               'Quantity'                 :  2 , 
                                               'AdditionalIngredientsID'  :  [ 'I0001' ,'I0002' ]  ,
                                               'Removed'                  :  [ 'I0004' ,'I0005' ]  ,
                                             },
                                             {  
                                               'MenuID'                   : 'M1002' , 
                                               'Quantity'                 :  3     
                        
                                             }
                                           ]
                                   
                               }"; // OrderType = 2 ,  Take Away 





            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, new StringContent(postData, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }



                // Act

                JToken expected = JToken.Parse(@"{  
                                   
                                   'BillNo'             :   '200001',
                                   'OrderType'          :    2 ,
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


        [Fact]
        public async Task Send_Order_Type_Delivery()
        {



            String postData = @"{  
                                   
                    'OrderType'          :    3 ,
                    'EmployeeID'         :   'W0001',
                    'CustomerID'         :   'C00001',
                    'Ship Address'       :   '123/456 xxx Road Bangkok 10520',
                    'Sender'             :   'S0001',
                    'OrderMenuItems'     : [   
                                                         
                                             { 
                                               'MenuID'                   : 'M1001' , 
                                               'Quantity'                 :  2 , 
                                               'AdditionalIngredientsID'  :  [ 'I0001' ,'I0002' ]  ,
                                               'Removed'                  :  [ 'I0004' ,'I0005' ]  ,
                                             },
                                             {  
                                               'MenuID'                   : 'M1002' , 
                                               'Quantity'                 :  3     
                        
                                             }
                                           ]
                                   
                               }"; // OrderType = 2 ,  Delivery




            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, new StringContent(postData, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }



                // Act

                JToken expected = JToken.Parse(@"{  
                                   
                                   'BillNo'             :   '300001',
                                   'OrderType'          :    3 ,
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



        [Fact]
        public async Task Send_Order_Type_Others()
        {



            String postData = @"{  
                                   
                    'OrderType'          :    9 ,
                    'EmployeeID'         :   'W0001',
                    'Description'        :   'For Anniversary Banquet',                 
                    'OrderMenuItems'     : [   
                                                         
                                             { 
                                               'MenuID'                   : 'M1001' , 
                                               'Quantity'                 :  2 , 
                                               'AdditionalIngredientsID'  :  [ 'I0001' ,'I0002' ]  ,
                                               'Removed'                  :  [ 'I0004' ,'I0005' ]  ,
                                             },
                                             {  
                                               'MenuID'                   : 'M1002' , 
                                               'Quantity'                 :  3     
                        
                                             }
                                           ]
                                   
                               }"; // OrderType = 9 ,  Others




            using (var httpClient = new HttpClient())
            {
                string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmVlIiwiZXhwIjoxNTgzNTU4MzI1fQ.fnfHszGmXXOwAyLYx5BvAYzB_KdPJdQB6KOyWDcxuMM";
                string uri = "http://localhost:9090/RestfulJava/api/MasterKitchen/SendOrder";
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, new StringContent(postData, Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    responseStr = await response.Content.ReadAsStringAsync();
                }



                // Act

                JToken expected = JToken.Parse(@"{  
                                   
                                   'BillNo'             :   '000003',
                                   'OrderType'          :    3 ,
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
}
