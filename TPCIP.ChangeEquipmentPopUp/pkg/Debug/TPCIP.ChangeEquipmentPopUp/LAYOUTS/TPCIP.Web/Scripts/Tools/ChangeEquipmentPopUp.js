/// <reference path="../_references.js" />

if (TdcChangeEquipment == null || typeof (TdcChangeEquipment) != "object") {
    var TdcChangeEquipment = new Object();
}

(function ($) {
    (function (context) {
        var toolName = 'ChangeEquipmentPopUp';
        context.ChangeEquipmentPopUp = "";
        context.LoadOldData = "";
        context.ToolInitiatorSender = "";

        context.init = function (sender) {
            var $webpart = $(sender);



            $(".chkRemoveEquipment").on("change", function () {
                if ($(".chkRemoveEquipment:checked").length > 0) {
                    $webpart.find('#UpdateBtn').prop('disabled', true);
                    $('#RemoveBtn').prop('disabled', false);
                }
                else {
                    $('#RemoveBtn').prop('disabled', true);
                    if (IsValidValue()) {
                        $webpart.find("#UpdateBtn").prop("disabled", false);
                    }
                }
            });

            $(".DdlCpeMac").on("change", function () {
                if ($("#CpeMac option:selected").text() == "Vælg" && $("#ExtraCpeMac option:selected").text() == "Vælg") {
                    $webpart.find("#UpdateBtn").prop("disabled", true);
                }
                else {
                    if ($(".chkRemoveEquipment:checked").length <= 0) {
                        $webpart.find("#UpdateBtn").prop("disabled", false);
                    }
                }
            });

        }

        function IsValidValue() {
            if (($("#CpeMac option:selected").text() != "Vælg" && $("#CpeMac option:selected").text() != "") || ($("#ExtraCpeMac option:selected").text() != "Vælg" && $("#ExtraCpeMac option:selected").text() != "")) {
                return true;
            }
            else { return false; }
        }

        // To Refresh ToolYouseeBroadband using NoteId
        context.RefreshWPYouSee = function (sender) {
            if (context.ToolInitiatorSender !== "" && context.ToolInitiatorSender !== undefined) {
                var $webpart = TdcAsyncWebPartLoader.getTool(context.ToolInitiatorSender);
                TdcAsyncWebPartLoader.RefreshWP($webpart);
            }
        }

        context.showChangeEquipmentPopUp = function (sender) {
            var $sender = $(sender);
            ///The tool that initiate the ChangeEquipment store the sender html to derive the toolname on the refresh of the tool
            context.ToolInitiatorSender = $sender;

            var $webpart = $('#ChangeEquipmentWebpart');
            context.ChangeEquipmentPopUp = $webpart;

            //hotjartracking and recording
            var eventName = $(sender).attr('data-eventname');
            TdcMain.CallHotJarVPVForEvents(eventName);
            TdcMain.CallHotJarTriggersForEvents(eventName);
            //hotjar ends

            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var customerId = requestData.context.customerId;
            $webpart.find("#AvailableMacAlert").hide();
            var noteIdValue = requestData.context.noteId;
            $("#ChangeEquipmentPopUp").data("noteid", noteIdValue);
            $("#ChangeEquipmentPopUp").modal('show');

            TdcAsyncWebPartLoader.ShowTool({
                toolname: toolName,
                action: 'Index',
                context: {
                    customerId: customerId,
                    //noteId: requestData.context.noteId,
                    //placeholder: $("#ChangeEquipmentPopUp").find("#ChangeEquipmentPopUp_placeholder"),
                },
                isloadingIcon: true,
                callback: function (html) {
                    $webpart = $(html);
                    TdcChangeEquipment.init($webpart);
                    var data = $(html).find(".dataLoadEquipment").data("loadequipment");
                    var success = data.IsSuccess.toString();
                    if (success.toLowerCase() == "false") {
                        $webpart.find("#RegesteredMac").html(data.ModemMac);
                        $webpart.find("#MtaMac").html(data.MtaMac);

                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".error").text("Vi kan ikke indhente data.");
                        var listAddCpe = data.AddCpeList;
                        if (listAddCpe == undefined) {
                            $("#ExtraCpeVisible").css('display', 'none');
                        }

                    }
                    else {
                        context.LoadOldData = data;

                        var flag = $(sender).data("flag");

                        if (flag == "OpdaterBtn") {
                            $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                            $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("Data er opdateret");

                        }
                        var listStdCpe = data.StdCpeList;
                        var listAddCpe = data.AddCpeList;
                        var CpeMac = $webpart.find("#CpeMac");
                        var ExtraCpeMac = $webpart.find("#ExtraCpeMac");
                        var cmMac = data.ModemMac;
                        $webpart.find("#RegesteredMac").html(" " + cmMac);
                        var mtaMac = data.MtaMac;
                        $webpart.find("#MtaMac").html(mtaMac);
                        $webpart.find("#newMac").val("");
                        if (CpeMac) {
                            CpeMac.html("");
                            if (listStdCpe != undefined) {
                                $.each(listStdCpe,
                                    function (index, value) {
                                        CpeMac.append($("<option></option>").val(value.Value).html(value.Value));
                                    });
                            }
                        }
                        var addCpeFlag = data.IsAddCpeExist.toString();
                        if (addCpeFlag.toLowerCase() == "true") {
                            if (ExtraCpeMac) {
                                ExtraCpeMac.html("");
                                if (listAddCpe != undefined) {
                                    $.each(listAddCpe,
                                        function (index, value) {
                                            ExtraCpeMac.append(
                                                $("<option></option>").val(value.Value).html(value.Value));
                                        });
                                }
                            }
                        } else {
                            $("#ExtraCpeVisible").css('display', 'none');
                        }

                        context.DropDownChangeEvent($('.DdlCpeMac'));

                    }
                    $webpart.find("#InvalidMac").css("display", "none");
                    $webpart.find("#UpdateBtn").prop("disabled", true);
                    $("#ChangeEquipmentPopUp").show();
                    $("#ChangeEquipmentPopUp").modal();
                },
                errorcallback: function () {

                },
                messageSuccess: "",
                messageError: "",
                messageProcess: "Henter oplysninger"
            }, sender);


        };

        context.DropDownChangeEvent = function (sender) {
            var selected = $("option:selected", $(sender)).val();
            //hotjar tracking and recording
            var eventName = $(sender).attr('data-eventname');
            TdcMain.CallHotJarVPVForEvents(eventName);
            TdcMain.CallHotJarTriggersForEvents(eventName);
            //hotjar end
            $(".DdlCpeMac option").each(function () {
                $(this).prop("disabled", false);
            });
            if (selected != "Vælg") {
                var thisID = $(sender).prop("id");

                $(".DdlCpeMac").each(function () {
                    if ($(this).prop("id") != thisID) {
                        $("option[value='" + selected + "']", $(this)).prop("disabled", true);
                    }
                });
            }
        };

        context.ValidationNewMac = function (sender) {

            var $sender = $(sender);
            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var noteId = requestData.context.noteId;
            var customerId = requestData.context.customerId;
            var $webpart = TdcAsyncWebPartLoader.getTool(sender);
            //var regex = /^\b[a-f0-9]{12}\b$/i;
            var newMac = TdcValidation.validateMACAddress(sender);

            if (newMac) {

                var CpeMac = $webpart.find("#CpeMac");
                var ExtraCpeMac = $webpart.find("#ExtraCpeMac");
                $webpart.find("#InvalidMac").css("display", "none");
                $webpart.find("#newMac").prop("disabled", true);

                //hotjartracking and recording
                var eventName = $(sender).attr('data-eventname');
                TdcMain.CallHotJarVPVForEvents(eventName);
                TdcMain.CallHotJarTriggersForEvents(eventName);
                //hotjar end

                setTimeout(function () {
                    TdcAsyncWebPartLoader.DoAction({
                        toolname: toolName,
                        action: 'LoadAvailableAndRegisteredChangeEquipPopUpData',
                        context: {
                            customerId: customerId,
                            newMac: newMac,
                        },
                        isloadingIcon: true,
                        callback: function (data) {
                            var user = data.OtherUser.toString();
                            var Err = data.IsError.toString();
                            if (user.toLowerCase() == "true") {
                                if (data.Lid != undefined && data.Lid != "" && data.Lid != customerId) {
                                    var msgErrLid = "Den indtastede MAC er allerede registreret på {0}. Tjek at du har tastet MAC korrekt. Hvis ja, skal du afprovisionere fra nuværende LID, før du kan registrere udstyret igen.".formatfunction(data.Lid);
                                    context.DisplayErrorMessageAndClearField(msgErrLid, $webpart);
                                }
                                else {
                                    context.DisplayErrorMessageAndClearField("Den indtastede MAC er allerede registreret på et LID/abonnement. Tjek at du har tastet MAC korrekt. Hvis ja, skal du afprovisionere fra nuværende LID, før du kan registrere udstyret igen.", $webpart);
                                }
                            }
                            else if (Err.toLowerCase() == "false") {

                                var success = data.IsSuccess.toString();

                                if (success.toLowerCase() == "false") {
                                    context.DisplayErrorMessageAndClearField("Modem er offline på den indtastede MAC. Tjek at oplysninger er korrekte og modem er forbundet korrekt.", $webpart);
                                    $webpart.find("#UpdateBtn").prop("disabled", true);
                                }
                                else {

                                    $webpart.find("#UpdateBtn").prop("disabled", false);

                                    $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                                    $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("MAC adresser til den indtastede Modem MAC er hentet med success.");

                                    var listCpeMac = data.ListCpeMac;
                                    var mtaMac = data.MtaMac;
                                    $webpart.find("#MtaMac").html(mtaMac);
                                    $webpart.find("#AvailableMacAlert").show();

                                    if (CpeMac) {
                                        CpeMac.html("");
                                        if (listCpeMac != undefined) {
                                            $.each(listCpeMac,
                                                function (index, value) {
                                                    CpeMac.append($("<option></option>").val(value.Value).html(value.Value));
                                                });
                                        }
                                    }
                                    var addCpeFlag = data.IsAddCpeExist.toString();
                                    if (addCpeFlag.toLowerCase() == "true") {
                                        if (ExtraCpeMac) {
                                            ExtraCpeMac.html("");
                                            if (listCpeMac != undefined) {
                                                $.each(listCpeMac,
                                                    function (index, value) {
                                                        ExtraCpeMac.append(
                                                            $("<option></option>").val(value.Value).html(value.Value));
                                                    });
                                            }
                                        }
                                    } else {
                                        $("#ExtraCpeVisible").css('display', 'none');
                                    }

                                }

                            }
                            else if (Err.toString() == "true") {
                                context.DisplayErrorMessageAndClearField(data.Message, $webpart);
                            }

                            $webpart.find("#newMac").prop("disabled", false);
                            $webpart.find("#newMac").focus(); //Added for focus

                        },
                        errorcallback: function () {
                            $webpart.find("#newMac").focus(); //Added for focus
                        },
                        messageSuccess: "",
                        messageError: "",
                        messageProcess: "Henter MAC adresser"
                    }, sender);
                }, 500);

            } else {

                // $webpart.find("#InvalidMac").css("display", "block");
                $webpart.find("#UpdateBtn").prop("disabled", true);
                context.DisplayErrorMessageAndClearField("", $webpart);
            }

        };

        context.DisplayErrorMessageAndClearField = function (errorMessage, $webpart) {
            $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
            if (errorMessage !== "" && errorMessage !== undefined) {
                $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".error").text(errorMessage);
            }
            $webpart.find("#MtaMac").html("");
            $webpart.find("#CpeMac").html("");
            $webpart.find("#ExtraCpeMac").html("");
        };

        context.UpdateDataChangeEquipmentPopUp = function (sender) {
            var $sender = $(sender);
            var $webpart = $('#ChangeEquipmentWebpart');

            //hotjartracking and recording
            var eventName = $(sender).attr('data-eventname');
            TdcMain.CallHotJarVPVForEvents(eventName);
            TdcMain.CallHotJarTriggersForEvents(eventName);
            //hotjar end


            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var noteId = requestData.context.noteId;
            var customerId = requestData.context.customerId;
            var newCpe = $webpart.find("#CpeMac option:selected").text();
            var newExtCpe = $webpart.find("#ExtraCpeMac option:selected").text();
            var regMac = $webpart.find("#RegesteredMac").text();
            var newMac = $webpart.find("#newMac").val();
            var cpeMac = newCpe;
            var extCpeMac = newExtCpe;
            $webpart.find("#AvailableMacAlert").hide();

            var flyMsgSender = $("#DivOpdater");
            TdcAsyncWebPartLoader.DoAction({
                toolname: toolName,
                action: 'UpdateChangeEquipPopUpData',
                context: {
                    customerId: customerId,
                    regMac: regMac,
                    newMac: newMac,
                    cpeMac: cpeMac,
                    extCpeMac: extCpeMac
                },
                isloadingIcon: true,
                callback: function (data) {
                    if (data.toLowerCase() == "true") {
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("Udstyr er skiftet. Provisionering og genstart af modem tager nogle minutter. Følg status ved at opdatere linjestatus.");
                    }
                    else {
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".error").text("Skift af udstyr fejlede. Tjek at data er tastet korrekt.");
                    }
                },
                errorcallback: function () {
                },
                messageSuccess: "",
                messageError: "",
                messageProcess: "Registrering af udstyr er igang"
            }, sender);


        };

        context.showRemoveBtnPopUp = function (sender) {
            //hotjartracking and recording
            var eventName = $(sender).attr('data-eventname');
            TdcMain.CallHotJarVPVForEvents(eventName);
            TdcMain.CallHotJarTriggersForEvents(eventName);
            //hotjar end

            $("#RemoveBtnPopUp").modal('show');
        };



        context.onChkRegesteredMac = function (sender) {

            //hotjartracking and recording
            var eventName = $(sender).attr('data-eventname');
            TdcMain.CallHotJarVPVForEvents(eventName);
            TdcMain.CallHotJarTriggersForEvents(eventName);
            //hotjar end

            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var noteId = requestData.context.noteId;
            var customerId = requestData.context.customerId;
            var $webpart = $('#ChangeEquipmentWebpart');

            if ($webpart.find('#chkRegesteredMac').is(':checked')) {
                $webpart.find('#ChkCpeMac').prop('checked', true);
                $webpart.find('#ChkExtraMac').prop('checked', true);
            }
            else {
                $webpart.find('#ChkCpeMac').prop('checked', false);
                $webpart.find('#ChkExtraMac').prop('checked', false);
            }
        };

        context.OnClickAnnuller = function (sender) {
            $("#RemoveBtnPopUp").modal('hide');
        };

        context.OnClickCross = function (sender) {
            $("#RemoveBtnPopUp").modal('hide');
        };

        context.OkClicked = function (sender) {
            var $sender = $(sender);
            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var noteId = requestData.context.noteId;
            var customerId = requestData.context.customerId;
            var $webpart = $('#ChangeEquipmentWebpart');
            var removeRegMac = $webpart.find('#chkRegesteredMac').is(':checked');
            var removeCpeMac = $webpart.find('#ChkCpeMac').is(':checked');
            var removeExtCpeMac = $webpart.find('#ChkExtraMac').is(':checked');
            var flyMsgSender = $("#DivOpdater");
            $webpart.find("#AvailableMacAlert").hide();

            TdcAsyncWebPartLoader.DoAction({
                toolname: toolName,
                action: 'RemoveChangeEquipPopUpData',
                context: {
                    customerId: customerId,
                    removeRegMac: removeRegMac,
                    removeCpeMac: removeCpeMac,
                    removeExtCpeMac: removeExtCpeMac
                },
                isloadingIcon: true,
                callback: function (data) {
                    if (data.toLowerCase() == "true") {
                        $webpart.find('#chkRegesteredMac').prop('checked', false);
                        $webpart.find('#ChkCpeMac').prop('checked', false);
                        $webpart.find('#ChkExtraMac').prop('checked', false);
                        $('#RemoveBtn').prop('disabled', true);
                        $('#UpdateBtn').prop('disabled', false);
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("Kundens udstyr er afprovisioneret. Følg status ved at opdatere linjestatus.");
                    }
                    else {
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".error").text("Vi kan ikke fjerne Mac adresse fra kundens udstyr");
                    }

                    $("#RemoveBtnPopUp").modal('hide');
                },
                errorcallback: function () {
                },
                messageSuccess: "",
                messageError: "",
                messageProcess: "Afprovisionering er igang"
            }, sender);
        };


    })(TdcChangeEquipment);

    $(document).ready(function () {
        TdcAsyncWebPartLoader.$rootElement.on(TdcAsyncWebPartLoader.toolLoadedEventName, function (event, html) {
            if ($(html).hasClass("TdcChangeEquipment")) {
                //TdcChangeEquipment.showChangeEquipmentPopUp(html);
            }
        });
    });

})(jQuery);