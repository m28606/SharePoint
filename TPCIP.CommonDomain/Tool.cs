using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace TPCIP.CommonDomain
{
    public class Tool
    {
        public const string ParamRequiredAction = "RequiredAction";
        public const string ParamIsLoadRequired = "IsLoadRequired";
        public const string ParamFasoShowCommentOnly = "ShowCommentOnly";
        public const string ParamProfileName = "profileName";
        public const string ParamMessageId = "messageID";
        public const string ParamDriks = "ShowDriks";
        public const string ParamANG1 = "ShowANG1";
        public const string ParamANG2 = "ShowANG2";
        public const string ParamMDB = "ShowMDB";
        public const string ParamCSAM = "ShowCSAM";
        public const string ParamOASIS = "ShowOASIS";
        public const string ParamSPOC = "ShowSPOC";
        public const string ParamMU = "ShowMU";
        public const string ParamEtrayCat = "cat";
        public const string ParamEtrayArea = "area";
        public const string ParamEtrayCIP_TAG1 = "CIP_TAG1";
        public const string ParamCitrixCoax = "ShowCitrixCoax";
        public const string ParamFasoCoax = "ShowFasoCoax";
        public const string ParamMersalgCoax = "ShowMersalgCoax";
        public const string ParamAftaleseddelCoax = "ShowAftaleseddelCoax";
        public const string ParamAftaleseddelRetabCoax = "ShowAftaleseddelRetabCoax";
        public const string ParamAndCoax = "ShowAndCoax";

        public const string ParamWebmanKob = "ShowWebmanKob";
        public const string ParamPsKob = "ShowPsKob";
        public const string ParamHbsKob = "ShowHbsKob";
        public const string ParamIbKob = "ShowIbKob";
        public const string ParamMawisKob = "ShowMawisKob";
        public const string ParamGshdslmonKob = "ShowGshdslmonKob";
        public const string ParamOmKob = "ShowOmKob";
        public const string ParamCitrixKob = "ShowCitrixKob";
        public const string ParamFasoKob = "ShowFasoKob";
        public const string ParamMersalgKob = "ShowMersalgKob";
        public const string ParamAftaleseddelKob = "ShowAftaleseddelKob";
        public const string ParamAftaleseddelRetabKob = "ShowAftaleseddelRetabKob";

        public const string ParamWebmanFib = "ShowWebmanFib";
        public const string ParamPsFib = "ShowPsFib";
        public const string ParamHbsFib = "ShowHbsFib";
        public const string ParamIbFib = "ShowIbFib";
        public const string ParamCitrixFib = "ShowCitrixFib";
        public const string ParamFasoFib = "ShowFasoFib";
        public const string ParamMersalgFib = "ShowMersalgFib";
        public const string ParamAftaleseddelFib = "ShowAftaleseddelFib";
        public const string ParamAftaleseddelRetabFib = "ShowAftaleseddelRetabFib";

        public const string ParamAttachToOv = "AttachToOv";

        //public const string ParamEtrayCat = "cat";
        //public const string ParamEtrayArea = "area";
        //public const string ParamEtrayCIP_TAG1 = "CIP_TAG1";

        public Tool()
        {

        }

        public string Name { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
