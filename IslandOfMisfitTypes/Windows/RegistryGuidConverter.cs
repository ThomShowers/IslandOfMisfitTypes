using System;
using System.Linq;
using IslandOfMisfitTypes.Collections;
using IslandOfMisfitTypes.Linq;

namespace IslandOfMisfitTypes.Windows
{
    /// <summary>
    /// Converts <see cref="Guid"/> instances between their regular representation and the
    /// representation used by MSI in the registry for ProductCode and UpgradeCode entries (and
    /// perhaps elsewhere).
    /// </summary>
    /// <remarks>
    /// Subkeys found under 
    /// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UpgradeCodes\
    /// represent the UpgradeCode values of installed MSI packages, but their names are the product
    /// of a function applied to the typical string representation of the Guids.
    /// 
    /// Given the starting Guid "{5C6F5296-AC5D-40FD-AE20-3C5E2E704077}", the transformation is as
    /// follows:
    /// - The leading '{' or '(', and trailing '}' or ')' characters are removed
    /// - The first 8 characters are reversed in place
    /// - The next 2 sets of 4 characters are reversed in place
    /// - The remaining 8 sets of 2 characeters are each reversed in place
    /// - The hyphens are removed
    /// 
    /// Applying the process again reverses the process, thereby allowing the return values to be
    /// represented by the <see cref="Guid"/> type as well. This means the API neither has, nor
    /// needs, any knowledge of whether it's taking a regular value and returning a registry
    /// value, or vice versa. This renders the first and last steps of the conversion process inert
    /// in the context of this type's implementation.
    /// <example>
    /// <code>
    /// namespace Examples
    /// {
    ///     class Example 
    ///     {
    ///         public void ConvertGuid()
    ///         {
    ///             var before = Guid.Parse("5C6F5296-AC5D-40FD-AE20-3C5E2E704077");
    ///             var after = RegistryGuidConverter.Convert(before);
    ///             // after.ToString() => "6925F6C5-D5CA-DF04-EA02-C3E5E2070477"
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public static class RegistryGuidConverter
    {
        /// <summary>
        /// Converts a <see cref="Guid"/> to (or back from) a registry formatted 
        /// <see cref="Guid"/>.
        /// </summary>
        /// <param name="target">The <see cref="Guid"/> to convert.</param>
        /// <returns>The converted <see cref="Guid"/>.</returns>
        public static Guid Convert(Guid target)
        {
            return
                Guid.Parse(
                    string.Concat(
                        target.ToString("N")
                        .Batch(new[] { 8, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2 })
                        .SelectMany(s => s.Reverse())));
        }
    }
}
