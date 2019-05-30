using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class Payment
    {
        [DataMember(Name = "paymentMethod")]
        public string paymentMethod { get; set; }

        [DataMember(Name = "amount")]
        public double amount { get; set; }

        [DataMember(Name = "cardNumberMasked")]
        public string cardNumberMasked { get; set; }

        [DataMember(Name = "cardExpDate")]
        public string cardExpDate { get; set; }
    }

    [Serializable]
    [DataContract]
    public class letterDeliveryInfo
    {
        [DataMember(Name = "deliveryType")]
        public string deliveryType { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SubscribeAutoCard
    {
        [DataMember(Name = "payment")]
        public Payment payment { get; set; }

        [DataMember(Name = "cyclePeriod")]
        public string cyclePeriod { get; set; }

        [DataMember(Name = "letterDeliveryInfo")]
        public letterDeliveryInfo letterDeliveryInfo { get; set; }

        [DataMember(Name = "notificationDeliveryInfo")]
        public notificationDeliveryInfo notificationDeliveryInfo { get; set; }

        [DataMember(Name = "billDeliveryInfo")]
        public billDeliveryInfo billDeliveryInfo { get; set; }

        [DataMember(Name = "subscriptions")]
        public List<subscriptions> subscriptions { get; set; }

        [DataMember]
        public long accountNo { get; set; }

        [DataMember]
        public string accountType { get; set; }

        [DataMember]
        public PartyObject owner { get; set; }

        [DataMember]
        public PartyObject payer { get; set; }

        [DataMember]
        public int numberOfSubscriptions { get; set; }

        [DataMember]
        public double arBalance { get; set; }

        [DataMember]
        public string linkItId { get; set; }

        [DataMember]
        public string regarding { get; set; }

        [DataMember]
        public bool costCenterMark { get; set; }

        [DataMember]
        public string customerNumber { get; set; }

        [DataMember]
        public int billFreq { get; set; }

        [DataMember]
        public bool isContractCustomer { get; set; }

        [DataMember]
        public bool consumerAssociation { get; set; }

        [DataMember]
        public long cancellationNoticeInDays { get; set; }

        [DataMember]
        public string billingAgent { get; set; }

        [DataMember]
        public string accountSegment { get; set; }

        [DataMember]
        public AccountActivity activity { get; set; }


    }

    [Serializable]
    [DataContract]
    public class notificationDeliveryInfo
    {
        [DataMember(Name = "mobileNo")]
        public string mobileNo { get; set; }

        [DataMember(Name = "notifyViaEmail")]
        public bool notifyViaEmail { get; set; }

        [DataMember(Name = "notifyViaSms")]
        public bool notifyViaSms { get; set; }
    }

    [Serializable]
    [DataContract]
    public class billDeliveryInfo
    {
        [DataMember(Name = "directDebit")]
        public bool directDebit { get; set; }

        [DataMember(Name = "paperCopy")]
        public bool paperCopy { get; set; }

        [DataMember(Name = "deliveryType")]
        public string deliveryType { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SplitBillAccountDetails
    {
        [DataMember(Name = "payerAccountNo")]
        public long payerAccountNo { get; set; }

        [DataMember(Name = "productName")]
        public string productName { get; set; }

        [DataMember(Name = "subscriptionId")]
        public string subscriptionId { get; set; }

    }

    [DataContract]
    public class DownloadFileResponse
    {
        [DataMember]
        public string filename;
        [DataMember]
        public string file;
    }

    [DataContract]
    public class subscriptions
    {
        [DataMember]
        public string parentSegment { get; set; }

    }

    [Serializable]
    [DataContract(Name = "Party")]
    public class PartyObject
    {
        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string co { get; set; }

        [DataMember]
        public string emailAddress { get; set; }

        [DataMember]
        public string floor { get; set; }

        [DataMember]
        public string floorside { get; set; }

        [DataMember]
        public string mobilePhoneNumber { get; set; }

        [DataMember]
        public string phoneNumber { get; set; }

        [DataMember]
        public string placeName { get; set; }

        [DataMember]
        public string locality { get; set; }

        [DataMember]
        public string street { get; set; }

        [DataMember]
        public string streetCode { get; set; }

        [DataMember]
        public string streetnumber { get; set; }

        [DataMember]
        public string apartmentNumber { get; set; }

        [DataMember]
        public string zipCode { get; set; }

        [DataMember]
        public string attentionTo { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string postbox { get; set; }

        [DataMember]
        public string houseLetter { get; set; }

        [DataMember]
        public string houseKey { get; set; }

        [DataMember]
        public string additionalAddress { get; set; }

        [DataMember]
        public string linkItID { get; set; }

        [DataMember]
        public string customerNo { get; set; }

        [DataMember]
        public string muncipality { get; set; }

        [DataMember]
        public string muncipalityCode { get; set; }

        [DataMember]
        public string lid { get; set; }

        [DataMember]
        public string customerHierarchyType { get; set; }

        [DataMember]
        public string adressType { get; set; }

        [DataMember]
        public string lastBusinessType { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AccountActivity
    {
        [DataMember]
        public PaymentInformation paymentInformation { get; set; }

        [DataMember]
        public PaymentInformation refundInformation { get; set; }

        [DataMember]
        public CreditCardInformation creditCardInformation { get; set; }

    }

    [Serializable]
    [DataContract]
    public class PaymentInformation
    {
        [DataMember]
        public string accountNo { get; set; }

        [DataMember]
        public long amount { get; set; }

        [DataMember]
        public int statusCode { get; set; }

        [DataMember]
        public string statusMessage { get; set; }

        [DataMember]
        public int invoiceNo { get; set; }

        [DataMember]
        public string paymentId { get; set; }

        [DataMember]
        public string transactionNo { get; set; }

    }

    [Serializable]
    [DataContract]
    public class CreditCardInformation
    {
        [DataMember]
        public string accountNo { get; set; }

        [DataMember]
        public bool active { get; set; }

        [DataMember]
        public string cardNumber { get; set; }

        [DataMember]
        public string cardPrefix { get; set; }

        [DataMember]
        public string statusMessage { get; set; }

        [DataMember]
        public string ticketId { get; set; }

    }
}

