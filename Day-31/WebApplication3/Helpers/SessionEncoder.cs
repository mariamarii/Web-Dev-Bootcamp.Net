using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace WebApplication3.Helpers;

public class SessionInfo
{
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string Purpose { get; set; } = string.Empty;
}

public interface ISessionEncoder
{
    string EncodeSession(string userId, DateTime expiresAt, string purpose);
    SessionInfo? DecodeSession(string encodedSession);
    bool IsSessionValid(string encodedSession);
}

public class SessionEncoder(IOptions<OtpConfig> otpConfig) : ISessionEncoder
{
    private readonly OtpConfig _config = otpConfig.Value;

    public string EncodeSession(string userId, DateTime expiresAt, string purpose)
    {
        var sessionInfo = new SessionInfo
        {
            UserId = userId,
            ExpiresAt = expiresAt,
            Purpose = purpose
        };

        var json = JsonSerializer.Serialize(sessionInfo);
        return EncodeString(json);
    }

    public SessionInfo? DecodeSession(string encodedSession)
    {
        try
        {
            var json = DecodeString(encodedSession);
            return JsonSerializer.Deserialize<SessionInfo>(json);
        }
        catch
        {
            return null;
        }
    }

    public bool IsSessionValid(string encodedSession)
    {
        var sessionInfo = DecodeSession(encodedSession);
        return sessionInfo != null && sessionInfo.ExpiresAt > DateTime.UtcNow;
    }

    private string EncodeString(string plainText)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_config.EncryptionKey);
        
        var key = new byte[32];
        if (keyBytes.Length >= 32)
        {
            Array.Copy(keyBytes, key, 32);
        }
        else
        {
            Array.Copy(keyBytes, key, keyBytes.Length);
        }
        
        var iv = new byte[16];
        RandomNumberGenerator.Fill(iv);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor();
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        var encrypted = msEncrypt.ToArray();
        var result = new byte[iv.Length + encrypted.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

        return Convert.ToBase64String(result).Replace('+', '-').Replace('/', '_').Replace("=", "");
    }

    private string DecodeString(string encodedText)
    {
        var base64 = encodedText.Replace('-', '+').Replace('_', '/');
        var padding = base64.Length % 4;
        if (padding > 0)
            base64 += new string('=', 4 - padding);

        var fullCipher = Convert.FromBase64String(base64);
        var iv = new byte[16];
        var cipher = new byte[fullCipher.Length - 16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        var keyBytes = Encoding.UTF8.GetBytes(_config.EncryptionKey);
        
        // Ensure key is exactly 32 bytes for AES-256
        var key = new byte[32];
        if (keyBytes.Length >= 32)
        {
            Array.Copy(keyBytes, key, 32);
        }
        else
        {
            Array.Copy(keyBytes, key, keyBytes.Length);
        }

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}
