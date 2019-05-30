using System.Runtime.Serialization;

namespace TPCIP.ToolPingWebPart.DataModel
{
    [DataContract]
    public class LineDiagnoseResult
    {
        [DataMember]
        public LineStateDiagnose linestateDiagnose { get; set; }
        [DataMember]
        public SeltDiagnose[] seltDiagnose { get; set; }
        [DataMember]
        public LineQualityDiagnose lineQualityDiagnose { get; set; }
        [DataMember]
        public LineInspectionDiagnose[] lineInspectionDiagnose { get; set; }
       
        [DataMember]
        public ShortLineQualityResult shortLineQualityResult { get; set; }
    }

    [DataContract]
    public class LineStateDiagnose
    {
        [DataMember]
        public LineConfigurationInfo lineConfigurationInfo { get; set; }
        
        [DataMember]
        public string[] classificationHistoryImageUrls { get; set; }
        
        [DataMember]
        public Problem[] diagnosisResult { get; set; }      
  
        [DataMember]
        public Distance[] shdslLineEstimatedDistance { get; set; }

        [DataMember]
        public LineOperationalInfo lineOperationalInfo { get; set; }
        
        [DataMember]
        public LineParameter[] lineParameters { get; set; }
    }

    [DataContract]
    public class LineConfigurationInfo
    {
        [DataMember]
        public string cpeType { get; set; }

        [DataMember]
        public string dslType { get; set; }
    }
    
    [DataContract]
    public class LineOperationalInfo
    {
        [DataMember]
        public bool lineUp { get; set; }

        [DataMember]
        public GroupLinks[] bondingGroupLinkStatus { get; set; }

        [DataMember]
        public string estimatedDELTDistance { get; set; }
    }

    [DataContract]
    public class GroupLinks
    {
        [DataMember]
        public string address { get; set; }
    }

    [DataContract]
    public class Problem
    {
        [DataMember]
        public string location { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public long confidence { get; set; }

        //[DataMember]
        //public string[] impact { get; set; }
    }

    [DataContract]
    public class Distance
    {
        [DataMember]
        public int distance { get; set; }
        [DataMember]
        public int wirePair { get; set; }
    }

    [DataContract]
    public class LineParameter
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string unit { get; set; }
        [DataMember]
        public string downstreamValue { get; set; }
        [DataMember]
        public string upstreamValue { get; set; }
    }

    [DataContract]
    public class SeltDiagnose 
    {
        //[DataMember]
        //public string quietLineNoiseGraphUrl { get; set; }
        [DataMember]
        public long inspectionId { get; set; }
        [DataMember]
        public LoopEstimate[] loopEstimate { get; set; }

        //[DataMember]
        //public long confidence { get; set; }

        //[DataMember]
        //public Problem[] diagnosis { get; set; }
    }

    [DataContract]
    public class LoopEstimate
    {
        //[DataMember]
        //public double attenuation { get; set; }

        [DataMember]
        public int lineLength { get; set; }

        [DataMember]
        public long timeStamp { get; set; }

        //[DataMember]
        //public int reliability { get; set; }

        //[DataMember]
        //public int lineLengthAccuracy { get; set; }

        //[DataMember]
        //public int loopTermination { get; set; }

        //[DataMember]
        //public SeltCapacityEstimate[] capacityEstimate { get; set; }

        //[DataMember]
        //public SeltBridgeTap[] bridgeTap { get; set; }

        //[DataMember]
        //public double electricalLength { get; set; }
             
        //[DataMember]
        //public double estimatedAttenuationAt1MhzSELT { get; set; }

        //[DataMember]
        //public double estimatedAttenuationDELT { get; set; }

        //[DataMember]
        //public double estimatedAttenuationSELT { get; set; }

        //[DataMember]
        //public string dataInterpretation { get; set; }
    }

    //[DataContract]
    //public class SeltCapacityEstimate 
    //{
    //    [DataMember]
    //    public string profileName { get; set; }
    //    [DataMember]
    //    public string dslType { get; set; }
    //    [DataMember]
    //    public long capacityDown { get; set; }
    //    [DataMember]
    //    public int capacityDownAccuracy { get; set; }
    //    [DataMember]
    //    public long capacityUp { get; set; }
    //    [DataMember]
    //    public int capacityUpAccuracy { get; set; }
    //    [DataMember]
    //    public string bitloadingGraphUrl { get; set; }
    //    [DataMember]
    //    public string signalsGraphUrl { get; set; }
    //    [DataMember]
    //    public string dataInterpretation { get; set; }
    //}

    //[DataContract]
    //public class SeltBridgeTap 
    //{
    //    [DataMember]
    //    public int distanceFromCO { get; set; }
    //    [DataMember]
    //    public int length { get; set; }
    //    [DataMember]
    //    public string dataInterpretation { get; set; }
    //}

    [DataContract]
    public class LineQualityDiagnose 
    {
        //[DataMember]
        //public BitLoadingDip[] bitloadingDips { get; set; }
        [DataMember]
        public string[] imageUrl { get; set; }
        [DataMember]
        public Problem[] problemList { get; set; }
        [DataMember]
        public long startTime { get; set; }
    }

    //[DataContract]
    //public class BitLoadingDip
    //{
    //    [DataMember]
    //    public int startTone { get; set; }
    //    [DataMember]
    //    public int stopTone { get; set; }
    //    [DataMember]
    //    public double depth { get; set; }
    //}

    [DataContract]
    public class LineInspectionDiagnose 
    {
        [DataMember]
        public long inspectionId { get; set; }
        [DataMember]
        public string type { get; set; }

        //[DataMember]
        //public string resource { get; set; }

        //[DataMember]
        //public Problem[] type { get; set; }

        //[DataMember]
        //public string description { get; set; }

        //[DataMember]
        //public string userName { get; set; }

        //[DataMember]
        //public string serviceStabilityStatus { get; set; }

        [DataMember]
        public string state { get; set; }

        [DataMember]
        public long startTime { get; set; }

        [DataMember]
        public long stopTime { get; set; }
    }

    [DataContract]
    public class ShortLineQualityResult
    {
        //[DataMember]
        //public BitLoadingDip[] bitloadingDips { get; set; }
        [DataMember]
        public long inspectionId { get; set; }
        [DataMember]
        public long startTime { get; set; }
        [DataMember]
        public string[] imageUrl { get; set; }
        [DataMember]
        public Problem[] problemList { get; set; }
 
    }
}
