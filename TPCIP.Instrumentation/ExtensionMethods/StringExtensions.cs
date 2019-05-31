using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TPCIP.CommonDomain.Enums;

namespace TPCIP.Instrumentation.ExtensionMethods
{
    public static class StringExtensions
    {
        public const long minNABS = 400000000;
        public const long maxNABS = 499999999;
        public const long minCU = 200000000;
        public const long maxCU = 349999999;
        public const long minKPM = 120000000000;
        public const long maxKPM = 129999999999;
        public const long minYOUSEE = 600000000;
        public const long maxYOUSEE = 699999999;
        public const long minWHOLESALE = 190000000;
        public const long maxWHOLESALE = 199999999;
        public const long minOTHER_RAS = 350000000;
        public const long maxOTHER_RAS = 399999999;
        public const long minSERVICE_PROVIDER = 100000000;
        public const long maxSERVICE_PROVIDER = 199999999;
        public const long minESYS = 500000000;
        public const long maxESYS = 549999999;
        public const long minMANUAL_BILLING = 800000000;
        public const long maxMANUAL_BILLING = 899999999;
        public const long minINTERNAL = 900000000;
        public const long maxINTERNAL = 999999999;

        public static bool Contains(this string source, string value, CompareOptions compareOptions)
        {
            if (source == null)
            {
                return false;
            }
            var compareInfo = CultureInfo.CurrentCulture.CompareInfo;
            return compareInfo.IndexOf(source, value, compareOptions) >= 0;
        }

        public static bool EqualsIgnoreCase(this string source, string compareTo)
        {
            if (source == null || compareTo == null) return false;
            return string.Equals(source.Trim(), compareTo.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Check if Customer starts with 6 & 9-digit or it starts with Y
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static bool IsYouSeeCustomer(this string customerId)
        {
            return (customerId.StartsWith("6") && customerId.Length == 9) || customerId.StartsWith("Y");
        }

        public static bool IsWithin(this long value, long minimum, long maximum)
        {

            return value >= minimum && value <= maximum;
        }

        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static CustomerTypeName GetCustomerType(long parentAccountNo)
        {
            long number = parentAccountNo;
            var digit = number.ToString().Length;
            if (parentAccountNo != 0)
            {
                if (number.IsWithin(minCU, maxCU))
                {
                    return CustomerTypeName.CU;
                }
                else if (number.IsWithin(minYOUSEE, maxYOUSEE))
                {
                    return CustomerTypeName.YOUSEE;
                }
                else if (digit == 6 || digit == 7)
                {
                    return CustomerTypeName.Mbilling;
                }
                else if (number.IsWithin(minNABS, maxNABS))
                {
                    return CustomerTypeName.NABS;
                }
                //else if (number.CheckRange(minSERVICE_PROVIDER, maxSERVICE_PROVIDER))
                //{
                //    return CustomerTypeName.SERVICE_PROVIDER;
                //}
                //else if (number.CheckRange(minESYS , maxESYS ))
                //{
                //    return CustomerTypeName.ESYS;
                //}
                //else if (number.CheckRange(minMANUAL_BILLING, maxMANUAL_BILLING))
                //{
                //    return CustomerTypeName.MANUAL_BILLING;
                //}
                //else if (number.CheckRange(minINTERNAL , maxINTERNAL ))
                //{
                //    return CustomerTypeName.INTERNAL;
                //}
                else if (number.IsWithin(minWHOLESALE, maxWHOLESALE))
                {
                    return CustomerTypeName.WHOLESALE;
                }
                else if (number.IsWithin(minOTHER_RAS, maxOTHER_RAS))
                {
                    return CustomerTypeName.OTHER_RAS;
                }
                else if (number.IsWithin(minKPM, maxKPM))
                {
                    return CustomerTypeName.KPM;
                }
                else
                {
                    return CustomerTypeName.None;
                }
            }
            else
            {
                return CustomerTypeName.InvalidAccountNumber;
            }
        }

    }
}
