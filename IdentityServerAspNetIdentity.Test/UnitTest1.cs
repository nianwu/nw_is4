using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServerAspNetIdentity.Test
{
    public class UnitTest1
    {
        private ITestOutputHelper _outputer;

        public UnitTest1(
            ITestOutputHelper outputer
        )
        {
            _outputer = outputer;
        }

        [Theory]
        [InlineData("1262163090@qq.com", "服务器申请", "要求如下:<br>内存4G")]
        public async Task SendMailTest(
            string address
            , string subject
            , string message
        )
        {
            try
            {
                var emailSender = new IdentityServerAspNetIdentity.EmailSender();
                await emailSender.SendEmailAsync(address, subject, message);
            }
            catch (Exception e)
            {
                _outputer.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
