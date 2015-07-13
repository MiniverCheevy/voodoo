using System.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Voodoo.Validation.Infrastructure;
using Xunit;

namespace Voodoo.Tests.Voodoo.Operations
{
    public class CommandTests
    {
        [Fact]
        public void Execute_ExceptionIsThrown_IsNotOk()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;

            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.Equal(false, result.IsOk);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [Fact]
        public void Execute_ExceptionIsThrown_ExceptionIsBubbled()
        {
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = false;
            var result = new CommandThatThrowsErrors(new EmptyRequest()).Execute();
            Assert.Equal(TestingResponse.OhNo, result.Message);
            Assert.NotNull(result.Exception);
            VoodooGlobalConfiguration.RemoveExceptionFromResponseAfterLogging = true;
        }

        [Fact]
        public void Execute_CommandReturnsResponse_IsOk()
        {
            var result = new CommandThatDoesNotThrowErrors(new EmptyRequest()).Execute();
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(true, result.IsOk);
        }

#if (!PCL)
        [Fact]
        public void Execute_RequestIsInvalidDataAnnotationsValidatorWithFirstErrorAsMessage_IsNotOk()
        {
         
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithFirstErrorAsMessage());
            var result = new CommandWithNonEmptyRequest(new RequestWithRequiredString()).Execute();
            Assert.NotNull(result);
            Assert.NotNull(result.Message);
            Assert.Equal(result.Details.First().Value, result.Message);            
            Assert.NotEqual(true, result.IsOk);
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithFirstErrorAsMessage());
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithGenericMessage());
        }
        [Fact]
        public void Execute_RequestIsInvalidDataAnnotationsValidatorWithGenericMessage_IsNotOk()
        {

            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithGenericMessage());
            var result = new CommandWithNonEmptyRequest(new RequestWithRequiredString()).Execute();
            Assert.NotNull(result);
            Assert.NotNull(result.Message);
            Assert.Equal(Strings.Validation.validationErrorsOccurred, result.Message);
            Assert.NotEqual(true, result.IsOk);

        }
#endif
    }
}