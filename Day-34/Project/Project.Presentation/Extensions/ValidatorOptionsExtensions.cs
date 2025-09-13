// using System.Linq.Expressions;
// using Microsoft.Extensions.Localization;
//
// namespace Project.Presentation.Extensions;
//
// public static class ValidatorOptionsExtensions
// {
//     public static void ConfigureLocalizedDisplayNames(this IServiceCollection serviceCollection)
//     {
//         // Register the localizer factory
//         IStringLocalizerFactory localizerFactory = serviceCollection.BuildServiceProvider()
//             .GetRequiredService<IStringLocalizerFactory>();
//         
//     }
//
//     // Helper method to extract the full property path (e.g., "Address.City")
//     private static string GetPropertyPath(LambdaExpression? lambdaExpression)
//     {
//         if (lambdaExpression == null)
//         {
//             return null;
//         }
//
//         Expression? expression = lambdaExpression.Body;
//         var pathParts = new List<string>();
//
//         while (expression is MemberExpression memberExpression)
//         {
//             pathParts.Add(memberExpression.Member.Name);
//             expression = memberExpression.Expression;
//         }
//
//         // Reverse to get the correct order (e.g., "Address.City")
//         pathParts.Reverse();
//         return string.Join(".", pathParts);
//     }
// }
