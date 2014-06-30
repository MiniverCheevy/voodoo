using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Voodoo
{
    public static class StringExtensions
    {
        public static string Repeat(this string source, int maxLength)
        {
            var sb = new StringBuilder();

            var loopCount = maxLength / source.Length;

            if (maxLength % source.Length > 0)
                loopCount++;

            for (var i = 0; i < loopCount; i++)
                sb.Append(source);

            return sb.ToString(0, maxLength);
        }

        public static string SubStringAfterLastIndexOf(this string source, string value)
        {
            var lastIndexOfValue = source.LastIndexOf(value);

            if (lastIndexOfValue == -1)
                return source;

            var substring = source.Substring(lastIndexOfValue);

            return substring.Length > 1 ? substring.Substring(1) : substring;
        }

        public static string RemoveAllAfterLastIndexOf(this string source, string value)
        {
            var lastIndexOfValue = source.LastIndexOf(value);

            if (lastIndexOfValue == -1)
                return source;

            return source.Remove(lastIndexOfValue);
        }


        public static string RawPhoneNumber(this string phoneNumber)
        {
            if (!String.IsNullOrEmpty(phoneNumber))
            {
                return phoneNumber.Replace(" ", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("-", "")
                    .Replace(".", "");
            }
            return phoneNumber;
        }

        public static string FormattedPhoneNumber(this string phoneNumber)
        {
            var rawPhoneNumber = phoneNumber.RawPhoneNumber();
            if (phoneNumber != null && !String.IsNullOrEmpty(rawPhoneNumber) && rawPhoneNumber.Length == 10)
                return "(" + rawPhoneNumber.Substring(0, 3) + ") " + rawPhoneNumber.Substring(3, 3) + "-" + rawPhoneNumber.Substring(6);

            return rawPhoneNumber;
        }

        public static string RawSSN(this string ssn)
        {
            if (!string.IsNullOrEmpty(ssn))
                ssn = ssn.Replace("-", "");
            
            return ssn;
        }

        public static string FormattedSSN(this string ssn)
        {
            var rawSSN = ssn.RawSSN();
            if (ssn != null && !String.IsNullOrEmpty(rawSSN) && rawSSN.Length == 9)
                return rawSSN.Substring(0, 3) + "-" + rawSSN.Substring(3, 2) + "-" + rawSSN.Substring(5);

            return rawSSN;
        }

        public static string SuperTrim(this string s)
        {
            return s != null ? s.Trim() : null;
        }

        public static string TrimNull(this string s)
        {
            return s != null ? s.Trim() : string.Empty;
        }

        //public static string Encrypt(this string @string, string password)
        //{
        //    @string = Encryptor.Encrypt(@string, password);

        //    return @string;
        //}

        //public static string Decrypt(this string @string, string password)
        //{
        //    @string = Decryptor.Decrypt(@string, password);

        //    return @string;
        //}

        public static bool SuperContains(this String source, string values, string delimiter)
        {
            var pvalues = values.Split(delimiter.ToCharArray());

            return pvalues.Any(value => source.Trim().ToLower().Equals(value.ToLower()));
        }

        public static string NumberWithinString(this string source)
        {
            var regex = new Regex(@"\d+");        
            
            return regex.Match(source).Value; 
        }

        /// <summary>
        /// Flattens a list of strings into a single string, which each child string separated by carriage returns
        /// </summary>
        /// <param name="strings"></param>
        /// <returns>Single string containing each child string separated by a carriage return</returns>
        public static string FlattenIntoSingleString(this IList<string> strings)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var childString in strings)
            {
                sb.AppendLine(childString);
            }

            return sb.ToString();
        }        
    }
}