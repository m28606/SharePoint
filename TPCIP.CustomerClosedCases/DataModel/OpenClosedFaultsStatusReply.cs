using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.CustomerClosedCases.DataModel
{
    [Serializable]
    [DataContract]
    public class OpenClosedFaultsStatusReply
    {
        [DataMember(Name = "RequestType")]
        public string requestType { get; set; }

        [DataMember(Name = "DaysBack")]
        public string daysBack { get; set; }

        [DataMember(Name = "QueryParameter")]
        public QueryParameter queryParameter { get; set; }

        [DataMember(Name = "FASO")]
        public List<FASO> faso { get; set; }

        [DataMember(Name = "NoOfTT")]
        public string noOfTT { get; set; }

        [DataMember(Name = "analegTTcount")]
        public string analegTTcount { get; set; }

        public enum ReplyType
        {
            NO_ERROR,
            FASO_FOUND,
            LINE_ERROR,
            CABLE_ERROR
        };
        [DataMember(Name = "ReplyType")]
        string replyTypeString
        {
            get { return replyType.ToString(); }
            set { replyType = (ReplyType)Enum.Parse(typeof(ReplyType), value); }
        }
        public ReplyType replyType { get; set; }
    }
}
