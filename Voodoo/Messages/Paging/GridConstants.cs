namespace Voodoo.Messages.Paging
{
    public struct GridConstants
    {
        public struct ButtonClassStyles
        {
            public static readonly string PageClass = "page";
            public static readonly string PrevClass = "prev";
            public static readonly string NextClass = "next";
            public static readonly string NextBlockClass = "nextBlock";
            public static readonly string PrevBlockClass = "prevBlock";
            public static readonly string FirstClass = "first";
            public static readonly string LastClass = "last";
            public static readonly string PrevClassDisabled = "prev disabled ";
            public static readonly string NextClassDisabled = "next disabled ";
            public static readonly string NextBlockClassDisabled = "nextBlock disabled ";
            public static readonly string PrevBlockClassDisabled = "prevBlock disabled ";
            public static readonly string FirstClassDisabled = "first disabled ";
            public static readonly string LastClassDisabled = "last disabled ";
            public static readonly string PageClassActive = "page active ";
        }

        public struct ButtonText
        {
            public static readonly string Ellipse = "...";
            public static readonly string FirstPage = "<<";
            public static readonly string PreviousPage = "<";
            public static readonly string NextPage = ">";
            public static readonly string LastPage = ">>";
        }
    }
}