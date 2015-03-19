<%@ Page Language="C#" %>
<%@ Import namespace="System.Net" %>
<%@ Import namespace="System.IO" %>

<%	
	/*********************************
	* This example shows a server side piece of code which is taking a set of form parameters from 
	* an enquiry form and creating a lead plus the initial post and action	
	**********************************/

	// Take in the request parameters from the enquiry form
	var name        = Request["name"] ?? "";	
    var email        = Request["email"] ?? "";	
    var message      = Request["message"] ?? "";
	      
	using (WebClient client = new WebClient())
    {
		// The following 3 lines add the authentication information to your request
        string authInfo = "api.user@yourdomain.com:YourApiUserPassword";
        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        client.Headers["Authorization"] = "Basic " + authInfo;

		// Send the required parameters to the request.  Check what you need by doing the same action in your browser with your F12 developer tools open
        System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
        reqparm.Add("AssignedToCurrentOwnerId", "508b904e-a540-11e4-8d58-00155d569b92");  
        reqparm.Add("CustomerCreateType", "CompleteLater");
        reqparm.Add("Description", email + message + name);
        reqparm.Add("CampaignId", "3e1a3aa0-ea44-11e2-8cdf-00155d569b92");
        reqparm.Add("FirstActionTitle", "New Enquiry");
        reqparm.Add("FirstActionDate", DateTime.Now.ToString());
        reqparm.Add("FirstPost", message);
        reqparm.Add("CustomerName", name);
        reqparm.Add("ContactFirstName", name);
        reqparm.Add("ContactEmail", email);            

        try
        {
            // Post the transaction to Harmony
            byte[] responsebytes = client.UploadValues("https://dls.harmonyerp.com/api/LeadTransaction/CreateLead", "POST", reqparm);
            theResponse = Encoding.UTF8.GetString(responsebytes);
        }
        catch (WebException e)
        {            
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                var returnCode = ((HttpWebResponse)e.Response).StatusCode;
                
                if (returnCode == 500 || returnCode == 412 || returnCode == 400)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
						// Deal with the exception message from harmony here. 
						// 400 is a validation error (e.g. you need to select a user)
						// 412 is a confirmation error (e.g. are you sure you want to do this?). Send confirmedWarnings=true along with you request to confirm
						// 500 is an error condition and Harmony returns the error message
                    }
                }
            }            
        }               
    }
        
%>
