/// <reference path="../asyncwebpartloader.js" />


if (TdcCompletedGuidesForPortalUser == null || typeof (TdcCompletedGuidesForPortalUser) != "object") {
    var TdcCompletedGuidesForPortalUser = new Object();
}

(function ($) {
    (function(context) {
        context.init = function () {
            TdcAsyncWebPartLoader.$rootElement.on('click', '.completedGuidesForPortalUserWP .completedGuidesForPortalUserClassLink', showCustomerBtnClicked);

            TdcAsyncWebPartLoader.$rootElement.on('click', '.completedGuidesForPortalUserWP .showGuideForPortalUser', itemClicked);
        };

        function itemClicked() {
            var $sender = $(this);
            var $row = $sender.closest('tr');
            var noteId = $row.attr('data-noteid');
            var customerId = $row.attr('data-customerid');
            var entityTitle = $row.attr('data-entitytitle');
            var entityId = $row.attr('data-entityid');

            TdcPageStatesManager.goToNewSession();
            TdcCustomerInformation.loadCustomer(customerId, undefined, false, undefined, false);
            TdcGuideManager.openGuideHistoryTab(noteId, entityId, entityTitle);

        }

        context.paginatorClicked = function(sender, pageNumber) {
            TdcAsyncWebPartLoader.ShowTool({
                toolname: "CompletedGuidesForPortalUser",
                context: {
                    pageNumber: pageNumber
                }
            });
        };
        

        function showCustomerBtnClicked (evt) {
            var $sender = $(this);
            var $row = $sender.closest('tr');
            var customerId = $row.attr('data-customerid');
            TdcCustomerInformation.loadCustomer(customerId, undefined, false, undefined, false);
            TdcPageStatesManager.goToNewSession();
            evt.preventDefault();
            evt.stopPropagation();
        };

    })(TdcCompletedGuidesForPortalUser);
    
    $(TdcCompletedGuidesForPortalUser.init);

})(jQuery);