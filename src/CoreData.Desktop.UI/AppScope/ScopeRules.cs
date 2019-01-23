using System;
using ImTools;

namespace CoreData.Desktop.UI.AppScope
{
    /// <summary>App lifetime scope rules.</summary>
    public class ScopeRules
    {
        /// <summary>Immutable default scope rules set.</summary>
        public static readonly ScopeRules Default = new ScopeRules(
            skipSingletonValidation: default,
            useLastConnection: default);

        /// <summary>Application scope rules ctor with all rules optional and 'default' by default.</summary>
        /// <param name="skipSingletonValidation">Optional parameter acting as 3-state flag:
        /// <para>1) true = skip check; 2) false = do check; 3) default(Opt<bool>) = use switches to decide on check or not.</para></param>
        public ScopeRules(Opt<bool> skipSingletonValidation, Opt<bool> useLastConnection)
        {
            SkipSingletonValidation = skipSingletonValidation.OrDefault(SingletonScopeRule.DefaultSkip);
            UseLastConnection = useLastConnection.OrDefault(true);
        }

        #region App startup rules
        /// <summary>Defines if a new app session could skip identity uniqueness validation.</summary>
        public bool SkipSingletonValidation { get; }

        /// <summary></summary>
        public bool UseLastConnection { get; }

        #endregion App startup rules
    }

    public static class SingletonScopeRule
    {
        public static readonly bool DefaultSkip =
            AppContext.TryGetSwitch("Switch.CoreData.SkipSingletonValidation", out var skip) && skip;

        /// <summary>Verifies identity singleton rule.</summary>
        public static bool Verify()
        {
            return true;
        }
    }
}
