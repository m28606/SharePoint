﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.PortalUserGuides.DataModel;

namespace TPCIP.PortalUserGuides
{
    [ServiceContract]
    public interface ICustomerAgent
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "notes/userid/{userId}?pageSize={pageSize}&page={page}&status={status}")] //array of statuses can not be serialized in querystring
        List<CustomerNote> getCustomerNotesByUserId(string userId, int page, int pageSize, string status);

        [OperationContract]
        [WebGet(UriTemplate = "note/{id}")]
        CustomerNote getCustomerNote(string id);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "note")]
        bool updateCustomerNote(CustomerNote customerNote);
    }
}
