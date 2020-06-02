using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterswitchNameEnquiry.DataAccess;
using InterswitchNameEnquiry.DTO;

namespace InterswitchNameEnquiry.Logic
{
    public class NameEnquiryLogic
    {
        public static NameEnquiryResponseResult DoNameEnquiry(string customerId)
        {

            var obj = new NameEnquiryResponseResult();
            try
            {
                if (customerId == "0003321999")
                {
                    obj.AccountCurrency = "566";
                    obj.AccountType = "10";
                    obj.CountryCode = "NG";
                    obj.CountryOfBirth = "Nigeria";
                    obj.CustomerAddress = new CustomerAddress { AddrLine1 = "No 30", AddrLine2 = "Benue Road", City = "Otukpo", PostalCode = "", StateCode = "" };
                    obj.CustomerID = "0003321999";
                    obj.CustomerName = new CustomerName { FirstName = "Balarabe", LastName = "Rilwan", OtherNames = "Musa" };
                    obj.CustomerPhoneNo = "08023221100";
                    obj.DOB = "05/09/1960";
                    obj.Nationality = "Nigerian";
                    obj.ResponseCode = "00";
                }
                else { obj.ResponseCode = "25"; }

                Repository.SaveRequestResponse(customerId, obj);

            }
            catch (Exception e)
            {
                Utils.LogError(e, "");
            }
            return obj;
        }
        public static AccessTokenResponse GenerateToken()
        {
            var token = Utils.GetToken();
            Repository.SaveAccessToken(new DataAccess.AccessTokenLog() { AccessToken = token, DateCreated = DateTime.Now, ExpiresIn = 3600, ExpiryDate = DateTime.Now.AddHours(1), TokenType = "bearer" });
            return new DTO.AccessTokenResponse() { access_token = token, expires_in = 3600, token_type = "bearer" };
        }

        public static DebitResponse DoDebit(DebitRequest req)
        {
            var bankRef = Utils.RandomString(50);
            //do prop validation
            // waiting for bank debit service  to do debit 

            Repository.SaveDebitTransactionLog(new DataAccess.DebitTransactionLog()
            {
                Amount = req.amount,
                ChannelId = 1,
                DateCreated = DateTime.Now,
                DestinationAccount = req.destinationAccount,
                DestinationAccountName = req.destinationAccountName,
                DestinationBankCode = req.destinationBankCode,
                InterswitchTransactionId = req.transactionId,
                SourceAccount = req.sourceAccount,
                SourceAccountName = req.sourceAccountName,
                BankRequestReference = bankRef
            });
            return new DTO.DebitResponse() { amount = req.amount, requestReference = bankRef, responseCode = "00", responseMessage = "Succesful", transactionDate = DateTime.Now, transactionId = req.transactionId };
        }
        ////public static Customer GetCustomerInfoByCustId(string CustomerId)
        ////{
        ////    Customer myCustomer = new Customer();
        ////    string SqlQuery = FinnacleSqlQuery.GetCustomerInfoByCustomerId(CustomerId);
        ////    using (OleDbDataReader myDataReader = BAOracleConnector.GetRows(SqlQuery, AppConfig.FinnacleConnection))
        ////    {
        ////        if (Information.IsNothing(myDataReader))
        ////        {
        ////            myCustomer = null;
        ////        }
        ////        else
        ////        {
        ////            myDataReader.Read();
        ////            myCustomer = CustomerDB.FillDataRecord(myDataReader);
        ////            string phoneSQL = FinnacleSqlQuery.GetPhoneNoCRM(CustomerId);
        ////            myCustomer.MobileNumber = BAOracleConnector.GetStringValue(phoneSQL, AppConfig.FinnacleConnection);
        ////            string emailSQL = FinnacleSqlQuery.GetEmailOnCRM(CustomerId);

        ////        }
        ////    }

        ////}

        private static string finacleQuery = @"  select foracid Accountnumber ,
           CUST_LAST_NAME LastName,
           CUST_FIRST_NAME   FirstName ,
           CUST_MIDDLE_NAME OtherNames,
           (ADDRESS_LINE1 || ' ' || ADDRESS_LINE2) AddrLine1,
           ADDRESS_LINE3        AddrLine2,
           (select trim(ref_desc) from tbaadm.rct where ref_rec_type = '01' and ref_code = a.state ) City ,
           STATE StateCode,
           ZIP                         PostalCode ,
           (select convert(phoneno,'US7ASCII','WE8ISO8859P1')  from crmuser.phoneemail d where phoneoremail= 'PHONE' and d.orgkey in (select cif_id from tbaadm.gam x where x.foracid = g.foracid) and rownum = 1) CustomerPhoneNo ,
           decode(schm_type,'ODA','CURRENT','SBA','SAVINGS','CAA','CURRENT','TDA','TERM DEPOSIT')  AccountType ,
          ACCT_CRNCY_CODE AccountCurrency,
           COUNTRY     CountryCode ,
           (select identificationtype || ' --> '||docdescr from  crmuser.entitydocument where orgkey=g.cif_id and ismandatory='Y' and preferreduniqueid = 'Y' and docreceiveddate is not null  and rownum = 1)   Identification  ,
          (select referencenumber from crmuser.entitydocument where orgkey = g.cif_id and ismandatory = 'Y' and preferreduniqueid = 'Y' and docreceiveddate is not null  and rownum = 1)   IdNumber ,                                                                                             
           (select countryofissue from crmuser.entitydocument where orgkey = g.cif_id and ismandatory = 'Y' and preferreduniqueid = 'Y' and docreceiveddate is not null and rownum = 1)   CountryOfIssue , 
           (select docexpirydate from crmuser.entitydocument where orgkey = g.cif_id and ismandatory = 'Y' and preferreduniqueid = 'Y' and docreceiveddate is not null and rownum = 1) Id_Expiry_date ,
           country Nationality,
           cust_dob     DOB
from tbaadm.gam g, crmuser.accounts a
where g.cif_id = a.orgkey
and entity_cre_flg= 'Y' and del_flg = 'N' and acct_cls_flg = 'N'
and acct_ownership!='O'
and schm_type in ('ODA','CAA','SBA')
and foracid = ''
    }";

    }
}