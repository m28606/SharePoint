/// <reference path="../guidemanager.js" />
/// <reference path="../pagestatesmanager.js" />

if (TdcPortalUserGuides == null || typeof (TdcPortalUserGuides) != "object") {
    var TdcPortalUserGuides = new Object();
}

(function ($) {
    (function (context) {

        context.init = function () {
            TdcAsyncWebPartLoader.$rootElement.on('click', '.portalUserGuideItem', itemClicked);
        };

        context.paginatorClicked = function (sender, pageNumber) {
            var $thisWp = $(sender).closest('.PortalUserGuidesWP');

            TdcAsyncWebPartLoader.ShowTool({
                toolname: "PortalUserGuides",
                context: {
                    guideSessionStatus: $thisWp.attr('data-guidesessionstatus'),
                    pageNumber: pageNumber,
                }
            });
        };


        function itemClicked() {
            var $row = $(this);
            var noteId = $row.attr('data-noteid');
            var customerId = $row.attr('data-customerid');
            var portalId = $row.attr('data-portalid');
            var entityTitle = $row.attr('data-entitytitle');
            var entityId = $row.attr('data-entityid');
            var resumable = $row.attr('data-isresumable');
            var sessionId = $row.attr('data-sessionId');
            var userId = $row.attr('data-userid');
            var noteText = $row.attr('data-notetext');
            var stepId = $row.attr('data-stepid');
            var toolName = $row.attr('data-toolname');
            var param = {
                noteId: noteId,
                sessionId: sessionId,
                userId: userId,
                noteText: noteText,
                stepId: stepId,
                portalId: portalId,
                entityTitle: entityTitle,
                entityId: entityId,
                toolName: toolName,
                customerId: customerId,
                row: $row
            };
            //Show popup to close non-resumable sessions only if session's portal id is same as that of current portal instance
            if (resumable == "False" && portalId == TdcAsyncWebPartLoader.portalId) {
                $('#PopupEndGuide').data("param", param).modal();
                $('.btn').removeClass("active");
            }
            else {

                TdcPageStatesManager.goToNewSession();

                /** 
                * @Description: Resumes Articles when loading from active session or loading articles from one instance to another 
                * @Author : Radhika Pokale  
                * @Incident : IM403376 - ServicePortal BC Portal
                **/
                var params = {
                    customerId: customerId,
                    //TODO:Akshay - Need to check if the rootcustomerid mapping is correct
                    rootCustomerId: customerId,
                    sectionTitle: $row.attr('data-section'),
                    subsectionTitle: "",
                    entityId: entityId,
                    entityType: $row.attr('data-entitytype') == "ArticleNote" ? "Article" : "Guide",
                    entityTitle: entityTitle,
                    noteText: "",
                    portalSection: "",
                    parentAccountNo: $row.attr('data-parentaccountno'),
                    sessionNoteId: noteId,
                };

                if (params.entityType == "Article") {

                    TdcGuideManager.resumeArticle(params);
                }
                else {
                    TdcGuideManager.resumeGuideOrShowHistoryInformation(noteId, entityId, entityTitle, portalId, TdcParkedGuides.refresh);
                }
                //TdcCustomerInformation.loadCustomer(customerId);
                TdcCustomerInformation.loadCustomer(customerId, undefined, false, undefined, false);
            }
        }

        context.EndGuideYes = function (sender) {
            var noteId = $('#PopupEndGuide').data('param').noteId;
            var portalUserId = $('#PopupEndGuide').data('param').userId;
            var sessionId = $('#PopupEndGuide').data('param').sessionId;
            var noteText = $('#PopupEndGuide').data('param').noteText;
            var stepId = $('#PopupEndGuide').data('param').stepId;
            var customerId = $('#PopupEndGuide').data('param').customerId;
            var toolName = $('#PopupEndGuide').data('param').toolName;
            var row = $('#PopupEndGuide').data('param').row;
            context.EndGuide(noteId, portalUserId, sessionId, noteText, stepId, toolName, customerId, row);
        };

        context.EndGuideNo = function (sender) {
            $('#PopupEndGuide').modal('hide');
            var noteId = $('#PopupEndGuide').data('param').noteId;
            var portalId = $('#PopupEndGuide').data('param').portalId;
            var entityTitle = $('#PopupEndGuide').data('param').entityTitle;
            var entityId = $('#PopupEndGuide').data('param').entityId;
            var customerId = $('#PopupEndGuide').data('param').customerId;
            TdcPageStatesManager.goToNewSession();
            TdcGuideManager.resumeGuideOrShowHistoryInformation(noteId, entityId, entityTitle, portalId, TdcParkedGuides.refresh);
            TdcCustomerInformation.loadCustomer(customerId, undefined, false, undefined, false);
        };

        context.EndGuide = function (noteId, portalUserId, sessionId, noteText, stepId, toolName, customerId, row) {

            TdcAsyncWebPartLoader.DoAction({
                toolname: "PortalUserGuides",
                action: 'EndGuide',
                context: {
                    guidenoteId: noteId,
                    portalUserId: portalUserId,
                    sessionId: sessionId,
                    noteText: noteText,
                    stepId: stepId
                },
                callback: function () {
                    $('#PopupEndGuide').filter(':visible').modal('hide');

                    var $thisWp = row.closest('.AsyncWebpartLoader');
                    var guideSessionStatus = $thisWp.attr('data-guidesessionstatus');
                    TdcAsyncWebPartLoader.ShowTool({
                        toolname: toolName,
                        context: {
                            guideSessionStatus: guideSessionStatus,
                            customerId: customerId,
                        }
                    });
                },
                messageProcess: TranslationsJs.Completing_guide,
                messageSuccess: TranslationsJs.Guide_was_completed,
                messageError: TranslationsJs.Complete_guide + TranslationsJs.failed + '.',
            }, row);
        };

    })(TdcPortalUserGuides);

    $(TdcPortalUserGuides.init);

})(jQuery);