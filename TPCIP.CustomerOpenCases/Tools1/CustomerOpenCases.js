/// <reference path="../asyncwebpartloader.js" />
/// <reference path="../guidetools/tooldkpfasupdate.js" />



if (TdcCustomerOpenCases == null || typeof (TdcCustomerOpenCases) != "object") {
    var TdcCustomerOpenCases = new Object();
}

(function ($) {
    (function (context) {
        if (TdcAsyncWebPartLoader.portalId == TdcAsyncWebPartLoader.portalIds.CIP) {
            TranslationsJs.CustomerOpenCases = "Åbne eTray sager";
        };

        context.init = function () {
            TdcAsyncWebPartLoader.getToolPlaceholder('CustomerOpenCases').on('click', ".CusomerOpenCases_Table td", openCaseCellClicked);


        }

        context.closeOpenCasesPopup = function (sender) {
            $('.ColumbusOpenCases_popup').filter(':visible').hide();

        }

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
               // $('.CustomerOpenCases').find('.openCasesDetails').hide();
               // $('.CustomerOpenCases .openCasesHeader').not($sender).find('span').filter('#pullUpDown').removeClass().addClass('ion-chevron-down pull-right');
                $sender.next().toggle();    
            }
        };
    
       
        function createErrorWindow(expression) {
           
            switch (expression) {
                case "MsgEtrayError":
                    TdcUI.createErrorWindow($(".CustomerOpenCases .etrayErrorHtml").val());
                    break;
                case "MsgBierError":
                    TdcUI.createErrorWindow($(".CustomerOpenCases .bierErrorHtml").val());
                    break;
                case "MsgFasoError":
                    TdcUI.createErrorWindow($(".CustomerOpenCases .fasErrorHtml").val());
                    break;
                case "MsgCUError":
                    TdcUI.createErrorWindow($(".CustomerOpenCases .cuErrorHtml").val());
                    break;

                default:
                    break;
            }

        }

        function columbusClickPopUp($sender) {
            var $window = $(window);
            var bottomWindowEdge = $window.scrollTop() + $window.height();
            var distanceFromBottom = bottomWindowEdge - $sender.offset().top;
            var $popup = $('.ColumbusOpenCases_popup');
            $popup.css({
                'top': '',
                'bottom': '',
            });

            $popup.show();


            if ($popup.outerHeight() < distanceFromBottom) $popup.css('top', 0);
            else $popup.css('bottom', 0);


        }

        $('.ColumbusOpenCases_popup #hideNoteButton').on('click', function (e) {
            $('.ColumbusOpenCases_popup').filter(':visible').hide();
            e.stopImmediatePropagation();
        });

        function openCaseCellClicked() {
                 var $row = $(this).closest('tr');
                 var id = $row.attr('data-id');
                 var type = $row.attr('data-type');
                 var link = $row.attr('data-link');
                 var selectedlid = $row.attr('data-selectedlid');
            if (type == 'CU' && id) {
                columbusClickPopUp($row);
            }

            else if (type == 'Faso' && id) {
                  var note = $row.find('.caseNote').text();
                  ToolDkpFasUpdate.openFasoTab(id, note, selectedlid, 'OpenFaso');
                 } else if (link) {
                       window.open(link, '_blank');
                 }
                   else {
                      createErrorWindow(type);
                 }
           
        };

     

    })(TdcCustomerOpenCases);

    $(TdcCustomerOpenCases.init);
})(jQuery);