// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="avaver@avaver.com">
//   Andrew Vaverchak
// </copyright>
// <summary>
//   Represents user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VKNet.Api.Objects
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Sex enumeration.
    /// </summary>
    public enum Sex
    {
        Female = 1,
        Male = 2,
        Unknown = 0
    }

    /// <summary>
    /// Represents user.
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "user")]
    public class User : IEquatable<User>, IComparable<User>
    {
        /// <summary>
        /// User identificator.
        /// </summary>
        [XmlElement("uid")]
        public int Id;

        /// <summary>
        /// First name.
        /// </summary>
        [XmlElement("first_name")]
        public string FirstName;

        /// <summary>
        /// Last name.
        /// </summary>
        [XmlElement("last_name")]
        public string LastName;

        /// <summary>
        /// User nickname.
        /// </summary>
        [XmlElement("nickname")]
        public string Nickname;

        /// <summary>
        /// Short page address (only last part).
        /// </summary>
        [XmlElement("domain")]
        public string ShortUrl;

        /// <summary>
        /// User's sex. 0 - unknown, 1 - female, 2 - male.
        /// </summary>
        [XmlElement("sex")]
        public byte RawSex;

        /// <summary>
        /// Gets user's sex.
        /// </summary>
        [XmlIgnore]
        public Sex Sex
        {
            get { return (Sex)this.RawSex; }
        }

        /// <summary>
        /// Indicates if user is online. 0 - offline, 1 - online.
        /// </summary>
        [XmlElement("online")]
        public byte RawOnline;

        /// <summary>
        /// Gets a value indicating whether user is online.
        /// </summary>
        [XmlIgnore]
        public bool Online
        {
            get { return this.RawOnline == 1; }
        }

        /// <summary>
        /// Birth date in following format: "23.11.1981" or "21.9" (if the year is hidden)
        /// </summary>
        [XmlElement("bdate")]
        public string RawBirthDate;

        /// <summary>
        /// Gets birth date. If the year is hidden, it is set to 1900.
        /// </summary>
        [XmlIgnore]
        public DateTime BirthDate
        {
            get
            {
                if (string.IsNullOrEmpty(this.RawBirthDate)) return DateTime.MinValue;
                
                var dateParts = this.RawBirthDate.Split('.');
                if (dateParts.Length < 2) return DateTime.MinValue;
                
                return new DateTime(
                    dateParts.Length > 2 ? int.Parse(dateParts[2]) : 1900,
                    int.Parse(dateParts[1]),
                    int.Parse(dateParts[0]));
            }
        }

        /// <summary>
        /// City name.
        /// </summary>
        [XmlElement("city")]
        public int CityId;

        /// <summary>
        /// Country name.
        /// </summary>
        [XmlElement("country")]
        public int CountryId;

        /// <summary>
        /// The timezone.
        /// </summary>
        [XmlElement("timezone")]
        public byte Timezone;

        /// <summary>
        /// Url to the profile image file (width 50px).
        /// </summary>
        [XmlElement("photo")]
        public string Photo50Url;

        /// <summary>
        /// Url to the profile image file (width 100px).
        /// </summary>
        [XmlElement("photo_medium")]
        public string Photo100Url;

        /// <summary>
        /// Url to the rectangle profile image file (width 100px, height 100px).
        /// </summary>
        [XmlElement("photo_medium_rec")]
        public string Photo100X100Url;

        /// <summary>
        /// Url to the profile image file (width 200px).
        /// </summary>
        [XmlElement("photo_big")]
        public string Photo200Url;

        /// <summary>
        /// Url to the rectangle profile image file (width 50px, height 50px).
        /// </summary>
        [XmlElement("photo_rec")]
        public string Photo50X50Url;

        /// <summary>
        /// User's rate.
        /// </summary>
        [XmlElement("rate")]
        public string Rate;

        /// <summary>
        /// Home phone number.
        /// </summary>
        [XmlElement("home_phone")]
        public string HomePhone;

        /// <summary>
        /// Indicates if user has mobile. 1 - true, 0 - false.
        /// </summary>
        [XmlElement("has_mobile")]
        public byte RawHasMobile;

        /// <summary>
        /// Gets a value indicating whether user specified mobile phone.
        /// </summary>
        [XmlIgnore]
        public bool HasMobile
        {
            get { return this.RawHasMobile == 1; }
        }

        /// <summary>
        /// Mobile phone number.
        /// </summary>
        [XmlElement("mobile_phone")]
        public string MobilePhone;

        /// <summary>
        /// User's university.
        /// </summary>
        [XmlElement("university")]
        public int UniversityId;

        /// <summary>
        /// User's university name.
        /// </summary>
        [XmlElement("university_name")]
        public string UniversityName;

        /// <summary>
        /// User's faculty.
        /// </summary>
        [XmlElement("faculty")]
        public int FacultyId;

        /// <summary>
        /// User's faculty name.
        /// </summary>
        [XmlElement("faculty_name")]
        public string FacultyName;

        /// <summary>
        /// User's graduation year.
        /// </summary>
        [XmlElement("graduation")]
        public int GraduationYear;

        /// <summary>
        /// Indicates if currently authenticated user can post on the user's wall. 0 - no, 1 - yes.
        /// </summary>
        [XmlElement("can_post")]
        public byte RawCanPost;

        /// <summary>
        /// Gets a value indicating whether currently authenticated user can post on the user's wall..
        /// </summary>
        [XmlIgnore]
        public bool CanPost
        {
            get { return this.RawCanPost == 1; }
        }

        /// <summary>
        /// Indicates if currently authenticated user can write private messages to the user. 0 - no, 1 - yes.
        /// </summary>
        [XmlElement("can_write_private_message")]
        public byte RawCanWriteMessages;

        /// <summary>
        /// Gets a value indicating whether currently authenticated user can write private messages to the user.
        /// </summary>
        [XmlIgnore]
        public bool CanWriteMessages
        {
            get { return this.RawCanWriteMessages == 1; }
        }

        /// <summary>
        /// User counters.
        /// </summary>
        [XmlElement("counters")]
        public Counters Counters;

        #region IEquatable<User> Members

        /// <summary>
        /// Checks users for equality.
        /// </summary>
        /// <param name="other">User object to compare with.</param>
        /// <returns>True if users are equal, otherwise false.</returns>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == this.Id;
        }

        /// <summary>
        /// Checks users for equality.
        /// </summary>
        /// <param name="obj">User object to compare with.</param>
        /// <returns>True if users are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(User)) return false;
            return this.Equals((User)obj);
        }

        #endregion

        #region IComparable<User> Members

        /// <summary>
        /// Compares two user objects using message ids.
        /// </summary>
        /// <param name="other">User object to compare with.</param>
        /// <returns>Comparison result.</returns>
        public int CompareTo(User other)
        {
            const string format = "{0} {1}";
            return string.Format(format, this.FirstName, this.LastName).CompareTo(string.Format(format, other.FirstName, other.LastName));
        }

        #endregion

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.Id;
                result = (result * 397) ^ (this.FirstName != null ? this.FirstName.GetHashCode() : 0);
                result = (result * 397) ^ (this.LastName != null ? this.LastName.GetHashCode() : 0);
                result = (result * 397) ^ (this.Nickname != null ? this.Nickname.GetHashCode() : 0);
                result = (result * 397) ^ (this.ShortUrl != null ? this.ShortUrl.GetHashCode() : 0);
                result = (result * 397) ^ this.RawSex.GetHashCode();
                result = (result * 397) ^ this.RawOnline.GetHashCode();
                result = (result * 397) ^ (this.RawBirthDate != null ? this.RawBirthDate.GetHashCode() : 0);
                result = (result * 397) ^ this.CityId;
                result = (result * 397) ^ this.CountryId;
                result = (result * 397) ^ this.Timezone.GetHashCode();
                result = (result * 397) ^ (this.Photo50Url != null ? this.Photo50Url.GetHashCode() : 0);
                result = (result * 397) ^ (this.Photo100Url != null ? this.Photo100Url.GetHashCode() : 0);
                result = (result * 397) ^ (this.Photo100X100Url != null ? this.Photo100X100Url.GetHashCode() : 0);
                result = (result * 397) ^ (this.Photo200Url != null ? this.Photo200Url.GetHashCode() : 0);
                result = (result * 397) ^ (this.Photo50X50Url != null ? this.Photo50X50Url.GetHashCode() : 0);
                result = (result * 397) ^ (this.Rate != null ? this.Rate.GetHashCode() : 0);
                result = (result * 397) ^ (this.HomePhone != null ? this.HomePhone.GetHashCode() : 0);
                result = (result * 397) ^ this.RawHasMobile.GetHashCode();
                result = (result * 397) ^ (this.MobilePhone != null ? this.MobilePhone.GetHashCode() : 0);
                result = (result * 397) ^ this.UniversityId;
                result = (result * 397) ^ (this.UniversityName != null ? this.UniversityName.GetHashCode() : 0);
                result = (result * 397) ^ this.FacultyId;
                result = (result * 397) ^ (this.FacultyName != null ? this.FacultyName.GetHashCode() : 0);
                result = (result * 397) ^ this.GraduationYear;
                result = (result * 397) ^ this.RawCanPost.GetHashCode();
                result = (result * 397) ^ this.RawCanWriteMessages.GetHashCode();
                result = (result * 397) ^ (this.Counters != null ? this.Counters.GetHashCode() : 0);
                return result;
            }
        }
    }
}