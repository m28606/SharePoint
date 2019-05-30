using System.Collections.Generic;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel.Householddatamodel;
using AddOnProduct = TPCIP.CommonDataModel.Householddatamodel.AddOnProduct;

namespace TPCIP.CommonServiceAgents.Fakes
{
    public class HouseholdAgent : IHouseholdAgent
    {
        private List<HouseholdDetails> getFakeHouseHoldDetails()
        {
            return new List<HouseholdDetails>()
            {
                new HouseholdDetails()
                {
                   groupName = "MOBIL TALE",
                   product="Fri Tale 25 GB Ekstra",
                   accountNo="609003082",
                   price="200",
                   upSale=true,
                   subscriptionId="23600506",
                   customerType="Owner",
                   isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                   addOnProduct = new addOnProduct()
                                       {
                           productCode = "BB1234",
                           productName ="FamilieFordel"
                                       }                                  
                               },

                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BR1234",
                           productName ="TDC til TDC"
                                       }                          
                               },
                           },

                       },
                       new ProductRelation
                       {
                            addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                           productCode = "BB1234",
                           productName ="TDC Mobilsikkerhed"
                                       }
                               },

                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                                           productCode = "BR1234",
                           productName ="TDC Hotspot"
                                       }
                               },
                           }
                       }
                   }
                },

                new HouseholdDetails()
                {
                   groupName = "MOBIL TALE",
                   product="Fri Tale 25 GB Ekstra",
                   accountNo="609003082",
                   price="200",
                   upSale=true,
                   subscriptionId="23600506",
                   customerType="Owner",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123433",
                           productName ="YouSee Play Musik"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct =  new addOnProduct()
                                       {
                           productCode = "BR1234",
                           productName ="Fri SMS & MMS"
                                       }
                               },
                           }
                       }
                   }
                },

                new HouseholdDetails()
                {
                   groupName = "MOBIL DATA",
                   product="Mobil Data",
                   accountNo="12345678",
                   price="200",
                   upSale=true,
                   subscriptionId="99182501",
                   customerType="Owner",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123456",
                           productName ="Broadband"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                             productCode = "BB123433",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                             productCode = "BR1234",
                           productName ="Broadband"
                                       }                      
                               },
                           }
                       }
                   }
                },

                new HouseholdDetails()
                {
                   groupName = "Mail",
                   product="YouSee Mail",
                   accountNo="12345678",
                   price="200",
                   upSale=true,
                   subscriptionId="99182501",
                   customerType="Owner",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "AP13_355_17",
                           productName ="YouSee Mail"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                             productCode = "BB123433",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                             productCode = "BR1234",
                           productName ="Broadband"
                                       }                      
                               },
                           }
                       }
                   }
                },


                new HouseholdDetails()
                {
                   groupName = "MOBIL DATA",
                   product="Mobil Data",
                   accountNo="12345678",
                   price="200",
                   upSale=true,
                   subscriptionId="99182501",
                   customerType="Owner",
                    isFutureProduct=true,
                    addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct =  new addOnProduct()
                                       {                            
                           productCode = "TV1234",
                           productName ="TV"
                                       }
                               }
                           }
                       }
                   }
                },

               new HouseholdDetails()
                {
                   groupName = "MOBIL BREDBAND",
                   product="TDC Simply",
                   accountNo="78945659",
                   price="300",
                   upSale=false,
                   subscriptionId="87341127",
                   customerType="User",
                   isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                              productCode = "TV12345",
                           productName ="TV"
                                       }
                               }
                           }
                       }
                   }
                },


                new HouseholdDetails()
                {
                   groupName = "TV",
                   product="Mellem pakke med",accountNo="123785569",price="2000",upSale=false,subscriptionId="YL342500",customerType="User",
                   isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "TV123456",
                           productName ="TV"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123433",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                           productCode = "BR1234",
                           productName ="Broadband"
                                       }
                               },
                           }
                       }
                   }
                },
                

                new HouseholdDetails()
                {
                   groupName = "TV",
                   product="Mellem pakke med",accountNo="779455699",price="3000",upSale=false,subscriptionId="YL342500",customerType="Owner",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                              productCode = "TV1234",
                           productName ="FamilieFordel"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "TV12982",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct =new addOnProduct()
                                       {
                           productCode = "TV12435",
                           productName ="Broadband"
                                       }
                               },
                           }
                       }
                   }
                },

                new HouseholdDetails()
                {
                   groupName = "TV",
                   product="NAT GEO",accountNo="650403733",price="7000",upSale=false,subscriptionId="YC201007",customerType="User",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "TV23456",
                           productName ="TV"
                                       }
                               }
                           }
                       }
                   }
                   
                }, 
               
                 new HouseholdDetails()
                {
                   groupName = "TV",
                   product="NAT GEO",accountNo="203649801",price="7000",upSale=false,subscriptionId="EM120268",customerType="User", isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct =  new addOnProduct()
                                       {
                            productCode = "TV123433",
                           productName ="Broadband"
                                       }
                               }
                           }
                       }
                   }
                },
             
                  new HouseholdDetails()
                {
                   groupName = "Bredband",
                   product="Bredband BlandSelv 50/10 Mbit,Coax",accountNo="127378566",price="2000",upSale=true,subscriptionId="YL839295", isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123478",
                           productName ="Broadband"
                                       }
                               }
                           }
                       }
                   }
                },
               

                new HouseholdDetails()
                {
                   groupName = "Bredband",
                   product="Bredband BlandSelv 50/10 Mbit,Coax",accountNo="127378566",price="2000",upSale=true,subscriptionId="YL839295",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                              productCode = "DT12345",
                           productName ="Data"
                                       }
                               }
                           }
                       },

                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123433",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct =  new addOnProduct()
                                       {
                           productCode = "BR1234",
                           productName ="Broadband"
                                       }
                               },
                           }
                       }
                   }

                },

                new HouseholdDetails()
                {
                   groupName = "Bredband",
                   product="pro4",accountNo="997485688",price="2000",upSale=true,subscriptionId="EM115311",customerType="User",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB19923433",
                           productName ="Broadband"
                                       }
                               }
                           }
                       }
                   }

                },

                 new HouseholdDetails()
                {
                   groupName = "Bredband",
                   product="pro4",accountNo="997485678",price="2000",upSale=true,subscriptionId="EM115311",customerType="User", isFutureProduct=true,
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123478",
                           productName ="Broadband"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB13333",
                           productName ="Broadband"
                                       }
                               },
                                new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                           productCode = "BR1234",
                           productName ="Broadband"
                                       }
                               },
                           }
                       }

                   }

                },

                new HouseholdDetails()
                {
                   groupName = "Fastnet",
                  product="product1",accountNo="123785682",price="2000",upSale=null,subscriptionId="9918250",customerType="Owner",
                  addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                                           productCode = "FN1234",
                             productName ="Duet sÃ¸gningsrÃ¦kkefÃ¸lge",
                                       }
                               }
                           }
                       }
                   }

                },

               new HouseholdDetails()
                {
                   groupName = "Fastnet",
                   product="product3",accountNo="125456291",price="7000",upSale=null,subscriptionId="EM120268",customerType="User",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "FN12345",
                           productName ="ekstranummer"
                                       }
                               }
                           }
                       }
                   }

                },

                new HouseholdDetails()
                {
                   groupName = "Telefoni",
                   product="product4",accountNo="997456891",price="2000",upSale=null,subscriptionId="EM115311",customerType="User",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                             productCode = "BB123477",
                           productName ="Broadband"
                                       }
                               }
                           }
                       },
                         new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {productCode = "Br123433",
                           productName ="Broadband"
                                       }
                               },
                               
                           }
                       }
                   }

                },

                new HouseholdDetails()
                {
                   groupName = "YouSee",
                   product="product1",accountNo="203621955",price="2000",upSale=true,subscriptionId="87341127",customerType="Owner",
                   addOnProductList = new List<ProductRelation>()
                   {
                       new ProductRelation()
                       {
                           addOnProducts = new List<AddOnProduct>()
                           {
                               new AddOnProduct()
                               {
                                    addOnProduct = new addOnProduct()
                                       {
                            productCode = "BB123400",
                           productName ="Broadband"
                                       }
                               }
                           }
                       }
                   }                
            }
        };
        }

        private List<ProductAttentionMarker> getFakeAttentionMarker()
        {
            return new List<ProductAttentionMarker>() { 
            new ProductAttentionMarker(){
            productName="YouSee Mobil",
            subscriptionNo="9918250",
            isTheftLocked=true,
            },
            new ProductAttentionMarker(){
            productName="Mobil Data",
            subscriptionNo="99182501",
            isTheftLocked=true,
            },
            new ProductAttentionMarker(){
            productName="YouSee Mobil",
            subscriptionNo="9918250",
            isTheftLocked=false,
            },

             new ProductAttentionMarker(){
            productName="TDC Simply",
            subscriptionNo="87341127",
            
            },

             new ProductAttentionMarker(){
            productName="AXN",
            subscriptionNo="9918250",
            isTheftLocked=false,
            },

            };
        }

        public virtual CustomerOverviewProducts GetCustomerOverviewDetails(string lid, long accountNo)
        {
            if (lid == "EM115311")
            {
                return new CustomerOverviewProducts()
                {
                    message = "Kunden har for mange produkter til at kunne indlæse overblik",
                    HouseholdDetails = null,
                };
            }
            else
            {
                return new CustomerOverviewProducts()
                {
                    ysMoreOptIn = true,
                    isYouseeMoreCustomer = true,
                    totalBenefitSlots = 2,
                    isTVCategoryBenefitSubscribed = true,
                    isBBCategoryBenefitSubscribed = true,
                    isMobilCategoryBenefitSubscribed = false,
                    message = "",
                    HouseholdDetails = getFakeHouseHoldDetails()

                };
            }
        }

        public virtual List<ProductAttentionMarker> GetProductAttentionMarker(string lid)
        {
            return getFakeAttentionMarker();
        }

        public virtual SimpleResult<bool> GetSplitBillingDetails(string subscriptionId)
        {
            if (subscriptionId == "99182501")
            {
                return new SimpleResult<bool>() { value = true };
            }
            else
            {
                return new SimpleResult<bool>() { value = false }; ;
            }
        }
    }
}
