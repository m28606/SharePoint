/// <reference path="../asyncwebpartloader.js" />
/// <reference path="../tools/customerinformation.js" />


if (TdcToolPingWebPart == null || typeof (TdcToolPingWebPart) != "object") {
    var TdcToolPingWebPart = new Object();
}


(function ($) {
    (function (context) {

        var toolName = "ToolPingWebPart";
        context.loadTool = function (webpart) {
            var sikDropDown = $(webpart).find("#sikLst");
            var sikDS = sikDropDown.data("ds");
            sikDropDown.html("");
            $.each(sikDS, function (index, value) {
                var splitVlaues = value.split("-");
                var SikTypedata = splitVlaues[0].trim();
                var SikDisplaydata = null;
                if (splitVlaues[1])
                    SikDisplaydata = splitVlaues[1].trim();
                if (SikDisplaydata)
                    $(webpart).find("#sikLst").append('<li style="color:grey" data-sikdata= ' + SikTypedata + ' onclick="TdcToolPingWebPart.loadSikData(this,2)"><a style="color:inherit" data-toggle="tab" >' + SikDisplaydata + " - [" + SikTypedata + "]" + '</a></li>');
                else
                    $(webpart).find("#sikLst").append('<li style="color:grey" data-sikdata= ' + SikTypedata + ' onclick="TdcToolPingWebPart.loadSikData(this,2)"><a style="color:inherit" data-toggle="tab" >' + SikTypedata + '</a></li>');
            });



            var cursik = sikDropDown.data("selsik");
            if (cursik != undefined) {

                $(webpart).find('#sikLst > li[data-sikdata=' + cursik + ']').addClass('activePingTab').css({ "color": "#2e6296" });

            }
            else {
                $(webpart).find('#sikLst > li:first').addClass('activePingTab').css({ "color": "#2e6296" });
            }

            var sikResult = $(webpart).find("#sikResult").data("sikresult");

            var hybridResult = null;
            var dslResult = null;
            if (sikResult.SikID == "LTE") {
                hybridResult = $(webpart).find("#hybridResult").data("hybridresult");
                $(webpart).find("#SikID").text(sikResult.SikID);
                $(webpart).find("#hybridPingResultInfo").css("display", "block");
                if (sikResult.CepType != null) {
                    $(webpart).find("#CpeType").html(sikResult.CepType);
                    $(webpart).find("#hybridResult").html($(webpart).find("#pingResultHybrid").render(hybridResult));
                }
              
            }
            else {
                dslResult = $(webpart).find("#dslResult").data("dslresult");
                $(webpart).find("#sikType").text(sikResult.Type);
                $(webpart).find("#SikID").text(sikResult.SikID);
                $(webpart).find("#wanResult").html(sikResult.WanIp);
                var CepText = sikResult.Cep != null ? sikResult.Cep.split('\\n')[0] : "";
                $(webpart).find("#cepResult").html(CepText);
                if (sikResult.CepType != null) {
                    $(webpart).find("#CpeType").html(sikResult.CepType);
                    $(webpart).find("#pingSingleResult").html($(webpart).find("#pingResultsimage").render(sikResult));
                }
                $(webpart).find("#hybridPingResultInfo").css("display", "none");
            }

            Number.prototype.padLeft = function (base, chr) {
                var len = (String(base || 10).length - String(this).length) + 1;
                return len > 0 ? new Array(len).join(chr || '0') + this : this;
            }

            var d = new Date,
               dformat = [d.getDate().padLeft(),
                       (d.getMonth() + 1).padLeft(),
                       d.getFullYear()].join('-') +
                       ' ' +
                     [d.getHours().padLeft(),
                       d.getMinutes().padLeft(),
                       d.getSeconds().padLeft()].join(':');
            $(webpart).find("#pingTime").html(dformat);

            if (dslResult && dslResult.Ipv4PingResult.IpResultList) {
                $(webpart).find("#pingResultContainer").html($(webpart).find("#pingResults").render(dslResult));
                $(webpart).find(".Lossperc").each(function (index) {
                    if ($(webpart).find("#pingStatus" + index).text() == "0.0 (OK)" || $(webpart).find("#pingStatus" + index).text().toLowerCase() == 'ok') {
                        $(webpart).find(".pingLossStatus" + index).attr("class", "glyphicon glyphicon-one-fine-green-dot");
                    }
                    else if ($(webpart).find("#pingStatus" + index).text().includes('100') || $(webpart).find("#pingStatus" + index).text().includes('100.0')) {
                        $(webpart).find(".pingLossStatus" + index).attr("class", "glyphicon glyphicon-one-fine-red-dot");
                    }
                    else {
                        $(webpart).find(".pingLossStatus" + index).attr("class", "glyphicon glyphicon-one-fine-Amber-dot");
                    }
                });
            }
            if (dslResult && dslResult.Ipv6PingResult.IpResultList) {
                $(webpart).find("#pingIPv6Container").html($(webpart).find("#pingIpv6").render(dslResult));
                $(webpart).find(".Lossperc6").each(function (index) {
                    if ($(webpart).find("#pingIpv6Status" + index).text() == "0.0 (OK)") {
                        $(webpart).find(".pingLossStatus6" + index).attr("class", "glyphicon glyphicon-one-fine-green-dot");
                    }
                    else if ($(webpart).find("#pingIpv6Status" + index).text().includes('100') || $(webpart).find("#pingIpv6Status" + index).text().includes('100.0')) {
                        $(webpart).find(".pingLossStatus6" + index).attr("class", "glyphicon glyphicon-one-fine-red-dot");
                    }
                    else {
                        $(webpart).find(".pingLossStatus6" + index).attr("class", "glyphicon glyphicon-one-fine-Amber-dot");
                    }
                });
            }
            //$(webpart).find("#pingLossStatus")
            if (sikResult.SikID == "LTE") {
                $(webpart).find("#btnPingWebpart").css("display", "none");
            } else {
                $(webpart).find("#btnPingWebpart").css("display", "block");
                if (sikResult.tpnumber == "8" || sikResult.tpnumber == "15") {
                    $(webpart).find("#wandetails").css('display', 'none');
                }
                if (sikResult.tpnumber == "17" || sikResult.tpnumber == "7") {
                    $(webpart).find("#btnRelease").attr('disabled', 'disabled');
                }
                else {
                    $(webpart).find("#btnRelease").removeAttr('disabled', 'disabled');
                }
            }

            $(webpart).find('#pingResult').hover(
            function () { $(this).addClass('maximizePingResult') },
            function () { $(this).removeClass('maximizePingResult') }
           );
        }

        context.loadSikData = function (sender, From) {
            var $sender = $(sender);
            var $webpart = TdcAsyncWebPartLoader.getTool($sender);
            var requestData = TdcAsyncWebPartLoader.getLastRequestData($sender);
            var noteId = requestData.context.noteId;
            var curSik = "";
            var curSikOriginal1;
            var sikDS = "";

            if (From == 2) {
                var sikDropDown = $webpart.find("#sikLst");
                sikDS = sikDropDown.data("ds");
                var curSikOriginal = $sender.text();
                 curSikOriginal1 = $sender.data("sikdata");
                var res = curSikOriginal1.split("-");
                 curSik = res[0].trim();
            }
            else {
                var sikDropDown = $webpart.find("#sikLst");
                 sikDS = sikDropDown.data("ds");
                var curSikOriginal = sikDropDown.find(".activePingTab").text();
                 curSikOriginal1 = sikDropDown.find(".activePingTab").data("sikdata");
                var res = curSikOriginal1.split("-");
                 curSik = res[0].trim();
            }

            TdcAsyncWebPartLoader.ShowTool({
                toolname: toolName,
                action: 'Index',
                context: {
                    noteId: noteId,
                    customerId: requestData.context.customerId,
                    sikId: curSik,
                },
                callback: function (webpart) {

                    $(webpart).find("#sikLst").data("ds", sikDS).data("selsik", curSikOriginal1);
                    return false;
                }
            });
            return false;
        }

        context.showDetailedPingResult = function (sender) {
            $(sender).next("#detailedPingResult").toggle();
        }

        context.startCountDownToPing = function (noteId) {
            setTimeout('TdcToolPingWebPart.ping(' + noteId + ')', 5000);
        };


        context.ping = function (noteId) {
            var $webpart = TdcAsyncWebPartLoader.getToolByName(toolName, noteId);
            if ($webpart == null || $webpart.length == 0) return;
            var requestData = TdcAsyncWebPartLoader.getFirstRequestData($webpart);

            var $tab = TdcTabManager.$findTab({ noteId: noteId });


            if (!TdcTabManager.isTabActive($tab)) {
                TdcToolPingWebPart.startCountDownToPing(noteId);
                return;
            }

            var sik = $webpart.attr('data-sik') || '';
            var dslam = $webpart.attr('data-dslam') || '';
            var portid = $webpart.attr('data-portid') || '';

            TdcAsyncWebPartLoader.DoAction({
                toolname: toolName,
                action: 'TimerRequest',
                context: {
                    noteId: noteId,
                    customerId: requestData.context.customerId,
                    sik: sik,
                    dslam: dslam,
                    portid: portid
                },
                callback: function (data) {
                    $webpart = TdcAsyncWebPartLoader.getToolByName(toolName, noteId); //webpart could be posibbly removed
                    if ($webpart == null || $webpart.length == 0) return;

                    TdcToolPingWebPart.startCountDownToPing(noteId);
                    var pingStatus = $webpart.find('.pingStatus');

                    pingStatus.removeAttr("class");
                    if (data.toLowerCase() == "true") {
                        pingStatus.attr("class", "pingStatus glyphicon glyphicon-ok-circle");
                    } else {
                        pingStatus.attr("class", "pingStatus glyphicon glyphicon-remove-circle");
                    }
                }
            });
        };

        context.release = function (sender) {
            var $sender = $(sender);
            var $webpart = TdcAsyncWebPartLoader.getTool($sender);
            var requestData = TdcAsyncWebPartLoader.getLastRequestData($sender);
            var noteId = requestData.context.noteId;

            var curSikOriginal = $webpart.find("#sikLst li.activePingTab").data("sikdata");
            var res = curSikOriginal.split("-");
            var curSik = res[0];
            TdcAsyncWebPartLoader.DoAction({
                toolname: toolName,
                action: 'ReleaseIP',
                context: {
                    noteId: noteId,
                    customerId: requestData.context.customerId,
                    sikId: curSik,
                },
                callback: function () {
                    var TextMessage = "Frigivelse IP succes for " + curSik;
                    TdcUI.createMessage(TextMessage, TdcUI.messageOptions.success, null, false, sender);
                    return false;

                },
                errorcallback: function (response) {
                    var errorTitle = response.getResponseHeader('ErrorTitle');
                    TdcUI.createMessage(errorTitle, TdcUI.messageOptions.error, null, false, sender);
                    return false;
                },

            }, sender);
        }

    })(TdcToolPingWebPart);

    $(document).ready(function () {
        TdcAsyncWebPartLoader.$rootElement.on(TdcAsyncWebPartLoader.toolLoadedEventName, function (event, html) {
            if ($(html).hasClass("TdcPingWebPart")) {
                TdcToolPingWebPart.loadTool(html);
            }
        });
    });

})(jQuery);