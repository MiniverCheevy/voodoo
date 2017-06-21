using System;

namespace Voodoo.Messages.Paging
{
    [Obsolete]
    public struct GridConstants
    {
        public struct ButtonClassStyles
        {
            public const string PageClass = "page";
            public const string PrevClass = "prev";
            public const string NextClass = "next";
            public const string NextBlockClass = "nextBlock";
            public const string PrevBlockClass = "prevBlock";
            public const string FirstClass = "first";
            public const string LastClass = "last";
            public const string PrevClassDisabled = "prev disabled ";
            public const string NextClassDisabled = "next disabled ";
            public const string NextBlockClassDisabled = "nextBlock disabled ";
            public const string PrevBlockClassDisabled = "prevBlock disabled ";
            public const string FirstClassDisabled = "first disabled ";
            public const string LastClassDisabled = "last disabled ";
            public const string PageClassActive = "page active ";
        }

        public struct ButtonText
        {
            public const string Ellipse = "...";
            public const string FirstPage = "<<";
            public const string PreviousPage = "<";
            public const string NextPage = ">";
            public const string LastPage = ">>";
        }
    }
}