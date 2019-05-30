using System;
using System.Collections.Generic;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.Instrumentation.ExtensionMethods;

namespace TPCIP.CommonServiceAgents
{
    public class SubscriptionAgent : ISubscriptionAgent
    {
        private string _lastSubscriptionId;

        protected virtual List<Subscription> GetFakeSubscriptions(string customerId)
        {
            if (string.IsNullOrEmpty((customerId))) throw new ArgumentNullException("customerId");

            var result = new List<Subscription>();

            if (customerId == "123" || customerId == "NONPRIV")
            {
                result.Add(
                    new Subscription
                    {
                        rootLid = customerId,
                        user = new Party() { firstName = "*Daniel", lastName = "*Turan", customerNo = "22222222", street = "*Plynarenska", streetnumber = "7", floor = "4", floorside = "5", city = "Bratislava", zipCode = "94526", emailAddress = "abc@domain.com" },
                        parentAccountNo = 2031622,
                        columbusCustomerNumber = "1234567891"
                    }
                );
            }
            else if (customerId == "64715008" || customerId =="YL123456")
            {
                result.AddRange(new List<Subscription>()
                {
                    //Added House no./letter
                    new Subscription {   columbusCustomerNumber = "1234567891",rootLid = customerId, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = customerId, street = "*Romanova", streetnumber = "10", floor = "2", floorside = "B", city = "Bratislava", zipCode = "94526", houseLetter="BirlaHouse", emailAddress = "aaa@domain.com", customerHierarchyType = "Enkel"  }, parentAccountNo = 2031713,parentSegment = "PRIV"},
                    new Subscription {columbusCustomerNumber = "1234567891",rootLid = customerId, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = customerId, street = "*Romanova", streetnumber = "10", floor = "2", floorside = "B", city = "Bratislava", zipCode = "94526", houseLetter="TataHouse", emailAddress = "bbb@domain.com", customerHierarchyType = "Enkel"}, parentAccountNo = 2031713, parentSegment = "PRIV"},
                    new Subscription {columbusCustomerNumber = "1234567891",rootLid = "123", user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Mileticova", streetnumber = "47", floor = "18", floorside = "", city = "Bratislava", zipCode = "94526",  houseLetter="Fakehouse", emailAddress = "ccc@domain.com", customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"}
                });
            }

            else
            {
                result.AddRange(new List<Subscription>()
                {
                    new Subscription {rootLid = customerId, payer = new Party() {linkItID = "0981-02-12-12.26.24.720894", emailAddress = "zzz@domain.com", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J", zipCode = "8310"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J",mobilePhoneNumber="66345678", zipCode = "8310", emailAddress = "aaa@domain.com", customerHierarchyType = "Enkel"}, parentAccountNo = 2031713,parentSegment = "PRIV",columbusCustomerNumber="123456789123456", permissions = new List<Permissions>() { 
                        new Permissions{ permissionItemId = "PRIVATE#DEAL#200#TT" , permissionParamValue = ""},
                        new Permissions{ permissionItemId = "PRIVATE#DEAL#100#TE" , permissionParamValue = "emas@tdc.dk"}
                    } 
                    },
                    new Subscription {rootLid = customerId, payer = new Party() {emailAddress = "bbb@domain.com", street = "Romanova", streetnumber = "10", city = "Bratislava", zipCode = "9456"}, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = customerId, street = "Romanova", streetnumber = "10", floor = "2A", city = "Bratislava", mobilePhoneNumber="12345678", zipCode = "9456",houseLetter="12", emailAddress = "bbb@domain.com", customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"},
                    new Subscription {rootLid = "123", payer = new Party() {emailAddress = "ccc@domain.com", street = "Mileticova", streetnumber = "37", city = "Bratislava", zipCode = "9426"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Mileticova", streetnumber = "37", floor = "18", floorside = "A", city = "Bratislava", zipCode = "9426", emailAddress = "ccc@domain.com",customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"},
                
                    new Subscription {rootLid = customerId, payer = new Party() {emailAddress = "zzz@domain.com", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J", zipCode = "8310"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J", zipCode = "8310", emailAddress = "aaa@domain.com", customerHierarchyType = "Enkel"}, parentAccountNo = 2031713,parentSegment = "PRIV", },
                    new Subscription {rootLid = customerId, payer = new Party() {emailAddress = "bbb@domain.com", street = "Romanova", streetnumber = "10", city = "Bratislava", zipCode = "9456"}, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = customerId, street = "Romanova", streetnumber = "10", floor = "2A", city = "Bratislava",mobilePhoneNumber="92345678", zipCode = "9456",houseLetter="12", emailAddress = "bbb@domain.com", customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"},
                    new Subscription {rootLid = "123", payer = new Party() {emailAddress = "ccc@domain.com", street = "Mileticova", streetnumber = "37", city = "Bratislava", zipCode = "9426"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Mileticova", streetnumber = "37", floor = "18", floorside = "A", city = "Bratislava", zipCode = "9426", emailAddress = "ccc@domain.com",customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"},
                
                    new Subscription {rootLid = customerId, payer = new Party() {emailAddress = "zzz@domain.com", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J",mobilePhoneNumber="92345678", zipCode = "8310"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Sletvej", streetnumber = "30", city = "Tranbjerg J", zipCode = "8310", emailAddress = "aaa@domain.com", customerHierarchyType = "Enkel"}, parentAccountNo = 2031713,parentSegment = "PRIV", },
                    new Subscription {rootLid = customerId, payer = new Party() {emailAddress = "bbb@domain.com", street = "Romanova", streetnumber = "10", city = "Bratislava", zipCode = "9456"}, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = customerId, street = "Romanova", streetnumber = "10", floor = "2A", city = "Bratislava", zipCode = "9456",houseLetter="12", emailAddress = "bbb@domain.com", customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"},
                    new Subscription {rootLid = "123", payer = new Party() {emailAddress = "ccc@domain.com", street = "Mileticova", streetnumber = "37", city = "Bratislava", zipCode = "9426"}, user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "Mileticova", streetnumber = "37", floor = "18", floorside = "A", city = "Bratislava", zipCode = "9426", emailAddress = "ccc@domain.com",customerHierarchyType = "Enkel" }, parentAccountNo = 2031713, parentSegment = "PRIV"}
                });
                //result.Add(new Subscription
                //{
                //    rootLid = customerId,
                //    subscriptionId = customerId,
                //    user =
                //        new Party()
                //        {
                //            firstName = "*Jessica",
                //            lastName = "*Alba",
                //            customerNo = customerId,
                //            street = "Prievozka",
                //            streetnumber = "22",
                //            floor = "3",
                //            floorside = "D",
                //            city = "Bratislava",
                //            zipCode = "94526"
                //        },
                //    parentAccountNo = 2031713,
                //    parentSegment = "NONPRIV"
                //});

            }


            foreach (var subscription in result)
            {
                subscription.products = new List<Product>();
                subscription.products.Add(new Product { productParameters = new List<ProductParameter>() });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "PA_INTERNET_NO", paramValue = "*fakeInternetNo" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "PA_LID", paramValue = "875983" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "87341127" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "PA_INTERNET_NO", paramValue = "MSISDN_33" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "PA_LID_LIST", paramValue = "1111111,87341127" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "MOBILE_NUMBER_MSISDN_PARAMCODE", paramValue = "87341126,33333333" });
                subscription.products[0].productParameters.Add(new ProductParameter { paramCode = "PA_TVSIK", paramValue = "*11221122" });
                subscription.products[0].productName = "Parent_Product";
                subscription.products[0].productCode = "1234";
                subscription.products[0].prices = new List<ProductPrices>();
                subscription.products[0].prices.Add(new ProductPrices { rate = 11.11M });
                subscription.products[0].productRelations = new List<ProductRelations>();
                subscription.products[0].productRelations.Add(new ProductRelations());
                subscription.products[0].productRelations[0].addOnProducts = new List<AddOnProduct>();
                subscription.products[0].productRelations[0].addOnProducts.Add(new AddOnProduct());
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level1",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),

                };
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(
                    new ProductParameter { paramCode = "PA_INTERNET_NO", paramValue = "fakeInternetNo" });
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(
                    new ProductParameter { paramCode = "PA_LID", paramValue = "fakevaluePA" });
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });


                #region level2
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations = new List<ProductRelations>();
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations.Add(new ProductRelations());
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts = new List<AddOnProduct>();
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts.Add(new AddOnProduct());
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level2",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),

                };
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue2" });
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID_LIST", paramValue = "fakevaluePA" });
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion Level2

                #region level3

                var parentProduct3 = subscription.products[0].productRelations[0].addOnProducts[0].addOnProduct.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct3.productRelations = new List<ProductRelations>();
                parentProduct3.productRelations.Add(new ProductRelations());
                parentProduct3.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct3.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct3.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level3",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),

                };
                parentProduct3.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue3" });
                parentProduct3.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevaluePA" });
                parentProduct3.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct3.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });


                #endregion level3

                #region level4
                var parentProduct4 = parentProduct3.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct4.productRelations = new List<ProductRelations>();
                parentProduct4.productRelations.Add(new ProductRelations());
                parentProduct4.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct4.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct4.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level4",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),

                };
                parentProduct4.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue4" });
                parentProduct4.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_INTERNET_NO", paramValue = "fakevaluePA" });
                parentProduct4.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct4.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });
                #endregion level4

                #region level5
                var parentProduct5 = parentProduct4.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct5.productRelations = new List<ProductRelations>();
                parentProduct5.productRelations.Add(new ProductRelations());
                parentProduct5.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct5.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct5.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level5",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),

                };
                parentProduct5.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue5" });
                parentProduct5.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID_LIST", paramValue = "fakevaluePA" });
                parentProduct5.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct5.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });
                #endregion level5


                #region level6
                var parentProduct6 = parentProduct5.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct6.productRelations = new List<ProductRelations>();
                parentProduct6.productRelations.Add(new ProductRelations());
                parentProduct6.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct6.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct6.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level6",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),
                };
                parentProduct6.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue" });
                parentProduct6.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID", paramValue = "fakevaluePA" });
                parentProduct6.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct6.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion level6

                #region level7
                var parentProduct7 = parentProduct6.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct7.productRelations = new List<ProductRelations>();
                parentProduct7.productRelations.Add(new ProductRelations());
                parentProduct7.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct7.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct7.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level7",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),
                };
                parentProduct7.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue" });
                parentProduct7.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID", paramValue = "fakevaluePA" });
                parentProduct7.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct7.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion level7

                #region level8
                var parentProduct8 = parentProduct7.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct8.productRelations = new List<ProductRelations>();
                parentProduct8.productRelations.Add(new ProductRelations());
                parentProduct8.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct8.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct8.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level8",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),
                };
                parentProduct8.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue" });
                parentProduct8.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID_LIST", paramValue = "fakevaluePA" });
                parentProduct8.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct8.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion level8

                #region level9
                var parentProduct9 = parentProduct8.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct9.productRelations = new List<ProductRelations>();
                parentProduct9.productRelations.Add(new ProductRelations());
                parentProduct9.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct9.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct9.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level9",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),
                };
                parentProduct9.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue" });
                parentProduct9.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_INTERNET_NO", paramValue = "fakevaluePA" });
                parentProduct9.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct9.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion level9

                #region level10
                var parentProduct10 = parentProduct9.productRelations[0].addOnProducts[0].addOnProduct;
                parentProduct10.productRelations = new List<ProductRelations>();
                parentProduct10.productRelations.Add(new ProductRelations());
                parentProduct10.productRelations[0].addOnProducts = new List<AddOnProduct>();
                parentProduct10.productRelations[0].addOnProducts.Add(new AddOnProduct());
                parentProduct10.productRelations[0].addOnProducts[0].addOnProduct = new Product
                {
                    productName = "Level10",
                    productRelations = new List<ProductRelations>(),
                    productParameters = new List<ProductParameter>(),
                };
                parentProduct10.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "MSISDN", paramValue = "fakevalue" });
                parentProduct10.productRelations[0].addOnProducts[0].addOnProduct.productParameters.Add(new ProductParameter { paramCode = "PA_LID", paramValue = "fakevaluePA" });
                parentProduct10.productRelations[0].addOnProducts[0].addOnProduct.prices = new List<ProductPrices>();
                parentProduct10.productRelations[0].addOnProducts[0].addOnProduct.prices.Add(new ProductPrices { rate = 11.11M });

                #endregion level10

            }

            return result;
        }

        public virtual List<Subscription> searchSubscriptions(string subscriptionId)
        {
            _lastSubscriptionId = subscriptionId;
            return GetFakeSubscriptions(subscriptionId);
        }


        public virtual List<Subscription> searchSubscriptionsByAccountNo(string accountNo)
        {
            if (accountNo == "987654321" || accountNo == "9876543" || accountNo == "987654")
            {
                return new List<Subscription>();
            }
            else
            {
                return new List<Subscription>()
                {
                    new Subscription {rootLid = string.IsNullOrEmpty(_lastSubscriptionId)? "1234": _lastSubscriptionId, user = new Party() {firstName = "*Jozko", lastName = "*Mrkvicka", customerNo = accountNo, street = "*Romanova", streetnumber = "10", emailAddress = "test@test.dk", floor = "2", floorside = "B", city = "Bratislava", zipCode = "94526"}, parentAccountNo = long.Parse(accountNo), parentSegment = "NONPRIV"},
                    new Subscription {rootLid = "123", user = new Party() {firstName = "*Jozko Sr", lastName = "*Mrkvicka", customerNo = accountNo, street = "*Romanova", streetnumber = "10", floor = "2", emailAddress = "test@test.sk", floorside = "B", city = "Bratislava", zipCode = "94526"}, parentAccountNo = long.Parse(accountNo), parentSegment = "NONPRIV"},
                    new Subscription {rootLid = "99181250", user = new Party() {firstName = "*Marian", lastName = "*Miezga", customerNo = "23456789", street = "*Mileticova", streetnumber = "47", floor = "18", floorside = "", city = "Bratislava", zipCode = "94526"}, parentAccountNo = long.Parse(accountNo), parentSegment = "NONPRIV"}
                };
            }
        }

        public virtual List<Subscription> searchSpecificSubscriptions(string subscriptionId, string accountNo)
        {
            _lastSubscriptionId = subscriptionId;
            return GetFakeSubscriptions(subscriptionId);
        }

        public virtual SimpleResult<string> getAccountType(string accountNo)
        {
            var strings = new List<string>() { "Yousee I", "Yousee O", "TDC Erhverv", "Yousee", null };
            var randomString = strings.PickRandom();
            return new SimpleResult<string>() { value = randomString };
        }
    }
}

