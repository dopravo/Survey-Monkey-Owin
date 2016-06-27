using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Dopravo.Utilities
{
    /// <summary>
    /// This Class Will Include Extension Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks Whether a List of Claims Contains a Certain Claim
        /// </summary>
        /// <param name="claims">List of Claims</param>
        /// <param name="claimName">Claim Name</param>
        /// <returns></returns>
        public static string ClaimValueIfExists(this IEnumerable<Claim> claims, string claimName)
        {
            IEnumerable<Claim> Claims = claims.Where(e => e.Type.Contains(claimName));
            string Value = null;
            if (Claims.Count() == 1)
            {
                Value = Claims.First().Value;
            }
            return Value;
        }

        /// <summary>
        /// Checks Whether a dictionary Contains a Certain Key and return its Value.
        /// </summary>
        /// <typeparam name="KeyType">The Type of Key</typeparam>
        /// <typeparam name="ValueType">The Type of Value</typeparam>
        /// <param name="dictionary">Dictionary where the search will take place</param>
        /// <param name="Key">The Key needed to be found</param>
        public static string PropertyValueIfExists<KeyType, ValueType>(this IDictionary<KeyType, ValueType> dictionary, KeyType Key)
        {
            return dictionary.ContainsKey(Key) ? dictionary[Key].ToString() : null;
        }
    }
}