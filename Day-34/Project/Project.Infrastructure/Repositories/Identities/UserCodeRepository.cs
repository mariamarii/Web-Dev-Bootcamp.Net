// using System.Security.Cryptography;
// using Microsoft.Extensions.Internal;
// using Project.Infrastructure.Data;
//
// namespace Project.Infrastructure.Repositories.Identities;
//
// public class UserCodeRepository(ApplicationDbContext dbContext, DataProtectionTokenProviderSetting tokenProviderSetting)
//     : IUserCodeRepository
// {
//     public async Task<UserCode> CreateCodeAsync(Ulid userId, string token, CancellationToken cancellationToken)
//     {
//         var code = new UserCode
//         {
//             Id = Ulid.NewUlid(),
//             UserId = userId,
//             Token = token,
//             Code = GenerateRandomCode(),
//             CreatedAt = SystemClock.Now,
//             ExpiresIn = tokenProviderSetting.ExpiresIn
//         };
//
//         await dbContext.UserCodes.AddAsync(code, cancellationToken);
//         await dbContext.SaveChangesAsync(cancellationToken);
//
//         return code;
//     }
//
//     public Task<UserCode?> GetByCodeAsync(Ulid userId, string code, CancellationToken cancellationToken)
//     {
//         return dbContext.UserCodes
//             .AsNoTracking()
//             .FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Code == code, cancellationToken);
//     }
//
//     public Task<UserCode?> GetByTokenAsync(Ulid userId, string token, CancellationToken cancellationToken)
//     {
//         return dbContext.UserCodes
//             .AsNoTracking()
//             .FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Token == token, cancellationToken);
//     }
//
//     public void UpdateCode(UserCode userCode, CancellationToken cancellationToken)
//     {
//         dbContext.UserCodes.Update(userCode);
//         dbContext.SaveChanges();
//     }
//
//     private static string GenerateRandomCode()
//     {
//         using var rng = RandomNumberGenerator.Create();
//
//         byte[] bytes = new byte[4];
//         rng.GetBytes(bytes);
//         string code = Math.Abs(BitConverter.ToInt32(bytes, 0) % 1000000)
//             .ToString("D6", System.Globalization.CultureInfo.InvariantCulture);
//         return code;
//     }
// }
