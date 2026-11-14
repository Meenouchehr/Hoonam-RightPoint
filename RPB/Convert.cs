using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Convert
    {
        #region ACC
        public static RPD.TBL_ACC ToACC(RPC.ACC item)
        {
            RPD.TBL_ACC obj = new RPD.TBL_ACC();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_Type = item.RedundantType;
            return obj;
        }

        public static RPC.ACC ToACC(RPD.TBL_ACC item)
        {
            RPC.ACC obj = new RPC.ACC();
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_Type;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }

        #endregion

        #region AMD
        public static RPD.TBL_AMD ToAMD(RPC.AMD item)
        {
            RPD.TBL_AMD obj = new RPD.TBL_AMD();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Month = item.Month;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Type = item.RedundantType;

            return obj;
        }
        public static RPC.AMD ToAMD(RPD.TBL_AMD item)
        {
            RPC.AMD obj = new RPC.AMD();
            obj.ID = item.FLD_ID_PK;
            obj.Month = item.FLD_Month;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_Type;

            return obj;
        }

        #endregion

        #region Answer
        public static RPD.TBL_Answer ToAnswer(RPC.Answer item)
        {
            RPD.TBL_Answer obj = new RPD.TBL_Answer();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Option = item.Option;
            obj.FLD_Program_FK = item.Program;
            obj.FLD_Respondent_FK = item.Respondent;
            return obj;
        }
        public static RPC.Answer ToAnswer(RPD.TBL_Answer item)
        {
            RPC.Answer obj = new RPC.Answer();
            obj.ID = item.FLD_ID_PK;
            obj.Option = item.FLD_Option;
            obj.Program = item.FLD_Program_FK;
            obj.Respondent = item.FLD_Respondent_FK;
            return obj;
        }
        public static RPC.Answer ToAnswer(RPD.VIW_Answer item)
        {
            RPC.Answer obj = new RPC.Answer();
            obj.After = item.FLD_After;
            obj.Before = item.FLD_Before;
            obj.ID = item.FLD_ID_PK;
            obj.InquirerCompanyTitle = item.FLD_InquirerCompanyTitle;
            obj.InquirerGender = item.FLD_InquirerGender;
            obj.InquirerName = item.FLD_InquirerName;
            obj.InquirerTitle = item.FLD_InquirerTitle;
            obj.IsPublish = item.FLD_IsPublish;
            obj.Middle = item.FLD_Middle;
            obj.Option = item.FLD_Option;
            obj.Program = item.FLD_Program_FK;
            obj.ProgramTitle = item.FLD_ProgramTitle;
            obj.QNo = item.FLD_QNo;
            obj.Respondent = item.FLD_Respondent_FK;
            obj.ResponderAges = item.FLD_ResponderAges;
            obj.ResponderCooperation = item.FLD_ResponderCooperation;
            obj.ResponderEducation = item.FLD_ResponderEducation;
            obj.ResponderFamilyIncome = item.FLD_ResponderFamilyIncome;
            obj.ResponderGender = item.FLD_ResponderGender;
            obj.ResponderName = item.FLD_ResponderName;
            obj.ResponderOccupation = item.FLD_ResponderOccupation;
            obj.Title = item.FLD_Title;


            return obj;
        }

        #endregion

        #region Compaign
        public static RPD.TBL_Campaign ToCampaign(RPC.Campaign item)
        {
            RPD.TBL_Campaign obj = new RPD.TBL_Campaign();
            obj.FLD_Comment = item.Comment;
            obj.FLD_EnglishComment = item.EnglishComment;
            obj.FLD_Company_FK = item.Company;
            obj.FLD_EndDate = item.EndDate;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_IsActive = item.IsActive;
            obj.FLD_StartDate = item.StartDate;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_TimeRange = item.TimeRange;
            obj.FLD_Description = item.Description;
            obj.FLD_ACC_FK = item.ACC;
            obj.FLD_Questionnaire_FK = item.Questionnaire;
            obj.FLD_Lottary_FK = item.Lottary;
            foreach (RPC.CampaignAccess Access in item.AccessList)
                obj.TBL_CampaignAccess.Add(Convert.ToCampaignAccess(Access));
            return obj;
        }

        public static RPC.Campaign ToCampaign(RPD.VIW_Campaign item)
        {
            RPC.Campaign obj = new RPC.Campaign();
            obj.ACC = item.FLD_ACC_FK;
            obj.ACCTitle = item.FLD_ACCTitle;
            obj.Comment = item.FLD_Comment;
            obj.EnglishComment = item.FLD_EnglishComment;
            obj.Company = item.FLD_Company_FK;
            obj.CompanyImage = item.FLD_CompanyImage;
            obj.CompanyTel = item.FLD_CompanyTel;
            obj.CompanyTitle = item.FLD_CompanyTitle;
            obj.EndDate = item.FLD_EndDate;
            obj.GroupID = item.FLD_Group_FK;
            obj.GroupComment = item.FLD_GroupComment;
            obj.GroupName = item.FLD_GroupName;
            obj.ID = item.FLD_ID_PK;
            obj.IsActive = item.FLD_IsActive;
            obj.Lottary = item.FLD_Lottary_FK;
            obj.LottaryTitle = item.FLD_LottaryTitle;
            obj.QNo = item.FLD_QNo;
            obj.QTitle = item.FLD_QTitle;
            obj.Questionnaire = item.FLD_Questionnaire_FK;
            obj.StartDate = item.FLD_StartDate;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.Description = item.FLD_Description;
            obj.TimeRange = item.FLD_TimeRange;


            return obj;
        }

        #endregion


        #region City
        public static RPD.TBL_City ToCity(RPC.City item)
        {
            RPD.TBL_City obj = new RPD.TBL_City();

            obj.FLD_ID_PK = item.ID;          
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
          
            return obj;
        }

        public static RPC.City ToCity(RPD.TBL_City item)
        {
            RPC.City obj = new RPC.City();
          
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;


            return obj;
        }

        #endregion


        #region CompaignAccess
        public static RPD.TBL_CampaignAccess ToCampaignAccess(RPC.CampaignAccess item)
        {
            RPD.TBL_CampaignAccess obj = new RPD.TBL_CampaignAccess();
            obj.FLD_Campaign_FK = item.Campaign;
            obj.FLD_User_FK = item.User;
            obj.FLD_ID_PK = item.ID;
            return obj;
        }

        public static RPC.CampaignAccess ToCampaignAccess(RPD.VIW_CampaignAccess item)
        {
            RPC.CampaignAccess obj = new RPC.CampaignAccess();
            obj.Campaign = item.FLD_Campaign_FK;
            obj.Comment = item.FLD_Comment;
            obj.EnglishComment = item.FLD_EnglishComment;
            obj.ID = item.FLD_ID_PK;
            obj.Name = item.FLD_Name;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.User = item.FLD_User_FK;
            obj.CompanyName = item.FLD_CompanyName;
            obj.CompanyEnglishName = item.FLD_CompanyEnglishName;
            obj.GroupName = item.FLD_GroupName;
            obj.GroupEnglishName = item.FLD_GroupEnglishName;
            obj.QuestionnaireTitle = item.FLD_QuestionnaireTitle;
            obj.QuestionnaireEnglishTitle = item.FLD_QuestionnaireEnglishTitle;

            return obj;
        }

        #endregion


        #region Channal
        public static RPC.Channel ToChannel(RPD.TBL_Channel item)
        {
            RPC.Channel obj = new RPC.Channel();
            obj.Broadcast = item.FLD_Broadcast;
            obj.ID = item.FLD_ID_PK;
            obj.Media = item.FLD_Media_FK;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.Image = item.FLD_Image;
            obj.Province = item.FLD_Province_FK;
            return obj;
        }
        public static RPD.TBL_Channel ToChannel(RPC.Channel item)
        {
            RPD.TBL_Channel obj = new RPD.TBL_Channel();
            obj.FLD_Broadcast = item.Broadcast;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Media_FK = item.Media;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_Image = item.Image;
            obj.FLD_Province_FK = item.Province;
            return obj;


        }


        public static RPD.TBL_Channel ToChannel(RPC.Program item)
        {
            if (item.Channel != null)
            {
                RPD.TBL_Channel obj = new RPD.TBL_Channel();
                obj.FLD_Broadcast = item.ChannelBroadcast;
                obj.FLD_ID_PK = (long)item.Channel;
                obj.FLD_Media_FK = item.Media;
                obj.FLD_Title = item.ChannelName;
                return obj;
            }
            return null;
        }


        #endregion

        #region Class
        public static RPD.TBL_Class ToClass(RPC.Class item)
        {
            RPD.TBL_Class obj = new RPD.TBL_Class();
            obj.FLD_ClassNo = item.ClassNo;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_PPS = item.PPS;
            return obj;
        }
        public static RPC.Class ToClass(RPD.TBL_Class item)
        {
            RPC.Class obj = new RPC.Class();
            obj.ClassNo = item.FLD_ClassNo;
            obj.ID = item.FLD_ID_PK;
            obj.PPS = item.FLD_PPS;
            return obj;
        }

       

        #endregion

        #region Contract
        public static RPD.TBL_Contract ToContract(RPC.Contract item)
        {
            RPD.TBL_Contract obj = new RPD.TBL_Contract();
            obj.FLD_Commnet = item.Comment;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;
            return obj;
        }
        public static RPC.Contract ToContract(RPD.TBL_Contract item)
        {
            RPC.Contract obj = new RPC.Contract();
            obj.Comment = item.FLD_Commnet;
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            return obj;
        }


        #endregion

        #region ContractAccess
        public static RPD.TBL_ContractAccess ToContractAccess(RPC.ContractAccess item)
        {
            RPD.TBL_ContractAccess obj = new RPD.TBL_ContractAccess();
            obj.FLD_Access = item.Access;
            obj.FLD_ClassID_FK = item.Class;
            obj.FLD_ContractID_FK = item.Contract;
            obj.FLD_ID_PK = item.ID;
            return obj;
        }
        public static RPC.ContractAccess ToContractAccess(RPD.TBL_ContractAccess item)
        {
            RPC.ContractAccess obj = new RPC.ContractAccess();
            obj.Access = item.FLD_Access;
            obj.Class = item.FLD_ClassID_FK;
            obj.Contract = item.FLD_ContractID_FK;
            obj.ID = item.FLD_ID_PK;
            return obj;
        }
        #endregion

        #region Company
        public static RPD.TBL_Company ToCompany(RPC.Company item)
        {
            RPD.TBL_Company obj = new RPD.TBL_Company();

            obj.FLD_Group_FK = item.Group;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Image = item.Image;
            obj.FLD_Title = item.Title;
            obj.TBL_Profile = new RPD.TBL_Profile() { FLD_CreateDate = item.CreateDate, FLD_Email = item.Email, FLD_IsEnabled = item.IsEnabled, FLD_Phone = item.Phone };





            return obj;
        }
        public static RPC.Company ToCompany(RPD.VIW_Company item)
        {
            RPC.Company obj = new RPC.Company();
            obj.CreateDate = item.FLD_CreateDate;
            obj.Email = item.FLD_Email;
            obj.Group = item.FLD_Group_FK;
            obj.GroupComment = item.FLD_GroupComment;
            obj.GroupName = item.FLD_GroupName;
            obj.ID = item.FLD_ID_PK;
            obj.Image = item.FLD_Image;
            obj.IsEnabled = item.FLD_IsEnabled;
            obj.Phone = item.FLD_Phone;
            obj.Title = item.FLD_Title;
            return obj;
        }
        public static RPD.TBL_Company ToCompany(RPC.User item)
        {
            RPD.TBL_Company obj = new RPD.TBL_Company();
            obj.FLD_ID_PK = (item.Company == null) ? 0 : (long)item.Company;
            obj.FLD_Title = item.CompanyTitle;

            return obj;
        }
        public static RPD.TBL_Company ToCompany(RPC.Profile item)
        {
            RPD.TBL_Company obj = new RPD.TBL_Company();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;

            return obj;
        }
        #endregion

        #region ContextType
        public static RPD.TBL_ContextType ToContextType(RPC.ContextType item)
        {
            RPD.TBL_ContextType obj = new RPD.TBL_ContextType();

            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;





            return obj;
        }
        public static RPC.ContextType ToContextType(RPD.TBL_ContextType item)
        {
            RPC.ContextType obj = new RPC.ContextType();
           
            obj.ID = item.FLD_ID_PK;
           
            obj.Title = item.FLD_Title;

            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }

        #endregion


        #region Foretaste
        public static RPD.TBL_Foretaste ToForetaste(RPC.Foretaste item)
        {
            RPD.TBL_Foretaste obj = new RPD.TBL_Foretaste();

          
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Image = item.Image;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishImage = item.EnglishImage;
            obj.FLD_EnglishTitle = item.EnglishTitle;





            return obj;
        }
        public static RPC.Foretaste ToForetaste(RPD.TBL_Foretaste item)
        {
            RPC.Foretaste obj = new RPC.Foretaste();           
            obj.ID = item.FLD_ID_PK;
            obj.Image = item.FLD_Image;           
            obj.Title = item.FLD_Title;
            obj.EnglishImage = item.FLD_EnglishImage;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }
      
        #endregion

        
        #region LCategory

        public static RPD.TBL_LCategory ToLCategory(RPC.LCategory item)
        {
            RPD.TBL_LCategory obj = new RPD.TBL_LCategory();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;
            return obj;
        }

        public static RPC.LCategory ToLCategory(RPD.TBL_LCategory item)
        {
            RPC.LCategory obj = new RPC.LCategory();
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            return obj;
        }

        public static RPD.TBL_LCategory ToLCategory(RPC.Lottary item)
        {
            if ((item.Category != null) && (item.Category == 0))
            {
                RPD.TBL_LCategory obj = new RPD.TBL_LCategory();
                obj.FLD_ID_PK = (long)item.Category;
                obj.FLD_Title = item.CategoryTitle;
                return obj;
            }
            return null;
        }
        #endregion

        #region Limit
        public static RPD.TBL_Limit ToLimit(RPC.Limit item)
        {
            RPD.TBL_Limit obj = new RPD.TBL_Limit();
            obj.FLD_Date = item.Date;
            obj.FLD_Day = item.Day;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Program_FK = item.Program;
            return obj;
        }
        public static RPC.Limit ToLimit(RPD.TBL_Limit item)
        {
            RPC.Limit obj = new RPC.Limit();
            obj.Date = item.FLD_Date;
            obj.Day = item.FLD_Day;
            obj.ID = item.FLD_ID_PK;
            obj.Program = item.FLD_Program_FK;
            return obj;
        }
        #endregion

        #region Location
        public static RPD.TBL_Location ToLocation(RPC.Location item)
        {
            RPD.TBL_Location obj = new RPD.TBL_Location();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Type = item.RedundantType;
            obj.FLD_After = item.After;
            obj.FLD_Before = item.Before;
            obj.FLD_Middle = item.Middle;
            return obj;
        }
        public static RPC.Location ToLocation(RPD.TBL_Location item)
        {
            RPC.Location obj = new RPC.Location();
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_Type;
            obj.After = item.FLD_After;
            obj.Before = item.FLD_Before;
            obj.Middle = item.FLD_Middle;
            return obj;
        }
        #endregion

        #region Lottary
        public static RPD.TBL_Lottary ToLottary(RPC.Lottary item)
        {
            RPD.TBL_Lottary obj = new RPD.TBL_Lottary();
            obj.FLD_Category_FK = item.Category;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_Type = item.RedundantType;
            return obj;
        }
        public static RPC.Lottary ToLottary(RPD.TBL_Lottary item)
        {
            RPC.Lottary obj = new RPC.Lottary();
            obj.Category = item.FLD_Category_FK;
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_Type;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }
        public static RPC.Lottary ToLottary(RPD.VIW_Lottary item)
        {
            RPC.Lottary obj = new RPC.Lottary();
            obj.Category = item.FLD_Category_FK;
            obj.CategoryTitle = item.FLD_CategoryTitle;
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_RedundantType;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }

        #endregion

        #region Media
        public static RPD.TBL_Media ToMedia(RPC.Media item)
        {
            RPD.TBL_Media obj = new RPD.TBL_Media();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            return obj;
        }
        public static RPC.Media ToMedia(RPD.TBL_Media item)
        {
            RPC.Media obj = new RPC.Media();
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }
        #endregion


        #region OwnerGroup
        public static RPD.TBL_OwnerGroup ToOwnerGroup(RPC.OwnerGroup item)
        {
            RPD.TBL_OwnerGroup obj = new RPD.TBL_OwnerGroup();
            obj.FLD_Commnet = item.Commnet;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Title = item.Title;

            return obj;
        }
        public static RPC.OwnerGroup ToOwnerGroup(RPD.TBL_OwnerGroup item)
        {
            RPC.OwnerGroup obj = new RPC.OwnerGroup();
            obj.Commnet = item.FLD_Commnet;
            obj.ID = item.FLD_ID_PK;
            obj.Title = item.FLD_Title;
            return obj;
        }
        #endregion

        #region Position
        public static RPD.TBL_Position ToPosition(RPC.Position item)
        {
            RPD.TBL_Position obj = new RPD.TBL_Position();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_Type = item.RedundantType;

            return obj;
        }
        public static RPC.Position ToPosition(RPD.TBL_Position item)
        {
            RPC.Position obj = new RPC.Position();
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.RedundantType = item.FLD_Type;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            return obj;
        }
        #endregion

        #region Profile
        public static RPD.TBL_Profile ToProfile(RPC.Profile item)
        {
            RPD.TBL_Profile obj = new RPD.TBL_Profile();
            obj.FLD_CreateDate = item.CreateDate;
            obj.FLD_Email = item.Email;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_IsEnabled = item.IsEnabled;
            obj.FLD_Phone = item.Phone;
            if (!(String.IsNullOrEmpty(item.Name)))
            {
                obj.TBL_User = ToUser(item);
            }
            if (!(String.IsNullOrEmpty(item.Title)))
            {
                obj.TBL_Company = ToCompany(item);
            }

            return obj;
        }
        public static RPD.TBL_Profile ToProfile(RPC.User item)
        {
            RPD.TBL_Profile obj = new RPD.TBL_Profile();
            obj.FLD_CreateDate = item.CreateDate;
            obj.FLD_Email = item.Email;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_IsEnabled = item.IsEnabled;
            obj.FLD_Phone = item.Phone;
            return obj;
        }
        public static RPC.Profile ToProfile(RPD.VIW_Profile item)
        {
            RPC.Profile obj = new RPC.Profile();
            obj.Access = item.FLD_Access;
            obj.Cell = item.FLD_Cell;
            obj.Company = item.FLD_Company_FK;
            obj.CompanyTitle = item.FLD_CompanyTitle;
            obj.CreateDate = item.FLD_CreateDate;
            obj.DisableDate = item.FLD_DisableDate;
            obj.Email = item.FLD_Email;
            obj.Gender = item.FLD_Gender;
            obj.ID = item.FLD_ID_PK;
            obj.IsEnabled = item.FLD_IsEnabled;
            obj.Name = item.FLD_Name;
            obj.Password = item.FLD_Password;
            obj.Phone = item.FLD_Phone;
            obj.Title = item.FLD_Title;
            obj.Username = item.FLD_Username;

            return obj;
        }

        public static RPD.TBL_Profile ToProfile(RPC.Company item)
        {
            RPD.TBL_Profile obj = new RPD.TBL_Profile();
            obj.FLD_CreateDate = item.CreateDate;
            obj.FLD_Email = item.Email;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_IsEnabled = item.IsEnabled;
            obj.FLD_Phone = item.Phone;


            return obj;
        }

        #endregion

        #region Program
        public static RPD.TBL_Program ToProgram(RPC.Program item)
        {
            RPD.TBL_Program obj = new RPD.TBL_Program();
            obj.FLD_After = item.After;
            obj.FLD_Before = item.Before;
            obj.FLD_Channel_FK = item.Channel;
            obj.FLD_Enable = item.Enable;            
            obj.FLD_EndTime = item.EndTime;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Middle = item.Middle;
            obj.FLD_PlayTime = item.PlayTime;
            obj.FLD_StartTime = item.StartTime;
            obj.FLD_Title = item.Title;

            if (item.WeekRepList != null)
                if (item.WeekRepList.Count > 0)
                    foreach (RPC.WeekRep weekRep in item.WeekRepList)
                    {
                        obj.TBL_WeekRep.Add(Convert.ToWeekRep(weekRep));
                    }

            if (item.LimitList != null)
                if (item.LimitList.Count > 0)
                    foreach (RPC.Limit limit in item.LimitList)
                    {
                        obj.TBL_Limit.Add(Convert.ToLimit(limit));
                    }

            if (item.ProgramAccessList != null)
            {
                if (item.ProgramAccessList.Count > 0)
                    foreach (RPC.ProgramAccess pa in item.ProgramAccessList)
                    {
                        obj.TBL_ProgramAccess.Add(Convert.ToProgramAccess(pa));
                    }
            }



            return obj;
        }

        public static RPC.Program ToProgram(RPD.VIW_Program item)
        {
            if (item == null)
                return null;
            RPC.Program obj = new RPC.Program();
            obj.After = item.FLD_After;
            obj.Before = item.FLD_Before;
            obj.Channel = item.FLD_Channel_FK;
            obj.ChannelBroadcast = item.FLD_ChannelBroadcast;
            obj.ChannelMedia = item.FLD_ChannelMedia;
            obj.ChannelName = item.FLD_ChannelName;
            obj.Enable = item.FLD_Enable;
            obj.EndTime = item.FLD_EndTime;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.ID = item.FLD_ID_PK;
            obj.Media = item.FLD_Media_FK;
            obj.Middle = item.FLD_Middle;
            obj.PlayTime = item.FLD_PlayTime;
         
            obj.StartTime = item.FLD_StartTime;
            obj.Title = item.FLD_Title;
         
            return obj;
        }

        public static RPC.Program ToProgram(RPD.VIW_Question item)
        {
            RPC.Program obj = new RPC.Program();
            obj.Channel= item.FLD_Channel_FK;
            obj.ChannelBroadcast = item.FLD_ChannelBroadcast;
            obj.ChannelMedia = item.FLD_ChannelName;
            obj.ChannelName = item.FLD_ChannelName;
            obj.Enable = item.FLD_Enable;
            obj.EndTime = item.FLD_EndTime;
          
            obj.ID = (long)((item.FLD_Program_FK == null) ? 0 : item.FLD_Program_FK);
            obj.StartTime = item.FLD_StartTime;
            obj.Title = item.FLD_ProgramTitle;
            obj.PlayTime = item.FLD_PlayTime;

            return obj;
        }

        #endregion

        #region ProgramAccess
        public static RPD.TBL_ProgramAccess ToProgramAccess(RPC.ProgramAccess item)
        {
            RPD.TBL_ProgramAccess obj = new RPD.TBL_ProgramAccess();
            obj.FLD_Class_FK = item.Class;
            obj.FLD_Extra = item.Extra;
            obj.FLD_ExtraType = item.ExtraType;
            obj.FLD_Group_FK = item.Group;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Program_FK = item.Program;
            obj.FLD_Series_FK = item.Series;
            obj.FLD_Location_FK= item.Location;
            obj.FLD_Month = item.Month;




            return obj;
        }

        public static RPC.ProgramAccess ToProgramAccess(RPD.VIW_ProgramAccess item)
        {
            RPC.ProgramAccess obj = new RPC.ProgramAccess();
            obj.Class = item.FLD_Class_FK;
            obj.ClassNo = item.FLD_ClassNo;
            obj.Extra = item.FLD_Extra;
            obj.ExtraType = item.FLD_ExtraType;
            obj.Group = item.FLD_Group_FK;
            obj.GroupName = item.FLD_Group;
            obj.ID = item.FLD_ID_PK;
            obj.Media = item.FLD_Media_FK;
            obj.PPS = item.FLD_PPS;
            obj.Program = item.FLD_Program_FK;
            obj.ProgramTitle = item.FLD_Program;
            obj.Series = item.FLD_Series_FK;
            obj.Location = item.FLD_Location_FK;
            obj.Month = item.FLD_Month;
            return obj;
        }



        #endregion


        #region ProgramSeries
        public static RPD.TBL_ProgramSeries ToProgramSeries(RPC.ProgramSeries item)
        {
            RPD.TBL_ProgramSeries obj = new RPD.TBL_ProgramSeries();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Title = item.Title;
            obj.FLD_Type = item.Type;
        
            return obj;
        }

        public static RPC.ProgramSeries ToProgramSeries(RPD.TBL_ProgramSeries item)
        {
            RPC.ProgramSeries obj = new RPC.ProgramSeries();
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.Title = item.FLD_Title;
            obj.Type = item.FLD_Type;
           

            return obj;
        }



        #endregion


        #region Province
        public static RPD.TBL_Province ToProvince(RPC.Province item)
        {
            RPD.TBL_Province obj = new RPD.TBL_Province();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Redundant = item.Redundant;
            obj.FLD_Title = item.Title;
            obj.FLD_Type = item.Type;

            return obj;
        }

        public static RPC.Province ToProvince(RPD.TBL_Province item)
        {
            RPC.Province obj = new RPC.Province();
            obj.ID = item.FLD_ID_PK;
            obj.Redundant = item.FLD_Redundant;
            obj.Title = item.FLD_Title;
            obj.Type = item.FLD_Type;


            return obj;
        }



        #endregion


        #region Questionnaire
        public static RPD.TBL_Questionnaire ToQuestionnaire(RPC.Questionnaire item)
        {
            RPD.TBL_Questionnaire obj = new RPD.TBL_Questionnaire();
            obj.FLD_ID_PK = item.ID;
            obj.FLD_IsPublish = item.IsPublish;
            obj.FLD_QNo = item.QNo;
            obj.FLD_Title = item.Title;
            obj.FLD_EnglishTitle = item.EnglishTitle;
            obj.FLD_CreatDate = item.CreatDate;

            if (item.QuestionList != null)
                if (item.QuestionList.Count > 0)
                {
                    foreach (RPC.Question question in item.QuestionList)
                    {
                        obj.TBL_Question.Add(Convert.ToQuestion(question));
                    }
                }

            return obj;
        }
        public static RPC.Questionnaire ToQuestionnaire(RPD.TBL_Questionnaire item)
        {
            RPC.Questionnaire obj = new RPC.Questionnaire();
            obj.ID = item.FLD_ID_PK;
            obj.IsPublish = item.FLD_IsPublish;
            obj.QNo = item.FLD_QNo;
            obj.Title = item.FLD_Title;
            obj.EnglishTitle = item.FLD_EnglishTitle;
            obj.CreatDate = item.FLD_CreatDate;
            return obj;
        }
        #endregion

        #region Question
        public static RPD.TBL_Question ToQuestion(RPC.Question item)
        {
            RPD.TBL_Question obj = new RPD.TBL_Question();
            obj.FLD_Program_FK = item.Program;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Questionnaire_FK = item.Questionnaire;

            return obj;
        }
        public static RPC.Question ToQuestion(RPD.TBL_Question item)
        {
            RPC.Question obj = new RPC.Question();
            obj.Program = item.FLD_Program_FK;
            obj.ID = item.FLD_ID_PK;
            obj.Questionnaire = item.FLD_Questionnaire_FK;

            return obj;
        }


        public static RPC.Question ToQuestion(RPD.VIW_Question item)
        {
            RPC.Question obj = new RPC.Question();
            obj.Program = item.FLD_Program_FK;
            obj.Questionnaire = item.FLD_Questionnaire_FK;
            return obj;
        }


        #endregion

        #region Respondent
        public static RPD.TBL_Respondent ToRespondent(RPC.Respondent item)
        {
            RPD.TBL_Respondent obj = new RPD.TBL_Respondent();

            obj.FLD_BestAdvertising = item.BestAdvertising;
            obj.FLD_BestAdvertising2 = item.BestAdvertising2;
            obj.FLD_BestClass = item.BestClass;
            obj.FLD_BestClass2 = item.BestClass2;
            obj.FLD_BestCompany = item.BestCompany;
            obj.FLD_BestCompany2 = item.BestCompany2;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Inquirer_FK = item.Inquirer;
            obj.FLD_IsPublish = item.IsPublish;
            obj.FLD_Questionnaire_FK = item.Questionnaire;
            obj.FLD_Responder_FK = item.Responder;
            obj.FLD_WatchTV = item.WatchTV;
            obj.FLD_WorstAdvertising = item.WorstAdvertising;
            obj.FLD_WorstAdvertising2 = item.WorstAdvertising2;
            obj.FLD_WorstClass = item.WorstClass;
            obj.FLD_WorstClass2 = item.WorstClass2;
            obj.FLD_WorstCompany = item.WorstCompany;
            obj.FLD_WorstCompany2 = item.WorstCompany2;
            obj.FLD_CreateDate = item.CreateDate;
            

            if (item.AnswerList != null)
                if (item.AnswerList.Count > 0)
                {
                    foreach (RPC.Answer answer in item.AnswerList)
                    {
                        obj.TBL_Answer.Add(Convert.ToAnswer(answer));
                    }
                }

            if (item.ResponderProfile != null)
                obj.TBL_Responder = ToResponder(item.ResponderProfile);
            //if (item.InquirerProfile != null)
                //obj.TBL_User = ToUser(item.InquirerProfile);

            return obj;
        }
        public static RPC.Respondent ToRespondent(RPD.VIW_Respondent item)
        {
            RPC.Respondent obj = new RPC.Respondent();
            obj.BestAdvertising = item.FLD_BestAdvertising;
            obj.BestAdvertising2 = item.FLD_BestAdvertising2;
            obj.BestClass = item.FLD_BestClass;
            obj.BestClass2 = item.FLD_BestClass2;
            obj.BestCompany = item.FLD_BestCompany;
            obj.BestCompany2 = item.FLD_BestCompany2;
            obj.ID = item.FLD_ID_PK;
            obj.Inquirer = item.FLD_Inquirer_FK;
            obj.IsPublish = item.FLD_IsPublish;
            obj.QNo = item.FLD_QNo;
            obj.QTitle = item.FLD_QTitle;
            obj.Questionnaire = item.FLD_Questionnaire_FK;
            obj.Responder = item.FLD_Responder_FK;
            obj.ResponderAges = item.FLD_ResponderAges;
            obj.ResponderCooperation = item.FLD_ResponderCooperation;
            obj.ResponderEducation = item.FLD_ResponderEducation;
            obj.ResponderFamilyIncome = item.FLD_ResponderFamilyIncome;
            obj.ResponderGender = item.FLD_ResponderGender;
            obj.ResponderName = item.FLD_ResponderName;
            obj.ResponderOccupation = item.FLD_ResponderOccupation;
            obj.ResponderMaritalStatus = item.FLD_ResponderMaritalStatus;

            obj.WatchTV = item.FLD_WatchTV;
            obj.WorstAdvertising = item.FLD_WorstAdvertising;
            obj.WorstAdvertising2 = item.FLD_WorstAdvertising2;
            obj.WorstClass = item.FLD_WorstClass;
            obj.WorstClass2 = item.FLD_WorstClass2;
            obj.WorstCompany = item.FLD_WorstCompany;
            obj.WorstCompany2 = item.FLD_WorstCompany2;
            obj.ResponderCity=item.FLD_ResponderCity;
            return obj;
        }

        #endregion

        #region Responder

        public static RPD.TBL_Responder ToResponder(RPC.Responder item)
        {
            RPD.TBL_Responder obj = new RPD.TBL_Responder();

            obj.FLD_Ages = item.Ages;
            obj.FLD_Cooperation = item.Cooperation;
            obj.FLD_Education = item.Education;
            obj.FLD_FamilyIncome = item.FamilyIncome;
            obj.FLD_Gender = item.Gender;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Name = item.Name;
            obj.FLD_Occupation = item.Occupation;
            obj.FLD_MaritalStatus = item.MaritalStatus;
            obj.FLD_City_FK = item.City;

            return obj;
        }
        public static RPC.Responder ToResponder(RPD.TBL_Responder item)
        {
            RPC.Responder obj = new RPC.Responder();
            obj.Ages = item.FLD_Ages;
            obj.Cooperation = item.FLD_Cooperation;
            obj.Education = item.FLD_Education;
            obj.FamilyIncome = item.FLD_FamilyIncome;
            obj.Gender = item.FLD_Gender;
            obj.ID = item.FLD_ID_PK;
            obj.Name = item.FLD_Name;
            obj.Occupation = item.FLD_Occupation;
            obj.MaritalStatus = item.FLD_MaritalStatus;
            obj.City = item.FLD_City_FK;
            return obj;
        }
        #endregion

        #region User

        public static RPD.TBL_User ToUser(RPC.User item)
        {
            RPD.TBL_User obj = new RPD.TBL_User();
            obj.FLD_Access = item.Access;
            obj.FLD_Cell = item.Cell;
            obj.FLD_Company_FK = (long?)((item.Company == 0) ? null : item.Company);
            obj.FLD_DisableDate = item.DisableDate;
            obj.FLD_Gender = item.Gender;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Name = item.Name;
            obj.FLD_Password = item.Password;
            obj.FLD_Username = item.Username;
            obj.FLD_FirstVisit = item.FirstVisit;
            obj.FLD_LastVisit = item.LastVisit;

            obj.TBL_Profile = ToProfile(item);

            if ((item.Company == null) && (item.CompanyTitle != null))
                if (item.CompanyTitle.Trim().Length > 0)
                    obj.TBL_Company = ToCompany(item);


            foreach (RPC.CampaignAccess Access in item.CampaignAccessList)
                obj.TBL_CampaignAccess.Add(Convert.ToCampaignAccess(Access));
            return obj;

        }
        public static RPC.User ToUser(RPD.TBL_User item)
        {
            RPC.User obj = new RPC.User();
            obj.Access = item.FLD_Access;
            obj.Cell = item.FLD_Cell;
            obj.Company = item.FLD_Company_FK;
            obj.DisableDate = item.FLD_DisableDate;
            obj.Gender = item.FLD_Gender;
            obj.ID = item.FLD_ID_PK;
            obj.Name = item.FLD_Name;
            obj.Password = item.FLD_Password;
            obj.Username = item.FLD_Username;
            obj.LastVisit = item.FLD_LastVisit;
            obj.FirstVisit = item.FLD_FirstVisit;

            return obj;
        }
        public static RPC.User ToUser(RPD.VIW_User item)
        {
            RPC.User obj = new RPC.User();
            obj.Access = item.FLD_Access;
            obj.Cell = item.FLD_Cell;
            obj.Company = item.FLD_Company_FK;
            obj.CompanyTitle = item.FLD_Title;
            obj.CreateDate = item.FLD_CreateDate;
            obj.DisableDate = item.FLD_DisableDate;
            obj.Email = item.FLD_Email;
            obj.Gender = item.FLD_Gender;
            obj.ID = item.FLD_ID_PK;
            obj.IsEnabled = item.FLD_IsEnabled;
            obj.Name = item.FLD_Name;
            obj.Password = item.FLD_Password;
            obj.Phone = item.FLD_Phone;
            obj.Username = item.FLD_Username;
            obj.Password = item.FLD_Password;
            obj.LastVisit = item.FLD_LastVisit;
            obj.FirstVisit = item.FLD_FirstVisit;

            return obj;
        }
        public static RPD.TBL_User ToUser(RPC.Profile item)
        {
            RPD.TBL_User obj = new RPD.TBL_User();
            obj.FLD_Access = item.Access;
            obj.FLD_Cell = item.Cell;
            obj.FLD_Company_FK = item.Company;
            obj.FLD_DisableDate = item.DisableDate;
            obj.FLD_Gender = item.Gender;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Name = item.Name;
            obj.FLD_Password = item.Password;
            obj.FLD_Username = item.Username;

            return obj;
        }
        #endregion

        #region WeekRep
        public static RPD.TBL_WeekRep ToWeekRep(RPC.WeekRep item)
        {
            RPD.TBL_WeekRep obj = new RPD.TBL_WeekRep();
            obj.FLD_Day = item.Day;
            obj.FLD_ID_PK = item.ID;
            obj.FLD_Program_FK = item.Program;
            return obj;
        }
        public static RPC.WeekRep ToWeekRep(RPD.TBL_WeekRep item)
        {
            RPC.WeekRep obj = new RPC.WeekRep();
            obj.Day = item.FLD_Day;
            obj.ID = item.FLD_ID_PK;
            obj.Program = item.FLD_Program_FK;
            return obj;
        }
        #endregion
    }
}
