/// <reference path="../_references.js" />

if (TdcChangeEquipment == null || typeof (TdcChangeEquipment) != "object") {
    var TdcChangeEquipment = new Object();
}

(function ($) {
    (function (context) {
        var toolName = 'ChangeEquipmentPopUp';
        context.RemoveSender = "";
        context.ChangeEquipmentPopUp = "";
        context.LoadOldData = "";

        context.init = function (sender) {
            var $webpart = $(sender);
            $(".chkRemoveEquipment").on("change", function () {
                if ($(".chkRemoveEquipment:checked").length > 0) {
                    $('#RemoveBtn').prop('disabled', false);
                    $('#UpdateBtn').prop('disabled', true);
                }
                else {
                    $('#RemoveBtn').prop('disabled', true);
                    $('#UpdateBtn').prop('disabled', false);
                }
            });            
           
        }
        
        // To Refresh ToolYouseeBroadband using NoteId
        context.RefreshWPYouSee = function (sender) {
            var toolName = 'ToolYouseeBroadband';
            var noteId = $("#ChangeEquipmentWebpart").attr("data-noteid");
            var $webpart = TdcAsyncWebPartLoader.getToolByName(toolName, noteId);
            TdcAsyncWebPartLoader.RefreshWP($webpart);
        }

        context.showChangeEquipmentPopUp = function (sender) {
            var $webpart = $('#ChangeEquipmentWebpart');
            context.ChangeEquipmentPopUp = $webpart;
            var $sender = $(sender);
            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var customerId = requestData.context.customerId;
            $webpart.find("#AvailableMacAlert").hide();
            var noteIdValue = requestData.context.noteId;
            $("#ChangeEquipmentWebpart").data("noteid", noteIdValue);
            $("#ChangeEquipmentPopUp").modal('show');

            TdcAsyncWebPartLoader.ShowTool({
                toolname: toolName,
                action: 'Index',
                context: {
                    customerId: customerId,
                    noteId:requestData.context.noteId
                },
                isloadingIcon: true,
                callback: function (html) {
                    $webpart = $(html);
                    TdcChangeEquipment.init($webpart);
                    var data = $(html).find(".dataLoadEquipment").data("loadequipment");
                    var success = data.IsSuccess.toString();
                    if (success.toLowerCase() == "false") {

                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".status").text("Vi kan ikke indhente data.");

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
                        $webpart.find("#RegesteredMac").html(cmMac);
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
                    $webpart.find("#UpdateBtn").prop("disabled", false);
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
            var $webpart = $('#ChangeEquipmentWebpart');
            var requestData = TdcAsyncWebPartLoader.getFirstRequestData(sender);
            var noteId = requestData.context.noteId;
            var customerId = requestData.context.customerId;
            var regex = /^\b[a-f0-9]{12}\b$/i;
            var newMac = $webpart.find("#newMac").val().trim().toLowerCase().replace(/\:/g, "");
            if (regex.test(newMac)) {
                $webpart.find("#InvalidMac").css("display", "none");
                $webpart.find("#UpdateBtn").prop("disabled", false);
                $webpart.find("#newMac").prop("disabled", true);
                setTimeout(function () {
                    TdcAsyncWebPartLoader.DoAction({
                        toolname: toolName,
                        action: 'LoadAvailableChangeEquipPopUpData',
                        context: {
                            newMac: newMac,
                        },
                        isloadingIcon: true,
                        callback: function (data) {
                            var success = data.IsSuccess.toString();
                            if (success.toLowerCase() == "false") {
                                $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                                $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".status").text("Modem er offline på den indtastede MAC. Tjek at oplysninger er korrekte og modem er forbundet korrekt.");
                                $webpart.find("#UpdateBtn").prop("disabled", true);


                                var listStdCpe = context.LoadOldData.StdCpeList;
                                var listAddCpe = context.LoadOldData.AddCpeList;
                                var CpeMac = $webpart.find("#CpeMac");
                                var ExtraCpeMac = $webpart.find("#ExtraCpeMac");
                                var cmMac = context.LoadOldData.ModemMac;
                                var mtaMac = context.LoadOldData.MtaMac;
                                $webpart.find("#MtaMac").html("");

                                CpeMac.html("");

                                ExtraCpeMac.html("");

                            }
                            else {

                                $webpart.find("#UpdateBtn").prop("disabled", false);

                                $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                                $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("MAC adresser til den indtastede Modem MAC er hentet med sucess.");

                                var listCpeMac = data.ListCpeMac;
                                var CpeMac = $webpart.find("#CpeMac");
                                var ExtraCpeMac = $webpart.find("#ExtraCpeMac");
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
                            $webpart.find("#newMac").prop("disabled", false);
                        },
                        errorcallback: function () {
                        },
                        messageSuccess: "",
                        messageError: "",
                        messageProcess: "Henter MAC adresser"
                    }, sender);
                }, 500);

            } else {

                $webpart.find("#InvalidMac").css("display", "block");
                $webpart.find("#UpdateBtn").prop("disabled", true);

                var listStdCpe = context.LoadOldData.StdCpeList;
                var listAddCpe = context.LoadOldData.AddCpeList;
                var CpeMac = $webpart.find("#CpeMac");
                var ExtraCpeMac = $webpart.find("#ExtraCpeMac");
                var cmMac = context.LoadOldData.ModemMac;

                var mtaMac = context.LoadOldData.MtaMac;
                $webpart.find("#MtaMac").html("");
                CpeMac.html("");
                ExtraCpeMac.html("");
            }

        };


        context.UpdateDataChangeEquipmentPopUp = function (sender) {
            var $sender = $(sender);
            var $webpart = $('#ChangeEquipmentWebpart');
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
                        $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("Udstyr er skiftet. Provisionering og genstart af modem tager nogle minutter. Følg status i linjestatusværktøj.");
                    }
                    else {
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".status").text("Skift af udstyr fejlede. Tjek at data er tastet korrekt.");
                    }
                },
                errorcallback: function () {
                },
                messageSuccess: "",
                messageError: "",
                messageProcess: "Registrering af udstyr igang"
            }, sender);


        };

        context.showRemoveBtnPopUp = function (sender) {
            context.RemoveSender = sender;
            $("#RemoveBtnPopUp").modal('show');
        };

        context.onChkRegesteredMac = function (sender) {

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
                        $("#ChangeEquipmentPopUp").find(".messageArea .successArea").show().find(".status").text("Kundens udstyr er afprovisioneret. Tjek status i linjestatusværktøj.");
                    }
                    else {
                        $("#ChangeEquipmentPopUp").find(".messageArea .alert").hide();
                        $("#ChangeEquipmentPopUp").find(".messageArea .errorArea").show().find(".status").text("Vi kan ikke fjerne Mac adresse fra kundens udstyr");
                    }

                    $("#RemoveBtnPopUp").modal('hide');
                },
                errorcallback: function () {
                },
                messageSuccess: "",
                messageError: "",
                messageProcess: "Afprovisionering igang"
            }, sender);
        };


    })(TdcChangeEquipment);

    $(document).ready(function () {
        TdcAsyncWebPartLoader.$rootElement.on(TdcAsyncWebPartLoader.toolLoadedEventName, function (event, html) {
            if ($(html).hasClass("TdcChangeEquipment")) {
                ChangeEquipmentPopUp.init(html);
            }
        });
    });

})(jQuery);