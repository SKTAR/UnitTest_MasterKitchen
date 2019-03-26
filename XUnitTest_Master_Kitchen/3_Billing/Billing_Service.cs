using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest_Master_Kitchen._3_Billing
{
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

    public class Billing_Service
    {
        public enum Bill_Status
        {
            Cleared = 0,   // already paid
            Pending = 1,   // didn't pay
            Held = 2,      // having some problems
            Others = 9     //  

        }

        public enum Payment_Type
        {
            Cash   = 1,
            Debit  = 2,
            Credit = 3,
            Others = 9

        }

        public enum Customer_Type
        {
            New     = 0 ,
            Member  = 1
        }

        [Fact]
        public async Task Confirm_Payment_By_Cash_Type_Dine_In_Existing_Member() // Create Customer ID in Backend
        {


            
            String postData = @"{  
                                   
                    'BillNo'             :   '100001' ,
                    'Customer'           :   { 'CustomerType' : 0 , 'CustomerName' : 'Mr.Surasak Kaewsiri' ,'MobileNo' : '0940826098' },                         
                    'EmployeeID'         :   'W0001',
                    'PaymentType'        :    1,
                    'CashRecieved'       :   500,

                    }"; // PaymentYpe = 1 ,  Cash  // CustomerType = 0 = New Customer

                        // For the promotion ,  We will discuss later 


            string responseStr = "";
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
                                   'BillStatus'         :    0, 
                                   'CustomerID'         :   'C00001',
                                   'DateTime'           :   2012-04-23T18:25:43.511,
                                   'SubTotalPrice'      :   400,
                                   'Tax'                :   28, 
                                   'CashRecieved'       :   500,     
                                   'Changed'            :   72,
                                   
                               }"); // Bill status = 0 , Cleared  , Return Customer ID
                                    
                                    
                JToken actual = JToken.Parse(responseStr);


                // Assert
                actual.Should().BeEquivalentTo(expected);



            }

        }


        [Fact]
        public async Task Confirm_Payment_By_Credit_Type_Dine_In_Member_Customer()
        {



            String postData = @"{  
                                   
                    'BillNo'             :   '100001' ,
                    'Customer'           :   { 'CustomerType' : 'New' , 'CustomerName' : 'Mr.Surasak Kaewsiri' ,'MobileNo' : '0940826098' },                         
                    'PaymentType'        :    3,
                    
                                   
                    }"; // PaymentYpe = 1 ,  Cash  // 




            string responseStr = "";
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
                                   'BillStatus'         :    0, 
                        
                                   'DateTimeCreated'    :    2012-04-23T18:25:43.511
                                   'OrderMenuItems'         :    
                                                        [ 
                                                            { 'MenuID' : 'M1001' , 'SellPrice' : 25 , 'Status' : 'In Process'}, 
                                                            { 'MenuID' : 'M1002' , 'SellPrice' : 25 , 'Status' : 'Queuing}, 
                                                        ]
                                   
                               }"); // Bill status = 0 , Cleared 


                JToken actual = JToken.Parse(responseStr);


                // Assert
                actual.Should().BeEquivalentTo(expected);



            }

        }





    }
}
