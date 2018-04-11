
namespace MerchantPortal.Helper
{
    public class CustomMessage
    {
        public static string SaveMessage()
        {
            return "Data saved successfully.";
        }
        public static string SaveMessage(string objectName)
        {
            return string.Format("{0} saved successfully.", objectName);
        }
        public static string InvalidModel(string objectName)
        {
            return string.Format("Give valid {0} information.", objectName);
        }
        public static string SaveErrorMessage(string objectName)
        {
            return string.Format("Unable to save {0} . Please try again!! ", objectName);
        }
        public static string UpdateMessage()
        {
            return "Data updated successfully.";
        }
        public static string UpdateMessage(string objectName)
        {
            return string.Format("{0} updated successfully.", objectName);
        }

        public static string UpdateErrorMessage(string objectName)
        {

            return string.Format("Unable to updated {0} . Please try again!! ", objectName);
        }

        public static string ApprovedMessage()
        {
            return "Data approved successfully.";
        }
        public static string ApprovedMessage(string objectName)
        {
            return string.Format("{0} approved successfully.", objectName);
        }
        public static string ApprovedErrorMessage(string objectName)
        {
            return string.Format("Unable to approved {0} . Please try again!! ", objectName);
        }

        public static string RejectedMessage(string objectName)
        {
            return string.Format("{0} rejected successfully.", objectName);
        }
        public static string RejectedErrorMessage(string objectName)
        {
            return string.Format("Unable to rejected {0} . Please try again!! ", objectName);
        }

        public static string AmmendmentMessage(string objectName)
        {
            return string.Format("{0} amendment successfully.", objectName);
        }
        public static string AmmendmentErrorMessage(string objectName)
        {
            return string.Format("Unable to ammendment {0} . Please try again!! ", objectName);
        }
        public static string DeleteConfirmationMessage(string objectName)
        {
            return string.Format("Are you sure want to delete {0}?", objectName);
        }
        public static string DeleteMessage()
        {
            return "Data deleted successfully.";
        }
        public static string DeleteMessage(string objectName)
        {
            return string.Format("{0} deleted successfully.", objectName);
        }
        public static string DeleteErrorMessage(string objectName)
        {
            return string.Format("Unable to delete {0} . Please try again!! ", objectName);
        }
        public static string ActionPermission(string objectName)
        {
            return string.Format("You are not permitted to {0} action.", objectName);
        }
        public static string AddPermission(string objectName)
        {
            return string.Format("You are not permitted to save {0}.", objectName);
        }
        public static string EditPermission(string objectName)
        {
            return string.Format("You are not permitted to  edit {0}.", objectName);
        }
        public static string DenpendentErrorMessage(string denpendent, string entry)
        {
            return string.Format(" Without {0} Entry you can not do {1} entry !", denpendent, entry);
        }
        public static string LinkButtonLoadErrorMessage(string objectstring)
        {
            return string.Format("{0} information Can not Load properly.Please try again !", objectstring);
        }
        public static string DeletePermission(string objectName)
        {
            return string.Format("You are not permitted to delete {0}.", objectName);
        }
        public static string SelectPermission(string objectName)
        {
            return string.Format("You are not permitted to view {0}.", objectName);
        }
        public static string ExportNoRecords(string objectName)
        {
            return string.Format("No {0} found to export.", objectName);
        }
        public static string GridNoRecords(string objectName)
        {
            return string.Format("No {0} record found .", objectName);
        }
        public static string CommoboxLoadError(string objectName)
        {
            return string.Format("{0} not loading properly. Please try again!!", objectName);
        }
        public static string DataGridLoadError(string objectName)
        {
            return string.Format("{0} not loading properly. Please try again!!", objectName);
        }
        public static string ControlLoadError(string objectName)
        {
            return string.Format("{0} not loading properly. Please try again!!", objectName);
        }
        public static string Compare_PaymentAmount_CurrentBalance()
        {
            return "Insufficient  Balance.";
        }
        public static string Calculation_Payment_Rate()
        {
            return "Please press calculate button";
        }
        public static string Report_Date_Entry(string objectName)
        {
            return string.Format("PLease Select/Entry {0}", objectName);
        }
        public static string AddressLayerWarraning(string CountryName)
        {
            return string.Format("Please entry address layer for  {0}. Try again.", CountryName);
        }
        public static string ReportReturnMessage(string objectName)
        {
            return string.Format(" No data found for {0} report.", objectName);
        }
        public static string ConfigurationDataNotFoundMessage
        {
            get { return "No exchange rate is found for the amount and the currency. please check the amount"; }
        }
        public static string CustomerValidationMessage()
        {
            return string.Format("Entered information does not match with the customer's profile. Please try again with the correct information. If this message comes up again for this customer, please contact BRAC Saajan Customer Service Team by quoting the customer reference number.");
        }
    }
}
