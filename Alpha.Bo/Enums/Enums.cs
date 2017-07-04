using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Enums
{
    public class Enums
    {
        public enum Gender
        {
            None = -1, Male = 0, Female = 1
        }
        public enum YesNo { Yes, No }
        public enum MaritalStatus
        {
            Not_selected = -1,
            Single = 0,
            In_Love = 1,
            Engaged = 2,
            Married = 3,
            Its_complicated = 4,
            Divorced = 5,
        }
        public enum Months
        {
            Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Des
        }

        public enum Countries
        {
            Not_Selected = -1,
            Afghanistan,
            Albania,
            Algeria,
            Andorra,
            Angola,
            Antigua_and_Barbuda,
            Argentina,
            Armenia,
            Australia,
            Austria,
            Azerbaijan,
            Bahamas,
            Bahrain,
            Bangladesh,
            Barbados,
            Belarus,
            Belgium,
            Belize,
            Benin,
            Bhutan,
            Bolivia,
            Bosnia_and_Herzegovina,
            Botswana,
            Brazil,
            Brunei,
            Bulgaria,
            Burkina_Faso,
            Burundi,
            Cabo_Verde,
            Cambodia,
            Cameroon,
            Canada,
            Central_African_Republic,
            Chad,
            Chile,
            China,
            Colombia,
            Comoros,
            Democratic_Republic_of_the_Congo,
            Republic_of_the_Congo,
            Costa_Rica,
            Cote_d_Ivoire,
            Croatia,
            Cuba,
            Cyprus,
            Czech_Republic,
            Denmark,
            Djibouti,
            Dominica,
            Dominican_Republic,
            East_Timor,
            Ecuador,
            Egypt,
            El_Salvador,
            Equatorial_Guinea,
            Eritrea,
            Estonia,
            Ethiopia,
            Fiji,
            Finland,
            France,
            Gabon,
            Gambia,
            Georgia,
            Germany,
            Ghana,
            Greece,
            Grenada,
            Guatemala,
            Guinea,
            Guinea_Bissau,
            Guyana,
            Haiti,
            Honduras,
            Hungary,
            Iceland,
            India,
            Indonesia,
            Iran,
            Iraq,
            Ireland,
            Israel,
            Italy,
            Jamaica,
            Japan,
            Jordan,
            Kazakhstan,
            Kenya,
            Kiribati,
            North_Korea,
            South_Korea,
            Kuwait,
            Kyrgyzstan,
            Laos,
            Latvia,
            Lebanon,
            Lesotho,
            Liberia,
            Libya,
            Liechtenstein,
            Lithuania,
            Luxembourg,
            Macedonia,
            Madagascar,
            Malawi,
            Malaysia,
            Maldives,
            Mali,
            Malta,
            Marshall_Islands,
            Mauritania,
            Mauritius,
            Mexico,
            Micronesia,
            Moldova,
            Monaco,
            Mongolia,
            Montenegro,
            Morocco,
            Mozambique,
            Myanmar,
            Namibia,
            Nauru,
            Nepal,
            Netherlands,
            New_Zealand,
            Nicaragua,
            Nigeria,
            Northern_Ireland,
            Norway,
            Oman,
            Pakistan,
            Palau,
            Palestinian_State,
            Panama,
            Papua_New_Guinea,
            Paraguay,
            Peru,
            Philippines,
            Poland,
            Portugal,
            Qatar,
            Romania,
            Russia,
            Rwanda,
            Samoa,
            San_Marino,
            Sao_Tome_and_Principe,
            Saudi_Arabia,
            Senegal,
            Serbia,
            Seychelles,
            Sierra_Leone,
            Singapore,
            Slovakia,
            Slovenia,
            Solomon_Islands,
            Somalia,
            South_Africa,
            Spain,
            Sri_Lanka,
            St_Kitts_and_Nevis,
            St_Lucia,
            St_Vincent_and_the_Grenadines,
            Sudan,
            Suriname,
            Swaziland,
            Sweden,
            Switzerland,
            Syria,
            Taiwan,
            Tajikistan,
            Tanzania,
            Thailand,
            Togo,
            Tonga,
            Trinidad_and_Tobago,
            Tunisia,
            Turkey,
            Turkmenistan,
            Tuvalu,
            Uganda,
            Ukraine,
            United_Arab_Emirates,
            United_Kingdom,
            United_States,
            Uruguay,
            Uzbekistan,
            Vanuatu,
            Vatican_City,
            Venezuela,
            Vietnam,
            Western_Sahara,
            Yemen,
            Zaire,
            Zambia,
            Zimbabwe,
        }

        public enum Languages
        {
            English, Sinhala, Tamil
        }

        public enum TokenType { ForgetPassword, EmailValidate }

        public enum SocialNetworks
        {
            Not_Selected = -1,
            Facebook = 0, Twitter = 1, Linkedin = 2, Youtube = 3, Google_Plus = 4, Others = 5, Phone_Number = 6,
            Gmail = 7, Ymail = 8, Hotmail = 9, Yandex = 10, Skype = 11, My_Site = 12, Blogger = 13, Github = 14,
            Instergram = 15, Pinster = 16
        }

        public enum UserRelationshipStatus
        {
            No = -1, Following, Follower, Block
        }
        public enum PostType
        {
            Question, Poll, Comment, All
        }
        public enum Imagetype
        {
            postimages, profileimages
        }
        public enum PostSearchType
        {
            Ask, Question, Feed
        }
        public enum UserPreferencesInfo
        {
            SendNotificationEmail, ShowAnonymas, ShowMyAsk, ShowMyContacts,
            ShowMyAnswers
        }

        public enum PostLikeType {
            Like,Dislike
        }

        public enum NotificationType {
            CommentToYourPost,
            LikeToYourPost,
            NeedReplay,
            LikeToYourComment
        }
    }
}
