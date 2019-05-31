/// <reference path="../asyncwebpartloader.js" />
/// <reference path="../guidemanager.js" />
/// <reference path="../tabmanager.js" />
/// <reference path="CustomerOpenCases.js" />



if (TdcCustomerClosedCases == null || typeof (TdcCustomerClosedCases) != "object") {
    var TdcCustomerClosedCases = new Object();
}

(function ($) {
    (function (context) {
        if (TdcAsyncWebPartLoader.portalId == TdcAsyncWebPartLoader.portalIds.CIP) {
            TranslationsJs.CustomerClosedCases = "Afsluttede eTray sager";
        };
       
        context.init = function () {
            TdcAsyncWebPartLoader.getToolPlaceholder('CustomerClosedCases').on('click', ".CustomerClosedCases_Table td", cellClicked)
        };
       

        context.ToggleConfigCategory = function (sender) {
            var $sender = $(sender);

            var s = $sender.find('span').filter('#pullUpDown');
            if (s.hasClass('ion-chevron-up')) {
                s.removeClass().addClass('ion-chevron-down pull-right');
            } else {
                s.removeClass().addClass('ion-chevron-up pull-right');
            }

            if ($sender.next().filter(':visible').length > 0) {
                $sender.next().toggle();
            }
            else {
               // $('.CustomerClosedCases').find('.closedCasesDetails').hide();
               // $('.CustomerClosedCases .closedCasesHeader').not($sender).find('span').filter('#pullUpDown').removeClass().addClass('ion-chevron-down pull-right');
                $sender.next().toggle();
            }
        };

        function createErrorWindow(expression) {

            switch (expression) {
                case "MsgColumbusError":
                    TdcUI.createErrorWindow($(".CustomerClosedCases .columbusErrorHtml").val());
                    break;
                case "MsgEtrayError":
                    TdcUI.createErrorWindow($(".CustomerClosedCases .etrayErrorHtml").val());
                    break;
                case "MsgBierError":
                    TdcUI.createErrorWindow($(".CustomerClosedCases .bierErrorHtml").val());
                    break;
                case "MsgFasoError":
                    TdcUI.createErrorWindow($(".CustomerClosedCases .fasErrorHtml").val());
                    break;

                default:
                    break;
            }
        };

            function cellClicked() {
                var $row = $(this).closest('tr');
                var id = $row.attr('data-id');
                var type = $row.attr('data-type');
                var link = $row.attr('data-link');
                var selectedlid = $row.attr('data-selectedlid');
                context.toolName = "ToolCustomerClosedCasesDetails";
                context.ColumbusDetails = "ClosedCasesDetails";
                if (type == 'Faso' && id)
                {
                    var note = $row.find('.caseNote').text();
                    ToolDkpFasUpdate.openFasoTab(id, note, selectedlid, 'ClosedFaso', 'ClosedFaso');
                }
                else if (type == 'Columbus')
                {
                    var newLid = $row.attr('data-newlid');
                    var oldLid = $row.attr('data-oldlid');
                    var kusagKd = $row.attr('data-kusagKd');
                    var note = $row.attr('data-note');
                    var transcode = $row.attr('data-transcode');
                    var orderText = $row.attr('data-ordertext');
                    var createdDateTime = $row.attr('data-createddatetime');
                    var fakDate = $row.attr('data-fakdate');
                    var cancellationDate = $row.attr('data-cancellationdate');
                    var orderDate = $row.attr('data-orderdate');

                    ColumbusDetails = {
                        id: id,
                        newLid: newLid,
                        oldLid: oldLid,
                        kusagKd: kusagKd,
                        note: note,
                        transcode: transcode,
                        orderText: orderText,
                        createdDateTime: createdDateTime,
                        fakDate: fakDate,
                        cancellationDate: cancellationDate,
                        orderDate: orderDate,
                    }
                    TdcTabManager.addToolSessionTab(event, context.toolName, context.ColumbusDetails, true, true, function () { context.ShowDetailCustomerClosedCasesTool(ColumbusDetails) });
                }
                else if (link)
                {
                    window.open(link, '_blank');
                }
                else
                {
                    createErrorWindow(type);
                }
            };
           
            context.ShowDetailCustomerClosedCasesTool = function (ColumbusDetails) {

                var dataContext = {
                    noteId: context.toolName,
                };
              
                TdcAsyncWebPartLoader.ShowTool({
                    toolname: "ToolCustomerClosedCasesDetails",
                    context: dataContext,
                    callback: function ($webpart) {

                        $webpart.find('#columbus .ColumbusDetails #orderDate').html(ColumbusDetails.orderDate);
                        $webpart.find('#columbus .ColumbusDetails #orderNo').html(ColumbusDetails.id);
                        $webpart.find('#columbus .ColumbusDetails #newLid').html(ColumbusDetails.newLid);
                        $webpart.find('#columbus .ColumbusDetails #oldLid').html(ColumbusDetails.oldLid);
                        $webpart.find('#columbus .ColumbusDetails #transcode').html(ColumbusDetails.transcode);
                        $webpart.find('#columbus .ColumbusDetails #orderText').html(ColumbusDetails.orderText);
                        $webpart.find('#columbus .ColumbusDetails #customerCaseText').html(ColumbusDetails.note);

                        $webpart.find(sectionName).parent().find(".ion-chevron-down").attr("data-alternateicon", "ion-chevron-down pull-right margin-to-arrow")
                        $webpart.find(sectionName).parent().find(".ion-chevron-down").removeClass("ion-chevron-down").addClass("ion-chevron-up");
                        $webpart.find(sectionName).toggle();
                      }
                });
            };

        })(TdcCustomerClosedCases);

        $(TdcCustomerClosedCases.init);
    })(jQuery);