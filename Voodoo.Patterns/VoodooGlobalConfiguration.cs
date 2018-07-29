﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Validation.Infrastructure;

namespace Voodoo
{
    public static class VoodooGlobalConfiguration
    {
        static VoodooGlobalConfiguration()
        {
            RemoveExceptionFromResponseAfterLogging = true;
            ExceptionTranslator = new ExceptionTranslater();
            RegisterExceptionMapping<ReflectionTypeLoadException>(new ReflectionTypeLoaderExceptionTranslation());
            LogMaximumNumberOfItemsInCollection = 100;
            ErrorDetailLoggingMethodology = ErrorDetailLoggingMethodology.LogAsSecondException;
        }

        public static bool IncludeResponseWithExceptionData { get; set; }
        public static ErrorDetailLoggingMethodology ErrorDetailLoggingMethodology { get; set; }
        internal static ExceptionTranslater ExceptionTranslator { get; set; }
        public static int LogMaximumNumberOfItemsInCollection { get; set; }
        public static string LogFilePath { get; set; }
        public static string ApplicationName { get; set; }
        public static bool RemoveExceptionFromResponseAfterLogging { get; set; }

        public static void RegisterLogger(ILogger logger)
        {
            LogManager.Logger = logger;
        }

        public static void RegisterValidator(IValidator validator)
        {
            ValidationManager.Validator = validator;
        }

        public static void RegisterExceptionMapping<T>(ExceptionTranslation mapper) where T : Exception
        {
            var type = typeof(T);
            if (ExceptionTranslator.ContainsKey(type))
                ExceptionTranslator[type].Add( mapper);
            else
                ExceptionTranslator.Add(type, new List<ExceptionTranslation> { mapper });
        }
    }
}