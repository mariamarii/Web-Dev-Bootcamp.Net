// using Microsoft.AspNetCore.Mvc;
// using SharedKernel;
// using Web.Api.Infrastructure;
//
// namespace Web.Api.Extensions;
//
// public static class ResultExtensions
// {
//     public static TOut Match<TOut>(
//         this Result result,
//         Func<TOut> onSuccess,
//         Func<Result, TOut> onFailure)
//     {
//         return result.IsSuccess ? onSuccess() : onFailure(result);
//     }
//
//     public static TOut Match<TIn, TOut>(
//         this Result<TIn> result,
//         Func<TIn, TOut> onSuccess,
//         Func<Result<TIn>, TOut> onFailure)
//     {
//         return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
//     }
//
//     public static IActionResult Created(Ulid id)
//     {
//         return new CreatedResult(string.Empty, id);
//     }
//
//     public static IActionResult ToActionResult(this Result result)
//     {
//         return result.Match(
//             () => new OkResult(),
//             error => CustomResults.Problem(result));
//     }
//
//     public static IActionResult ToActionResult<T>(this Result<T> result)
//     {
//         return result.Match(
//             value => new OkObjectResult(value),
//             error => CustomResults.Problem(result));
//     }
//
//     public static IActionResult ToCreatedActionResult<T>(this Result<T> result)
//     {
//         return result.Match(
//             value => new CreatedResult(string.Empty, value),
//             error => CustomResults.Problem(result));
//     }
// }
