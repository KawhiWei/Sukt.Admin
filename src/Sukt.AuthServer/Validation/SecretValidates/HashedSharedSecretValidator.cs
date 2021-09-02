using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Module.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.SecretValidates
{
    /// <summary>
    /// hash密钥处理器
    /// </summary>
    public class HashedSharedSecretValidator : ISecretValidator
    {
        private readonly ILogger _logger;
        const int SHA256_FIXED_LEN = 32;//固定sha256的长度
        const int SHA512_FIXED_LEN = SHA256_FIXED_LEN * 2;//固定sha512的长度

        public HashedSharedSecretValidator(ILogger<HashedSharedSecretValidator> logger)
        {
            _logger = logger;
        }

        public async Task<SecretValidationResult> ValidateAsync(SuktApplicationModel suktApplication, ParsedSecret parsedSecret)
        {
            await Task.CompletedTask;
            var result = new SecretValidationResult() { Success = false };
            if (!parsedSecret.Type.Equals(ParsedSecretTypes.SharedSecret))
            {
                _logger.LogDebug("无法处理的共享密钥 {type}", parsedSecret.Type ?? "null");
                return result;
            }
            if (!suktApplication.SecretType.Equals(SecretTypes.SharedSecret))
            {
                return result;
            }
            var currentSecret = parsedSecret.Credential as string;
            if (parsedSecret.Id.IsNullOrEmpty() || currentSecret.IsNullOrEmpty())
            {
                throw new ArgumentException("客户端Id或密钥为空");
            }
            var secretDescription = suktApplication.Description.IsNullOrEmpty() ? "没有描述" : suktApplication.Description;
            byte[] secretBytes;
            try
            {
                secretBytes = Convert.FromBase64String(suktApplication.ClientSecret);
            }
            catch (FormatException)
            {
                //to do 异常日志打印
                _logger.LogInformation("密钥：{description} 使用了无效的hash算法", secretDescription);
                return result;
            }
            catch (ArgumentNullException)
            {
                //to do 异常日志打印
                _logger.LogInformation("密钥：{description} is null", secretDescription);
                return result;
            }

            bool isValid = secretBytes.Length switch
            {
                SHA256_FIXED_LEN => SecretConstantComparer.IsEqual(suktApplication.ClientSecret, currentSecret.Sha256()),
                SHA512_FIXED_LEN => SecretConstantComparer.IsEqual(suktApplication.ClientSecret, currentSecret.Sha512()),
                _ => false,
            };

            if (isValid == true)
            {
                result.Success = true;
                return result;
            }

            _logger.LogDebug("没有找到对应的hash密钥");
            return result;
        }
        /**
         * 阴间写法得到sha256的字符串 
         */
        //public string a(ReadOnlySpan<char> input)
        //{
        //    const int SHA256_FIXED_LEN = 32;//固定sha256的长度
        //    using var sHA = HashAlgorithm.Create(HashAlgorithmName.SHA256.Name);//创建hashsha256
        //    Span<byte> buffer = stackalloc byte[SHA256_FIXED_LEN];//在栈上申请固定长度的内存
        //    var memoryOwner = MemoryPool<byte>.Shared.Rent(Encoding.UTF8.GetMaxByteCount(input.Length));//计算转换的byte
        //    var written = Encoding.UTF8.GetBytes(input, memoryOwner.Memory.Span);//申请内存
        //    if (sHA.TryComputeHash(memoryOwner.Memory.Slice(0, written).Span, buffer, out _))
        //    {
        //        Span<byte> base64Buffer = stackalloc byte[SHA256_FIXED_LEN * 2];
        //    }
        //    Span<char> output = stackalloc char[SHA256_FIXED_LEN * 2];//申请
        //    WriteHexUpper(buffer, output, out _);
        //    return output.ToString();
        //}
        //public static void WriteHexUpper(in byte @byte, in Span<char> span)
        //{
        //    int b;
        //    b = @byte >> 4;
        //    span[0] = (char)(55 + b + (((b - 10) >> 31) & -7));
        //    b = @byte & 0xF;
        //    span[1] = (char)(55 + b + (((b - 10) >> 31) & -7));
        //}
        //public static void WriteHexUpper(in ReadOnlySpan<byte> bytes, in Span<char> span, out int written)
        //{
        //    written = 0;
        //    int bytesLength = bytes.Length;
        //    for (int i = 0; i < bytesLength; i++)
        //    {
        //        WriteHexUpper(bytes[i], span.Slice(i * 2, 2));
        //        written += 2;
        //    }
        //}



    }
}
