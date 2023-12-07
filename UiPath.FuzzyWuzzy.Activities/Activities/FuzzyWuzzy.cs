using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using UiPath.FuzzyWuzzy.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using FuzzySharp;

namespace UiPath.FuzzyWuzzy.Activities
{
    [LocalizedDisplayName(nameof(Resources.FuzzyWuzzy_DisplayName))]
    [LocalizedDescription(nameof(Resources.FuzzyWuzzy_Description))]
    public class FuzzyWuzzy : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.FuzzyWuzzy_String1_DisplayName))]
        [LocalizedDescription(nameof(Resources.FuzzyWuzzy_String1_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> String1 { get; set; }

        [LocalizedDisplayName(nameof(Resources.FuzzyWuzzy_String2_DisplayName))]
        [LocalizedDescription(nameof(Resources.FuzzyWuzzy_String2_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> String2 { get; set; }

        [LocalizedDisplayName(nameof(Resources.FuzzyWuzzy_Match_DisplayName))]
        [LocalizedDescription(nameof(Resources.FuzzyWuzzy_Match_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<int> Match { get; set; }

        #endregion


        #region Constructors

        public FuzzyWuzzy()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (String1 == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(String1)));
            if (String2 == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(String2)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var string1 = String1.Get(context);
            var string2 = String2.Get(context);

            var match = Fuzz.Ratio(string1, string2);

            // Outputs
            return (ctx) => {
                Match.Set(ctx, match);
            };
        }

        #endregion
    }
}

