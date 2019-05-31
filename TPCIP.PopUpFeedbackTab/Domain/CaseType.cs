using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPCIP.PopUpFeedbackTab.Domain
{
    public enum CaseType
    {
        Columbus,
        MsgNoColumbus,
        MsgColumbusError,

        Etray,
        MsgNoEtray,
        MsgEtrayError,

        Faso,
        MsgNoFaso,
        MsgFasoError,
        MsgFasoNotReady,

        Bier,
        MsgNoBier,
        MsgBierError,

        CU,
        MsgNoCU,
        MsgCUError
    }
}
