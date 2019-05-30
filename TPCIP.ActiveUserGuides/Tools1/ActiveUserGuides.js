/// <reference path="../guidemanager.js" />
/// <reference path="../pagestatesmanager.js" />

if (TdcActiveUserGuides == null || typeof (TdcActiveUserGuides) != "object") {
    var TdcActiveUserGuides = new Object();
}

(function ($) {
    (function (context) {

        context.init = function () {
            TdcAsyncWebPartLoader.$rootElement.on('click', '.activeUserGuideItem', itemClicked);
        };

        context.paginatorClicked = function (sender, pageNumber) {
            var $thisWp = $(sender).closest('.ActiveUserGuidesWP');

            TdcAsyncWebPartLoader.ShowTool({
                toolname: "ActiveUserGuides",
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

                var params = {
                    customerId: customerId,
                    //TODO:Akshay - Need to check if the rootcustomerid mapping is correct
                    rootCustomerId: customerId,
                    sectionTitle: $row.attr('data-section'),
                    subsectionTitle: "",
                    entityId: entityId,
                    entityType: $row.attr('data-entityType') == "ArticleNote" ? "Article" : "Guide",
                    entityTitle: entityTitle,
                    noteText: "",
                    portalSection: "",
                    parentAccountNo: $row.attr('data-parentAccountNo'),
                    sessionNoteId: noteId,
                };
                
                if (params.entityType == "Article") {

                    TdcGuideManager.resumeArticle(params);
                }
                else {
                    TdcGuideManager.resumeGuideOrShowHistoryInformation(noteId, entityId, entityTitle, portalId, TdcParkedGuides.refresh);
                }
                TdcCustomerInformation.loadCustomer(customerId, undefined, false, undefined, false);
                
            }
        }

    })(TdcActiveUserGuides);

    $(TdcActiveUserGuides.init);

})(jQuery);